using dotNetLedger.Adapters;
using dotNetLedger.Transactions;
using NBitcoin;
using NBitcoin.RPC;
using BTC = NBitcoin.Transaction;

namespace dotNetLedger.Bitcoin.Adapters
{
    internal sealed class BitcoinRpcApiAdapter : RpcApiAdapterBase, ILedgerRpcApiAdapter
    {
        private readonly RPCClient _rpc;

        public BitcoinRpcApiAdapter(RPCClient rpc)
        {
            _rpc = rpc;
        }

        public override async Task<LedgerHealth> GetHealthAsync(CancellationToken ct = default)
        {
            // En Bitcoin no hay "health": liveness = responde
            try
            {
                await _rpc.UptimeAsync(ct);
                return new LedgerHealth(true, "ok");
            }
            catch (Exception ex)
            {
                return new LedgerHealth(false, ex.Message);
            }
        }

        public override async Task<LedgerNodeVersion> GetVersionAsync(CancellationToken ct = default)
        {
            await _rpc.UptimeAsync(ct); // liveness probe
            return new LedgerNodeVersion(
                ClientName: "bitcoind",
                ClientVersion: null,
                Raw: null);
        }

        public override async Task<LedgerNetworkInfo> GetNetworkInfoAsync(CancellationToken ct = default)
        {
            var info = await _rpc.GetBlockchainInfoAsync(ct);
            return new LedgerNetworkInfo(
                NetworkName: info.Chain.Name,
                ChainId: null,
                GenesisId: info.BestBlockHash?.ToString(),
                Raw: null);
        }

        public override async Task<LedgerSyncStatus> GetSyncStatusAsync(CancellationToken ct = default)
        {
            var info = await _rpc.GetBlockchainInfoAsync(ct);
            return new LedgerSyncStatus(
                IsSynced: !info.InitialBlockDownload,
                Progress: info.VerificationProgress,
                Raw: null);
        }

        public override async Task<LedgerHead> GetHeadAsync(CancellationToken ct = default)
        {
            var height = await _rpc.GetBlockCountAsync(ct);
            var hash = await _rpc.GetBestBlockHashAsync(ct);

            return new LedgerHead(
                HeightLike: height,
                SlotLike: null,
                HeadId: hash.ToString(),
                Raw: null);
        }

        public override async Task<LedgerBlock?> GetBlockAsync(
            LedgerBlockId id,
            LedgerBlockReadOptions? options = null,
            CancellationToken ct = default)
        {
            Block block;
            long? height = null;
            switch (id)
            {
                case LedgerBlockId.ByHash byHash:
                    block = await _rpc.GetBlockAsync(uint256.Parse(byHash.Hash), ct);
                    break;

                case LedgerBlockId.ByNumber byNumber:
                    var hash = await _rpc.GetBlockHashAsync((int)byNumber.Number, ct);
                    block = await _rpc.GetBlockAsync(hash, ct);
                    height = byNumber.Number;
                    break;

                default:
                    return null;
            }

            return new LedgerBlock(
                CanonicalId: id,
                Hash: block.GetHash().ToString(),
                NumberOrHeight: height,
                Slot: null,
                Time: block.Header.BlockTime,
                Raw: null);
        }

        public override async Task<LedgerTransaction?> GetTransactionAsync(
            LedgerTxId id,
            LedgerTxReadOptions? options = null,
            CancellationToken ct = default)
        {
            var tx = await _rpc.GetRawTransactionAsync(uint256.Parse(id.Value), cancellationToken: ct);
            return new LedgerTransaction(
                id,
                RawBytes: tx.ToBytes(),
                Raw: null);
        }

        public override async Task<LedgerTxStatus> GetTransactionStatusAsync(LedgerTxId id, CancellationToken ct = default)
        {
            var info = await _rpc.GetRawTransactionInfoAsync(uint256.Parse(id.Value), ct);

            return new LedgerTxStatus(
                IsKnown: true,
                IsFinal: info.Confirmations > 0,
                ConfirmationsLike: info.Confirmations,
                State: info.Confirmations > 0 ? "confirmed" : "mempool",
                Raw: null);
        }

        public override async Task<LedgerBroadcastResult> BroadcastSignedTransactionAsync(
            TransactionBase signedTransaction,
            LedgerBroadcastOptions? options = null,
            CancellationToken ct = default)
        {
            if (!signedTransaction.CheckSigned())
                throw new InvalidOperationException("Transaction is not signed");

            var tx = BTC.Load(signedTransaction.GetBytes().Span.ToArray(), _rpc.Network);
            var txid = await _rpc.SendRawTransactionAsync(tx, ct);

            return new LedgerBroadcastResult(
                Accepted: true,
                TxId: new LedgerTxId(txid.ToString()),
                Raw: null);
        }

        public override async Task<LedgerFeeQuote> EstimateFeesAsync(LedgerFeeRequest request, CancellationToken ct = default)
        {
            var target = request.TargetConfirmations ?? 6;
            var fee = await _rpc.EstimateSmartFeeAsync(target, null, ct);

            return new LedgerFeeQuote(
                Unit: "sat/vB",
                Value: fee.FeeRate?.SatoshiPerByte,
                Raw: null);
        }

        public override async Task<LedgerPreflightResult> PreflightSignedTransactionAsync(
            ReadOnlyMemory<byte> signedTransaction,
            LedgerPreflightOptions? options = null,
            CancellationToken ct = default)
        {
            var tx = BTC.Load(signedTransaction.Span.ToArray(), _rpc.Network);
            var result = await _rpc.TestMempoolAcceptAsync(tx, ct);

            var accepted = result.IsAllowed;

            return new LedgerPreflightResult(
                WouldLikelySucceed: accepted,
                Reason: accepted ? null : result.RejectReason,
                Raw: null);
        }
    }

}
