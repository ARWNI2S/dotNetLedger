namespace dotNetLedger.Adapters
{
    /// <summary>
    /// Represents the health status of a ledger, including an optional message describing the condition.
    /// </summary>
    /// <param name="IsHealthy">A value indicating whether the ledger is considered healthy. Set to <see langword="true"/> if the ledger is
    /// healthy; otherwise, <see langword="false"/>.</param>
    /// <param name="message">An optional message providing additional information about the ledger's health. May be <see langword="null"/> if
    /// no message is available.</param>
    public readonly struct LedgerHealth(bool isHealthy, string? message = null)
    {
        public bool IsHealthy { get; } = isHealthy;
        public string? Message { get; } = message;
    }

    /// <summary>
    /// Represents the version information for a ledger node, including client name, client version, and optional raw
    /// version data.
    /// </summary>
    /// <param name="clientName">The name of the ledger node client. Can be null if the client name is unknown.</param>
    /// <param name="clientVersion">The version string of the ledger node client. Can be null if the client version is unknown.</param>
    /// <param name="raw">An optional raw version string as reported by the node. Can be null if not available.</param>
    public readonly struct LedgerNodeVersion(string? clientName, string? clientVersion, string? raw = null)
    {
        public string? ClientName { get; } = clientName;
        public string? ClientVersion { get; } = clientVersion;
        public string? Raw { get; } = raw;
    }


    /// <summary>
    /// Represents network information for a ledger, including identifiers and optional raw metadata.
    /// </summary>
    /// <param name="NetworkName">The name of the ledger network. Can be null if the network name is not available.</param>
    /// <param name="ChainId">The unique identifier for the blockchain network. Can be null if not specified.</param>
    /// <param name="GenesisId">The identifier of the genesis block for the network. Can be null if not provided.</param>
    /// <param name="Raw">Optional raw metadata or additional information about the network. Can be null if not applicable.</param>
    public readonly struct LedgerNetworkInfo(
        string? networkName,
        string? chainId,
        string? genesisId,
        string? raw = null)
    {
        public string? NetworkName { get; } = networkName;
        public string? ChainId { get; } = chainId;
        public string? GenesisId { get; } = genesisId;
        public string? Raw { get; } = raw;
    }


    /// <summary>
    /// Represents the synchronization status of a ledger, including whether it is fully synced, the progress
    /// percentage, the current stage, and any raw status information.
    /// </summary>
    /// <param name="isSynced">Indicates whether the ledger is fully synchronized. Set to <see langword="true"/> if synchronization is
    /// complete; otherwise, <see langword="false"/>.</param>
    /// <param name="progress">The progress of the synchronization as a percentage, or <see langword="null"/> if progress is not available.</param>
    /// <param name="stage">The current stage or phase of the synchronization process, or <see langword="null"/> if not specified.</param>
    /// <param name="raw">Raw status information or details about the synchronization, or <see langword="null"/> if not provided.</param>
    public readonly struct LedgerSyncStatus(
        bool isSynced,
        double? progress = null,
        string? stage = null,
        string? raw = null)
    {
        public bool IsSynced { get; } = isSynced;
        public double? Progress { get; } = progress;
        public string? Stage { get; } = stage;
        public string? Raw { get; } = raw;
    }

    /// <summary>
    /// Represents the head information of a blockchain ledger, including block height, slot, block hash, and optional
    /// metadata.
    /// </summary>
    /// <remarks>This struct is intended to provide a unified representation of ledger head data across
    /// multiple blockchain platforms. The meaning of each field may vary depending on the underlying ledger type. All
    /// fields are optional to accommodate differences between ledgers.</remarks>
    /// <param name="HeightLike">The block height or equivalent identifier for the ledger. For Bitcoin, this is the block height; for Ethereum,
    /// the block number; for Solana, the block height if applicable. Can be null if not available.</param>
    /// <param name="slotLike">The slot number for Solana ledgers, if applicable. Can be null for ledgers that do not use slots.</param>
    /// <param name="headId">The unique identifier of the ledger head, such as the block hash for Bitcoin, Ethereum, or Solana. Can be null
    /// if not available.</param>
    /// <param name="finalityHint">An optional hint indicating the finality status of the ledger head. Can be null if finality information is not
    /// provided.</param>
    /// <param name="raw">An optional raw string containing additional ledger head metadata or serialized information. Can be null if not
    /// provided.</param>
    public readonly struct LedgerHead(
        long? heightLike,          // BTC height, ETH blockNumber, SOL blockHeight (si aplica)
        long? slotLike,            // SOL slot (si aplica)
        string? headId,            // BTC block hash / ETH block hash / SOL blockhash
        string? finalityHint = null,
        string? raw = null)
    {
        public long? HeightLike { get; } = heightLike;
        public long? SlotLike { get; } = slotLike;
        public string? HeadId { get; } = headId;
        public string? FinalityHint { get; } = finalityHint;
        public string? Raw { get; } = raw;
    }

    /// <summary>
    /// Represents a unique identifier for a ledger block across different blockchain platforms.
    /// </summary>
    /// <remarks>This abstract struct provides a unified way to reference blocks by hash, number, or slot,
    /// depending on the blockchain protocol. Use the appropriate derived type—ByHash, ByNumber, or BySlot—to specify
    /// the block identifier relevant to the target blockchain. For example, ByHash is suitable for blockchains like
    /// Bitcoin and Ethereum, ByNumber for block numbers or heights, and BySlot for slot-based blockchains such as
    /// Solana.</remarks>
    public abstract class LedgerBlockId
    {
        public sealed class ByHash(string hash) : LedgerBlockId       // BTC/ETH (hash), SOL (blockhash) si lo usas así
        {
            public string Hash { get; } = hash;
        }
        public sealed class ByNumber(long number) : LedgerBlockId     // ETH block number / BTC height (si decides mapear)
        {
            public long Number { get; } = number;
        }
        public sealed class BySlot(long slot) : LedgerBlockId        // SOL slot
        {
            public long Slot { get; } = slot;
        }
    }

    /// <summary>
    /// Specifies options for reading a ledger block, including whether to retrieve only the block header and whether to
    /// include transaction data.
    /// </summary>
    /// <param name="headerOnly">Indicates whether only the block header should be retrieved. Set to <see langword="true"/> to exclude
    /// transaction and other block details; otherwise, <see langword="false"/> to retrieve the full block.</param>
    /// <param name="includeTransactions">Indicates whether transaction data should be included in the block read. Set to <see langword="true"/> to
    /// include transactions; otherwise, <see langword="false"/> to omit them.</param>
    public readonly struct LedgerBlockReadOptions(
        bool headerOnly = false,
        bool includeTransactions = true)
    {
        public bool HeaderOnly { get; } = headerOnly;
        public bool IncludeTransactions { get; } = includeTransactions;
    }

    /// <summary>
    /// Represents an immutable block in a ledger, containing identifying information, metadata, and a collection of
    /// transaction identifiers.
    /// </summary>
    /// <remarks>This struct is intended for use in ledger systems where blocks may contain optional metadata
    /// and transaction lists. All properties are immutable and may be null if the corresponding information is not
    /// present in the source ledger.</remarks>
    /// <param name="canonicalId">The canonical identifier for the block, used to uniquely distinguish it within the ledger.</param>
    /// <param name="Hash">The hash value of the block, typically used for integrity verification. Can be null if not available.</param>
    /// <param name="numberOrHeight">The block's number or height in the ledger sequence. Can be null if not applicable.</param>
    /// <param name="slot">The slot number associated with the block, if the ledger uses slot-based ordering. Can be null if not used.</param>
    /// <param name="time">The timestamp indicating when the block was created or structed. Can be null if not specified.</param>
    /// <param name="transactions">A read-only list of transaction identifiers included in the block. Can be null or empty if the block contains no
    /// transactions.</param>
    /// <param name="raw">The raw, serialized representation of the block, if available. Can be null if not provided.</param>
    public readonly struct LedgerBlock(
            LedgerBlockId canonicalId,
            string? hash,
            long? numberOrHeight,
            long? slot,
            DateTimeOffset? time,
            IReadOnlyList<LedgerTxId>? transactions = null,
            string? raw = null)
    {
        public LedgerBlockId CanonicalId { get; } = canonicalId;
        public string? Hash { get; } = hash;
        public long? NumberOrHeight { get; } = numberOrHeight;
        public long? Slot { get; } = slot;
        public DateTimeOffset? Time { get; } = time;
        public IReadOnlyList<LedgerTxId>? Transactions { get; } = transactions;
        public string? Raw { get; } = raw;
    }

    /// <summary>
    /// Represents a unique identifier for a ledger transaction.
    /// </summary>
    /// <param name="value">The string value that uniquely identifies the ledger transaction. Cannot be null or empty.</param>
    public readonly struct LedgerTxId(string value)
    {
        public string Value { get; } = value;
    }

    /// <summary>
    /// Specifies options for reading a ledger transaction, including whether to include the raw transaction data.
    /// </summary>
    /// <param name="includeRaw">A value indicating whether the raw transaction data should be included in the read result. Set to <see
    /// langword="true"/> to include raw data; otherwise, <see langword="false"/>.</param>
    public readonly struct LedgerTxReadOptions(
        bool includeRaw = false)
    {
        public bool IncludeRaw { get; } = includeRaw;
    }

    /// <summary>
    /// Represents an immutable ledger transaction, including its unique identifier and optional raw data formats.
    /// </summary>
    /// <remarks>Use this struct to encapsulate transaction details when working with ledger operations. The
    /// raw data fields provide access to original transaction formats, which may be useful for auditing or
    /// interoperability scenarios.</remarks>
    /// <param name="Id">The unique identifier for the ledger transaction.</param>
    /// <param name="rawBytes">The optional raw binary representation of the transaction. May be null if not available.</param>
    /// <param name="raw">The optional raw string representation of the transaction. May be null if not available.</param>
    public readonly struct LedgerTransaction(
        LedgerTxId Id,
        byte[]? rawBytes = null,
        string? raw = null)
    {
        public LedgerTxId Id { get; } = Id;
        public byte[]? RawBytes { get; } = rawBytes;
        public string? Raw { get; } = raw;
    }

    /// <summary>
    /// Represents the status of a ledger transaction, including its known state, finality, confirmation count, and
    /// additional status details.
    /// </summary>
    /// <remarks>This struct is typically used to convey transaction status information retrieved from a
    /// ledger or blockchain system. The meaning of the state and raw fields may vary depending on the underlying ledger
    /// implementation.</remarks>
    /// <param name="isKnown">Indicates whether the transaction is recognized by the ledger. Set to <see langword="true"/> if the transaction
    /// is known; otherwise, <see langword="false"/>.</param>
    /// <param name="isFinal">Indicates whether the transaction's status is considered final and will not change. Set to <see
    /// langword="true"/> if the status is final; otherwise, <see langword="false"/>.</param>
    /// <param name="confirmationsLike">The number of confirmations or similar metric associated with the transaction, if available. May be <see
    /// langword="null"/> if not applicable or unknown.</param>
    /// <param name="state">A string representing the current state or status of the transaction, such as 'pending', 'confirmed', or other
    /// ledger-specific values. May be <see langword="null"/> if not specified.</param>
    /// <param name="raw">The raw status information as provided by the ledger, which may include protocol-specific or unprocessed
    /// details. May be <see langword="null"/> if not available.</param>
    public readonly struct LedgerTxStatus(
        bool isKnown,
        bool isFinal,
        long? confirmationsLike = null,
        string? state = null,
        string? raw = null)
    {
        public bool IsKnown { get; } = isKnown;
        public bool IsFinal { get; } = isFinal;
        public long? ConfirmationsLike { get; } = confirmationsLike;
        public string? State { get; } = state;
        public string? Raw { get; } = raw;
    }

    /// <summary>
    /// Represents options for broadcasting a ledger operation, allowing customization of preflight checks and retry
    /// behavior.
    /// </summary>
    /// <param name="skipPreflight">Specifies whether to skip preflight validation before broadcasting the ledger operation. Set to <see
    /// langword="true"/> to bypass preflight checks; otherwise, <see langword="false"/>.</param>
    /// <param name="maxRetries">The maximum number of retry attempts allowed if the broadcast operation fails. Specify <see langword="null"/> to
    /// use the default retry policy.</param>
    public readonly struct LedgerBroadcastOptions(
        bool skipPreflight = false,
        int? maxRetries = null)
    {
        public bool SkipPreflight { get; } = skipPreflight;
        public int? MaxRetries { get; } = maxRetries;
    }

    /// <summary>
    /// Represents the result of broadcasting a transaction to the ledger, including acceptance status, transaction
    /// identifier, and any error information.
    /// </summary>
    /// <param name="accepted">A value indicating whether the transaction was accepted by the ledger. Set to <see langword="true"/> if the
    /// broadcast succeeded; otherwise, <see langword="false"/>.</param>
    /// <param name="txId">The unique identifier of the transaction as assigned by the ledger, or <see langword="null"/> if the transaction
    /// was not accepted.</param>
    /// <param name="error">An error message describing why the transaction was not accepted, or <see langword="null"/> if no error
    /// occurred.</param>
    /// <param name="raw">The raw response returned by the ledger after broadcasting the transaction, or <see langword="null"/> if
    /// unavailable.</param>
    public readonly struct LedgerBroadcastResult(
        bool accepted,
        LedgerTxId? txId = null,
        string? error = null,
        string? raw = null)
    {
        public bool Accepted { get; } = accepted;
        public LedgerTxId? TxId { get; } = txId;
        public string? Error { get; } = error;
        public string? Raw { get; } = raw;
    }

    /// <summary>
    /// Represents a request to estimate the fee required for a ledger operation, including the intended action and
    /// optional transaction details.
    /// </summary>
    /// <remarks>This struct is typically used when querying a ledger or blockchain system to determine the
    /// fee required for a specific operation. Providing a signed transaction and target confirmations can yield a more
    /// accurate fee estimate based on the actual transaction and desired confirmation timeframe.</remarks>
    /// <param name="intent">The intent describing the ledger operation for which the fee is to be estimated.</param>
    /// <param name="signedTransaction">An optional signed transaction payload to be used for fee estimation. If provided, the fee will be calculated
    /// based on this transaction.</param>
    /// <param name="targetConfirmations">The optional number of target confirmations for the operation. If specified, the fee estimation will consider
    /// the desired confirmation speed.</param>
    public readonly struct LedgerFeeRequest(
        // “lo mínimo común”: una intención + payload opcional.
        LedgerFeeIntent intent,
        byte[]? signedTransaction = null,
        int? targetConfirmations = null)
    {
        public LedgerFeeIntent Intent { get; } = intent;
        public byte[]? SignedTransaction { get; } = signedTransaction;
        public int? TargetConfirmations { get; } = targetConfirmations;
    }

    /// <summary>
    /// Specifies the intent for fee selection when broadcasting a ledger transaction.
    /// </summary>
    /// <remarks>Use this enumeration to indicate whether a transaction should prioritize timely inclusion in
    /// the ledger or minimize transaction costs. The value influences how transaction fees are determined and may
    /// affect processing speed and cost.</remarks>
    public enum LedgerFeeIntent
    {
        Unknown = 0,
        BroadcastSoon,     // “quiero que entre pronto”
        BroadcastCheap     // “quiero minimizar coste”
    }

    /// <summary>
    /// Represents a quoted fee for a ledger transaction, including the unit of measurement, value, and optional
    /// strategy and raw quote information.
    /// </summary>
    /// <param name="unit">The unit of the fee quote, such as "sat/vB", "gwei", or "lamports". May be null if the unit is unspecified.</param>
    /// <param name="value">The quoted fee value, expressed in the specified unit. May be null if the value is not available.</param>
    /// <param name="strategy">An optional description of the fee calculation strategy, such as "fastest" or "economical". May be null if no
    /// strategy is provided.</param>
    /// <param name="raw">An optional raw string containing the original fee quote or additional context. May be null if not applicable.</param>
    public readonly struct LedgerFeeQuote(
        string? unit,          // sat/vB, gwei, lamports, etc.
        decimal? value,
        string? strategy = null,
        string? raw = null)
    {
        public string? Unit { get; } = unit;
        public decimal? Value { get; } = value;
        public string? Strategy { get; } = strategy;
        public string? Raw { get; } = raw;
    }

    /// <summary>
    /// Represents options for preflight ledger operations, allowing configuration of detail inclusion in results.
    /// </summary>
    /// <param name="includeDetails">Specifies whether detailed information should be included in the preflight operation results. Set to <see
    /// langword="true"/> to include details; otherwise, <see langword="false"/>.</param>
    public readonly struct LedgerPreflightOptions(
        bool includeDetails = true)
    {
        public bool IncludeDetails { get; } = includeDetails;
    }

    /// <summary>
    /// Represents the result of a preflight check for a ledger operation, indicating whether the operation would likely
    /// succeed and providing optional diagnostic information.
    /// </summary>
    /// <param name="wouldLikelySucceed">A value indicating whether the ledger operation is expected to succeed. <see langword="true"/> if the operation
    /// would likely succeed; otherwise, <see langword="false"/>.</param>
    /// <param name="reason">An optional string describing the reason for the expected outcome. May be <see langword="null"/> if no
    /// additional information is available.</param>
    /// <param name="raw">An optional string containing raw diagnostic or response data from the preflight check. May be <see
    /// langword="null"/> if not applicable.</param>
    public readonly struct LedgerPreflightResult(
        bool wouldLikelySucceed,
        string? reason = null,
        string? raw = null)
    {
        public bool WouldLikelySucceed { get; } = wouldLikelySucceed;
        public string? Reason { get; } = reason;
        public string? Raw { get; } = raw;
    }

}
