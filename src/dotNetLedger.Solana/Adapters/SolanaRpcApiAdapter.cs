using dotNetLedger.Adapters;
using dotNetLedger.Transactions;
using Solnet.Rpc;
using Solnet.Rpc.Types;

namespace dotNetLedger.Solana.Adapters
{
    internal sealed class SolanaRpcApiAdapter(IRpcClient rpc) : RpcApiAdapterBase, ILedgerRpcApiAdapter
    {
        public override async Task<LedgerHealth> GetHealthAsync(CancellationToken ct = default)
        {
            var r = await rpc.GetHealthAsync();
            return new LedgerHealth(r.WasSuccessful && r.Result == "ok", r.Result);
        }

        public override async Task<LedgerNodeVersion> GetVersionAsync(CancellationToken ct = default)
        {
            var r = await rpc.GetVersionAsync();
            return new LedgerNodeVersion("solana", r.Result?.SolanaCore, r.RawRpcResponse);
        }

        public override async Task<LedgerNetworkInfo> GetNetworkInfoAsync(CancellationToken ct = default)
        {
            var genesis = await rpc.GetGenesisHashAsync();
            return new LedgerNetworkInfo(
                NetworkName: null,
                ChainId: null,
                GenesisId: genesis.Result,
                Raw: genesis.RawRpcResponse);
        }

        public override async Task<LedgerSyncStatus> GetSyncStatusAsync(CancellationToken ct = default)
        {
            // Solana no expone “syncing” explícito → health + slot progression
            var health = await rpc.GetHealthAsync();
            return new LedgerSyncStatus(
                IsSynced: health.Result == "ok",
                Raw: health.RawRpcResponse);
        }

        public override async Task<LedgerHead> GetHeadAsync(CancellationToken ct = default)
        {
            var slot = await rpc.GetSlotAsync();
            var height = await rpc.GetBlockHeightAsync();
            var hash = await rpc.GetLatestBlockHashAsync();

            return new LedgerHead(
                HeightLike: (long)height.Result,
                SlotLike: (long)slot.Result,
                HeadId: hash.Result?.Value?.Blockhash,
                Raw: hash.RawRpcResponse);
        }

        public override async Task<LedgerBlock?> GetBlockAsync(LedgerBlockId id, LedgerBlockReadOptions? options = null, CancellationToken ct = default)
        {
            if (id is not LedgerBlockId.BySlot bySlot)
                return null;

            var r = await rpc.GetBlockAsync((ulong)bySlot.Slot);
            if (!r.WasSuccessful || r.Result == null) return null;

            return new LedgerBlock(
                CanonicalId: id,
                Hash: r.Result.Blockhash,
                NumberOrHeight: null,
                Slot: bySlot.Slot,
                Time: DateTimeOffset.FromUnixTimeSeconds(r.Result.BlockTime),
                Raw: r.RawRpcResponse);
        }

        public override async Task<LedgerTransaction?> GetTransactionAsync(LedgerTxId id, LedgerTxReadOptions? options = null, CancellationToken ct = default)
        {
            var r = await rpc.GetTransactionAsync(id.Value);
            if (!r.WasSuccessful || r.Result == null) return null;

            return new LedgerTransaction(
                id,
                Raw: r.RawRpcResponse);
        }

        public override async Task<LedgerTxStatus> GetTransactionStatusAsync(LedgerTxId id, CancellationToken ct = default)
        {
            var r = await rpc.GetSignatureStatusesAsync(new() { id.Value }, true);
            var s = r.Result?.Value?[0];

            return new LedgerTxStatus(
                IsKnown: s != null,
                IsFinal: s?.ConfirmationStatus == "finalized",
                State: s?.ConfirmationStatus,
                Raw: r.RawRpcResponse);
        }

        public override async Task<LedgerBroadcastResult> BroadcastSignedTransactionAsync(
            TransactionBase signedTransaction,
            LedgerBroadcastOptions? options = null,
            CancellationToken ct = default)
        {
            if (!signedTransaction.CheckSigned())
                throw new InvalidOperationException("Transaction is not signed");

            var r = await rpc.SendTransactionAsync(
                Convert.ToBase64String(signedTransaction.GetBytes().Span),
                skipPreflight: options?.SkipPreflight ?? false,
                preFlightCommitment: Commitment.Finalized);

            return new LedgerBroadcastResult(
                Accepted: r.WasSuccessful,
                TxId: r.WasSuccessful ? new LedgerTxId(r.Result) : null,
                Error: r.ErrorData?.Error?.InstructionError?.BorshIoError,
                Raw: r.RawRpcResponse);
        }

        public override async Task<LedgerFeeQuote> EstimateFeesAsync(LedgerFeeRequest request, CancellationToken ct = default)
        {
            if (request.SignedTransaction is null)
                return new LedgerFeeQuote(null, null);

            var r = await rpc.GetFeeForMessageAsync(Convert.ToBase64String(request.SignedTransaction.Value.ToArray()));
            return new LedgerFeeQuote("lamports", r.Result.Value, Raw: r.RawRpcResponse);
        }

        public override async Task<LedgerPreflightResult> PreflightSignedTransactionAsync(
            ReadOnlyMemory<byte> signedTransaction,
            LedgerPreflightOptions? options = null,
            CancellationToken ct = default)
        {
            var r = await rpc.SimulateTransactionAsync(signedTransaction.ToArray());
            return new LedgerPreflightResult(
                WouldLikelySucceed: r.WasSuccessful && r.ErrorData == null,
                Reason: r.ErrorData?.Error?.InstructionError?.BorshIoError,
                Raw: r.RawRpcResponse);
        }
    }

    //public class SolanaLedgerAdapter : ILedgerCommonAdapter
    //{
    //    public Task<LedgerBroadcastResult> BroadcastSignedTransactionAsync(ReadOnlyMemory<byte> signedTransaction, LedgerBroadcastOptions? options = null, CancellationToken ct = default)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<LedgerFeeQuote> EstimateFeesAsync(LedgerFeeRequest request, CancellationToken ct = default)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<LedgerBlock?> GetBlockAsync(LedgerBlockId id, LedgerBlockReadOptions? options = null, CancellationToken ct = default)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<LedgerHead> GetHeadAsync(CancellationToken ct = default)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<LedgerHealth> GetHealthAsync(CancellationToken ct = default)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<LedgerNetworkInfo> GetNetworkInfoAsync(CancellationToken ct = default)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<LedgerSyncStatus> GetSyncStatusAsync(CancellationToken ct = default)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<LedgerTransaction?> GetTransactionAsync(LedgerTxId id, LedgerTxReadOptions? options = null, CancellationToken ct = default)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<LedgerTxStatus> GetTransactionStatusAsync(LedgerTxId id, CancellationToken ct = default)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<LedgerNodeVersion> GetVersionAsync(CancellationToken ct = default)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<LedgerPreflightResult> PreflightSignedTransactionAsync(ReadOnlyMemory<byte> signedTransaction, LedgerPreflightOptions? options = null, CancellationToken ct = default)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
