using dotNetLedger.Transactions;

namespace dotNetLedger.Adapters
{
    /// <summary>
    /// Adaptador RPC "mínimo común" entre Bitcoin/Ethereum/Solana.
    /// - No expone conceptos exclusivos (UTXO scan, rent, program accounts, logs, etc.).
    /// - Las diferencias de plataforma se encapsulan en los DTOs y en el adapter concreto.
    /// </summary>
    public interface ILedgerRpcApiAdapter : ILedgerAdapter
    {
        #region Health / version / network info

        /// <summary>
        /// Asynchronously retrieves the current health status of the ledger.
        /// </summary>
        /// <param name="ct">A cancellation token that can be used to cancel the health check operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="LedgerHealth"/>
        /// object describing the ledger's health status.</returns>
        Task<LedgerHealth> GetHealthAsync(CancellationToken ct = default);

        /// <summary>
        /// Asynchronously retrieves the current version information of the ledger node.
        /// </summary>
        /// <param name="ct">A cancellation token that can be used to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the version information of the
        /// ledger node.</returns>
        Task<LedgerNodeVersion> GetVersionAsync(CancellationToken ct = default);

        /// <summary>
        /// Asynchronously retrieves information about the current ledger network, including status and configuration
        /// details.
        /// </summary>
        /// <param name="ct">A cancellation token that can be used to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a <see
        /// cref="LedgerNetworkInfo"/> object with details about the ledger network.</returns>
        Task<LedgerNetworkInfo> GetNetworkInfoAsync(CancellationToken ct = default);

        /// <summary>
        /// Asynchronously retrieves the current synchronization status of the ledger.
        /// </summary>
        /// <param name="ct">A cancellation token that can be used to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="LedgerSyncStatus"/>
        /// object describing the ledger's synchronization state.</returns>
        Task<LedgerSyncStatus> GetSyncStatusAsync(CancellationToken ct = default);

        #endregion

        #region Head / chain tip

        /// <summary>
        /// Asynchronously retrieves the current head of the ledger, representing the latest block or chain tip.
        /// </summary>
        /// <param name="ct">A cancellation token that can be used to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="LedgerHead"/>
        /// object describing the current chain tip.</returns>
        Task<LedgerHead> GetHeadAsync(CancellationToken ct = default);

        #endregion

        #region Blocks

        /// <summary>
        /// Asynchronously retrieves a ledger block by its identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the ledger block to retrieve.</param>
        /// <param name="options">Optional settings that control how the block is read, such as specifying read consistency or additional data
        /// to include. If null, default read options are used.</param>
        /// <param name="ct">A cancellation token that can be used to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the ledger block if found;
        /// otherwise, null.</returns>
        Task<LedgerBlock?> GetBlockAsync(LedgerBlockId id, LedgerBlockReadOptions? options = null, CancellationToken ct = default);

        #endregion

        #region Transactions

        /// <summary>
        /// Asynchronously retrieves a ledger transaction by its identifier.
        /// </summary>
        /// <remarks>If the transaction does not exist, the returned task completes with a null result.
        /// The operation may be subject to read consistency and access control as specified in the options
        /// parameter.</remarks>
        /// <param name="id">The unique identifier of the transaction to retrieve.</param>
        /// <param name="options">Optional settings that control how the transaction is read, such as read consistency or additional query
        /// options. If null, default read options are used.</param>
        /// <param name="ct">A cancellation token that can be used to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the ledger transaction if found;
        /// otherwise, null.</returns>
        Task<LedgerTransaction?> GetTransactionAsync(LedgerTxId id, LedgerTxReadOptions? options = null, CancellationToken ct = default);

        /// <summary>
        /// Asynchronously retrieves the status of a transaction identified by the specified ledger transaction ID.
        /// </summary>
        /// <param name="id">The unique identifier of the transaction whose status is to be retrieved.</param>
        /// <param name="ct">A cancellation token that can be used to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the status of the specified
        /// transaction.</returns>
        Task<LedgerTxStatus> GetTransactionStatusAsync(LedgerTxId id, CancellationToken ct = default);

        /// <summary>
        /// Broadcasts a signed transaction to the ledger asynchronously.
        /// </summary>
        /// <remarks>The method does not validate the transaction signature; callers must ensure the
        /// transaction is properly signed before broadcasting. The operation may fail if the network is unavailable or
        /// the transaction is invalid.</remarks>
        /// <param name="signedTransaction">A read-only memory buffer containing the signed transaction data to be broadcast. Cannot be empty.</param>
        /// <param name="options">Optional settings that control broadcast behavior, such as retry policies or network selection. If null,
        /// default options are used.</param>
        /// <param name="ct">A cancellation token that can be used to cancel the broadcast operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the outcome of the broadcast,
        /// including success status and any error information.</returns>
        Task<LedgerBroadcastResult> BroadcastSignedTransactionAsync(
            TransactionBase signedTransaction,
            LedgerBroadcastOptions? options = null,
            CancellationToken ct = default);

        #endregion

        #region Fees / cost estimation

        /// <summary>
        /// Estimates the transaction fees required for processing a request on the ledger. This method allows clients
        /// to obtain fee information before submitting a transaction.
        /// </summary>
        /// <remarks>The fee estimation may reflect different fee models depending on the ledger type,
        /// such as fee-rate targets for Bitcoin, gas or fee history for Ethereum, or message fees for Solana. Use this
        /// method to preview costs and optimize transaction parameters before submission.</remarks>
        /// <param name="request">The fee estimation request containing transaction details and parameters relevant to the ledger. Cannot be
        /// null.</param>
        /// <param name="ct">A cancellation token that can be used to cancel the fee estimation operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a fee quote with estimated costs
        /// for the specified transaction.</returns>
        Task<LedgerFeeQuote> EstimateFeesAsync(LedgerFeeRequest request, CancellationToken ct = default);

        #endregion

        #region Preflight / simulation

        /// <summary>
        /// Simulates the execution of a signed transaction to determine whether it would be accepted by the ledger
        /// without modifying ledger state.
        /// </summary>
        /// <remarks>This method allows you to check transaction validity and estimate effects before
        /// submitting to the ledger. No state changes are performed. Typical use cases include validating transactions,
        /// estimating fees, or checking for errors prior to broadcast. The exact simulation behavior may vary depending
        /// on ledger type and options provided.</remarks>
        /// <param name="signedTransaction">The signed transaction to be preflighted. Must contain a complete, valid transaction in binary format.</param>
        /// <param name="options">Optional settings that control simulation behavior, such as resource limits or simulation mode. If null,
        /// default options are used.</param>
        /// <param name="ct">A cancellation token that can be used to cancel the preflight operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a LedgerPreflightResult
        /// describing whether the transaction would be accepted and any relevant simulation details.</returns>
        Task<LedgerPreflightResult> PreflightSignedTransactionAsync(
            byte[] signedTransaction,
            LedgerPreflightOptions? options = null,
            CancellationToken ct = default);

        #endregion
    }
}