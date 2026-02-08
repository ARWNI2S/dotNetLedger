using dotNetLedger.Configuration;
using dotNetLedger.Rpc;

namespace dotNetLedger.Solana.Client
{
    public class SolanaRpcClient : LedgerRpcClient, ILedgerRpcClient
    {
        public SolanaRpcClient(RpcClientOptions options)
            : base(options)
        {
        }
        public SolanaRpcClient(HttpClient httpClient, RpcClientOptions options)
            : base(httpClient, options)
        {
        }
        public SolanaRpcClient(HttpClient httpClient)
            : base(httpClient)
        {
        }
    }
}
