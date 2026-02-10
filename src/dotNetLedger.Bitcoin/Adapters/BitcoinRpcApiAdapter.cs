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
                clientName: "bitcoind",
                clientVersion: null,
                raw: null);
        }

        public override async Task<LedgerNetworkInfo> GetNetworkInfoAsync(CancellationToken ct = default)
        {
            var info = await _rpc.GetBlockchainInfoAsync(ct);
            return new LedgerNetworkInfo(
                networkName: info.Chain.Name,
                chainId: null,
                genesisId: info.BestBlockHash?.ToString(),
                raw: null);
        }

        public override async Task<LedgerSyncStatus> GetSyncStatusAsync(CancellationToken ct = default)
        {
            var info = await _rpc.GetBlockchainInfoAsync(ct);
            return new LedgerSyncStatus(
                isSynced: !info.InitialBlockDownload,
                progress: info.VerificationProgress,
                raw: null);
        }

        public override async Task<LedgerHead> GetHeadAsync(CancellationToken ct = default)
        {
            var height = await _rpc.GetBlockCountAsync(ct);
            var hash = await _rpc.GetBestBlockHashAsync(ct);

            return new LedgerHead(
                heightLike: height,
                slotLike: null,
                headId: hash.ToString(),
                raw: null);
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
                canonicalId: id,
                hash: block.GetHash().ToString(),
                numberOrHeight: height,
                slot: null,
                time: block.Header.BlockTime,
                raw: null);
        }

        public override async Task<LedgerTransaction?> GetTransactionAsync(
            LedgerTxId id,
            LedgerTxReadOptions? options = null,
            CancellationToken ct = default)
        {
            var tx = await _rpc.GetRawTransactionAsync(uint256.Parse(id.Value), cancellationToken: ct);
            return new LedgerTransaction(
                id,
                rawBytes: tx.ToBytes(),
                raw: null);
        }

        public override async Task<LedgerTxStatus> GetTransactionStatusAsync(LedgerTxId id, CancellationToken ct = default)
        {
            var info = await _rpc.GetRawTransactionInfoAsync(uint256.Parse(id.Value), ct);

            return new LedgerTxStatus(
                isKnown: true,
                isFinal: info.Confirmations > 0,
                confirmationsLike: info.Confirmations,
                state: info.Confirmations > 0 ? "confirmed" : "mempool",
                raw: null);
        }

        public override async Task<LedgerBroadcastResult> BroadcastSignedTransactionAsync(
            TransactionBase signedTransaction,
            LedgerBroadcastOptions? options = null,
            CancellationToken ct = default)
        {
            if (!signedTransaction.CheckSigned())
                throw new InvalidOperationException("Transaction is not signed");

            var tx = BTC.Load(signedTransaction.GetBytes(), _rpc.Network);
            var txid = await _rpc.SendRawTransactionAsync(tx, ct);

            return new LedgerBroadcastResult(
                accepted: true,
                txId: new LedgerTxId(txid.ToString()),
                raw: null);
        }

        public override async Task<LedgerFeeQuote> EstimateFeesAsync(LedgerFeeRequest request, CancellationToken ct = default)
        {
            var target = request.TargetConfirmations ?? 6;
            var fee = await _rpc.EstimateSmartFeeAsync(target, null, ct);

            return new LedgerFeeQuote(
                unit: "sat/vB",
                value: fee.FeeRate?.SatoshiPerByte,
                raw: null);
        }

        public override async Task<LedgerPreflightResult> PreflightSignedTransactionAsync(
            byte[] signedTransaction,
            LedgerPreflightOptions? options = null,
            CancellationToken ct = default)
        {
            var tx = BTC.Load(signedTransaction, _rpc.Network);
            var result = await _rpc.TestMempoolAcceptAsync(tx, ct);

            var accepted = result.IsAllowed;

            return new LedgerPreflightResult(
                wouldLikelySucceed: accepted,
                reason: accepted ? null : result.RejectReason,
                raw: null);
        }
    }

}
