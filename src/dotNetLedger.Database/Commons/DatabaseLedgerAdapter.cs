using dotNetLedger.Commons;

namespace dotNetLedger.Database.Commons
{
    public class DatabaseLedgerAdapter : ILedgerCommonAdapter
    {
        public Task<LedgerBroadcastResult> BroadcastSignedTransactionAsync(ReadOnlyMemory<byte> signedTransaction, LedgerBroadcastOptions? options = null, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task<LedgerFeeQuote> EstimateFeesAsync(LedgerFeeRequest request, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task<LedgerBlock?> GetBlockAsync(LedgerBlockId id, LedgerBlockReadOptions? options = null, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task<LedgerHead> GetHeadAsync(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task<LedgerHealth> GetHealthAsync(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task<LedgerNetworkInfo> GetNetworkInfoAsync(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task<LedgerSyncStatus> GetSyncStatusAsync(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task<LedgerTransaction?> GetTransactionAsync(LedgerTxId id, LedgerTxReadOptions? options = null, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task<LedgerTxStatus> GetTransactionStatusAsync(LedgerTxId id, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task<LedgerNodeVersion> GetVersionAsync(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task<LedgerPreflightResult> PreflightSignedTransactionAsync(ReadOnlyMemory<byte> signedTransaction, LedgerPreflightOptions? options = null, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }
    }

}
