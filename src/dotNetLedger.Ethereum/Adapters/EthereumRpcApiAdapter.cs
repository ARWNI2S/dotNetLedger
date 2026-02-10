using dotNetLedger.Adapters;
using dotNetLedger.Transactions;
using Nethereum.Hex.HexTypes;
using Nethereum.JsonRpc.Client;
using Newtonsoft.Json.Linq;

namespace dotNetLedger.Ethereum.Adapters
{
    internal sealed class EthereumRpcApiAdapter : RpcApiAdapterBase, ILedgerRpcApiAdapter
    {
        private readonly IClient _client;

        public EthereumRpcApiAdapter(IClient client)
        {
            _client = client;
        }

        public override async Task<LedgerHealth> GetHealthAsync(CancellationToken ct = default)
        {
            try
            {
                await _client.SendRequestAsync<string>(ApiMethods.web3_clientVersion.ToString());
                return new LedgerHealth(true, "ok");
            }
            catch (Exception ex)
            {
                return new LedgerHealth(false, ex.Message);
            }
        }

        public override async Task<LedgerNodeVersion> GetVersionAsync(CancellationToken ct = default)
        {
            var version = await _client.SendRequestAsync<string>(ApiMethods.web3_clientVersion.ToString());
            return new LedgerNodeVersion("ethereum", version, null);
        }

        public override async Task<LedgerNetworkInfo> GetNetworkInfoAsync(CancellationToken ct = default)
        {
            var chainId = await _client.SendRequestAsync<string>(ApiMethods.eth_chainId.ToString());
            return new LedgerNetworkInfo(
                networkName: null,
                chainId: chainId,
                genesisId: null,
                raw: null);
        }

        public override async Task<LedgerSyncStatus> GetSyncStatusAsync(CancellationToken ct = default)
        {
            var syncing = await _client.SendRequestAsync<object>(ApiMethods.eth_syncing.ToString());

            return new LedgerSyncStatus(
                isSynced: syncing is bool b && b == false,
                raw: null);
        }

        public override async Task<LedgerHead> GetHeadAsync(CancellationToken ct = default)
        {
            var blockNumberHex = await _client.SendRequestAsync<string>(ApiMethods.eth_blockNumber.ToString());
            var height = new HexBigInteger(blockNumberHex).Value;

            var block = await _client.SendRequestAsync<JObject>(
                ApiMethods.eth_getBlockByNumber.ToString(),
                "latest",
                false);

            return new LedgerHead(
                heightLike: (long)height,
                slotLike: null,
                headId: block?["hash"]?.Value<string>(),
                raw: null);
        }

        public override async Task<LedgerBlock?> GetBlockAsync(
            LedgerBlockId id,
            LedgerBlockReadOptions? options = null,
            CancellationToken ct = default)
        {
            JObject? block = id switch
            {
                LedgerBlockId.ByHash byHash =>
                    await _client.SendRequestAsync<JObject>(
                        ApiMethods.eth_getBlockByHash.ToString(),
                        byHash.Hash,
                        false),

                LedgerBlockId.ByNumber byNumber =>
                    await _client.SendRequestAsync<JObject>(
                        ApiMethods.eth_getBlockByNumber.ToString(),
                        new HexBigInteger(byNumber.Number).HexValue,
                        false),

                _ => null
            };

            if (block == null) return null;

            return new LedgerBlock(
                canonicalId: id,
                hash: block["hash"]?.Value<string>(),
                numberOrHeight: block["number"] != null
                    ? (long)new HexBigInteger(block["number"]!.Value<string>()).Value
                    : null,
                slot: null,
                time: block["timestamp"] != null
                    ? DateTimeOffset.FromUnixTimeSeconds(
                        (long)new HexBigInteger(block["timestamp"]!.Value<string>()).Value)
                    : null,
                raw: null);
        }

        public override async Task<LedgerTransaction?> GetTransactionAsync(
            LedgerTxId id,
            LedgerTxReadOptions? options = null,
            CancellationToken ct = default)
        {
            var tx = await _client.SendRequestAsync<JObject>(
                ApiMethods.eth_getTransactionByHash.ToString(),
                id.Value);

            if (tx == null) return null;

            return new LedgerTransaction(id, rawBytes: null, raw: null);
        }

        public override async Task<LedgerTxStatus> GetTransactionStatusAsync(LedgerTxId id, CancellationToken ct = default)
        {
            var receipt = await _client.SendRequestAsync<JObject>(
                ApiMethods.eth_getTransactionReceipt.ToString(),
                id.Value);

            if (receipt == null)
            {
                return new LedgerTxStatus(
                    isKnown: false,
                    isFinal: false,
                    state: "unknown",
                    raw: null);
            }

            var statusHex = receipt["status"]?.Value<string>();
            var success = statusHex != null && statusHex == "0x1";

            return new LedgerTxStatus(
                isKnown: true,
                isFinal: true,
                state: success ? "success" : "failed",
                raw: null);
        }

        public override async Task<LedgerBroadcastResult> BroadcastSignedTransactionAsync(
            TransactionBase signedTransaction,
            LedgerBroadcastOptions? options = null,
            CancellationToken ct = default)
        {
            if (!signedTransaction.CheckSigned())
                throw new InvalidOperationException("Transaction is not signed");

            var hex = "0x" + Convert.ToHexString(signedTransaction.GetBytes()).ToLowerInvariant();

            var txHash = await _client.SendRequestAsync<string>(
                ApiMethods.eth_sendRawTransaction.ToString(),
                hex);

            return new LedgerBroadcastResult(
                accepted: true,
                txId: new LedgerTxId(txHash),
                raw: null);
        }

        public override async Task<LedgerFeeQuote> EstimateFeesAsync(LedgerFeeRequest request, CancellationToken ct = default)
        {
            var gasPriceHex = await _client.SendRequestAsync<string>(ApiMethods.eth_gasPrice.ToString());
            var gasPrice = new HexBigInteger(gasPriceHex).Value;

            return new LedgerFeeQuote(
                unit: "wei",
                value: (decimal)gasPrice,
                raw: null);
        }

        public override async Task<LedgerPreflightResult> PreflightSignedTransactionAsync(
            byte[] signedTransaction,
            LedgerPreflightOptions? options = null,
            CancellationToken ct = default)
        {
            var hex = "0x" + Convert.ToHexString(signedTransaction).ToLowerInvariant();

            try
            {
                await _client.SendRequestAsync<string>(
                    ApiMethods.eth_estimateGas.ToString(),
                    null,
                    new JObject { ["data"] = hex });

                return new LedgerPreflightResult(true, null, null);
            }
            catch (Exception ex)
            {
                return new LedgerPreflightResult(false, ex.Message, null);
            }
        }
    }
}
