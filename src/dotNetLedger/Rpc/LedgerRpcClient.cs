using dotNetLedger.Configuration;

namespace dotNetLedger.Rpc
{
    public abstract class LedgerRpcClient
    {
        private HttpClient? _httpClient;

        /// <summary>
        /// Gets the underlying HTTP client.
        /// </summary>
        public virtual HttpClient HttpClient => _httpClient ??= new();

        /// <summary>
        /// Gets the network address of the ledger node associated with this instance.
        /// </summary>
        public abstract Uri LedgerNodeAddress { get; }

        public LedgerRpcClient(RpcClientOptions options)
        {
        }

        public LedgerRpcClient(HttpClient httpClient, RpcClientOptions options)
        {
            _httpClient = httpClient;
        }

        protected LedgerRpcClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
    }
}
