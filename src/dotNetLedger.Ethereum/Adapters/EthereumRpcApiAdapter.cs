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
                NetworkName: null,
                ChainId: chainId,
                GenesisId: null,
                Raw: null);
        }

        public override async Task<LedgerSyncStatus> GetSyncStatusAsync(CancellationToken ct = default)
        {
            var syncing = await _client.SendRequestAsync<object>(ApiMethods.eth_syncing.ToString());

            return new LedgerSyncStatus(
                IsSynced: syncing is bool b && b == false,
                Raw: null);
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
                HeightLike: (long)height,
                SlotLike: null,
                HeadId: block?["hash"]?.Value<string>(),
                Raw: null);
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
                CanonicalId: id,
                Hash: block["hash"]?.Value<string>(),
                NumberOrHeight: block["number"] != null
                    ? (long)new HexBigInteger(block["number"]!.Value<string>()).Value
                    : null,
                Slot: null,
                Time: block["timestamp"] != null
                    ? DateTimeOffset.FromUnixTimeSeconds(
                        (long)new HexBigInteger(block["timestamp"]!.Value<string>()).Value)
                    : null,
                Raw: null);
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

            return new LedgerTransaction(id, RawBytes: null, Raw: null);
        }

        public override async Task<LedgerTxStatus> GetTransactionStatusAsync(LedgerTxId id, CancellationToken ct = default)
        {
            var receipt = await _client.SendRequestAsync<JObject>(
                ApiMethods.eth_getTransactionReceipt.ToString(),
                id.Value);

            if (receipt == null)
            {
                return new LedgerTxStatus(
                    IsKnown: false,
                    IsFinal: false,
                    State: "unknown",
                    Raw: null);
            }

            var statusHex = receipt["status"]?.Value<string>();
            var success = statusHex != null && statusHex == "0x1";

            return new LedgerTxStatus(
                IsKnown: true,
                IsFinal: true,
                State: success ? "success" : "failed",
                Raw: null);
        }

        public override async Task<LedgerBroadcastResult> BroadcastSignedTransactionAsync(
            TransactionBase signedTransaction,
            LedgerBroadcastOptions? options = null,
            CancellationToken ct = default)
        {
            if (!signedTransaction.CheckSigned())
                throw new InvalidOperationException("Transaction is not signed");

            var hex = "0x" + Convert.ToHexString(signedTransaction.GetBytes().Span).ToLowerInvariant();

            var txHash = await _client.SendRequestAsync<string>(
                ApiMethods.eth_sendRawTransaction.ToString(),
                hex);

            return new LedgerBroadcastResult(
                Accepted: true,
                TxId: new LedgerTxId(txHash),
                Raw: null);
        }

        public override async Task<LedgerFeeQuote> EstimateFeesAsync(LedgerFeeRequest request, CancellationToken ct = default)
        {
            var gasPriceHex = await _client.SendRequestAsync<string>(ApiMethods.eth_gasPrice.ToString());
            var gasPrice = new HexBigInteger(gasPriceHex).Value;

            return new LedgerFeeQuote(
                Unit: "wei",
                Value: (decimal)gasPrice,
                Raw: null);
        }

        public override async Task<LedgerPreflightResult> PreflightSignedTransactionAsync(
            ReadOnlyMemory<byte> signedTransaction,
            LedgerPreflightOptions? options = null,
            CancellationToken ct = default)
        {
            var hex = "0x" + Convert.ToHexString(signedTransaction.Span).ToLowerInvariant();

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
