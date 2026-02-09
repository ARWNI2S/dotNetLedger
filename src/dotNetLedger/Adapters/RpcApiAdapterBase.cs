using dotNetLedger.Transactions;

namespace dotNetLedger.Adapters
{
    public abstract class RpcApiAdapterBase : ILedgerAdapter
    {
        public virtual Task<LedgerBroadcastResult> BroadcastSignedTransactionAsync(TransactionBase signedTransaction, LedgerBroadcastOptions? options = null, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public virtual Task<LedgerFeeQuote> EstimateFeesAsync(LedgerFeeRequest request, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public virtual Task<LedgerBlock?> GetBlockAsync(LedgerBlockId id, LedgerBlockReadOptions? options = null, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public virtual Task<LedgerHead> GetHeadAsync(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public virtual Task<LedgerHealth> GetHealthAsync(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public virtual Task<LedgerNetworkInfo> GetNetworkInfoAsync(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public virtual Task<LedgerSyncStatus> GetSyncStatusAsync(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public virtual Task<LedgerTransaction?> GetTransactionAsync(LedgerTxId id, LedgerTxReadOptions? options = null, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public virtual Task<LedgerTxStatus> GetTransactionStatusAsync(LedgerTxId id, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public virtual Task<LedgerNodeVersion> GetVersionAsync(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public virtual Task<LedgerPreflightResult> PreflightSignedTransactionAsync(ReadOnlyMemory<byte> signedTransaction, LedgerPreflightOptions? options = null, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }
    }
}
