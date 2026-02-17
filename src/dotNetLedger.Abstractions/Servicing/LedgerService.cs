using dotNetLedger.Adapters;

namespace dotNetLedger.Servicing
{
    public abstract class LedgerService
    {
        protected ILedgerRpcApiAdapter RpcApi { get; }

        protected ILedgerRpcStreamAdapter? Stream { get; }

        public Address Address { get; }

        protected LedgerService(ILedgerProvider provider, string address)
            : this(new Address(address), provider.RpcApiAdapter, provider.StreamAdapter) { }

        protected LedgerService(ILedgerProvider provider, Address address)
            : this(address, provider.RpcApiAdapter, provider.StreamAdapter) { }

        protected LedgerService(string address, ILedgerRpcApiAdapter rpcApi, ILedgerRpcStreamAdapter? stream = null)
            : this(new Address(address), rpcApi, stream) { }

        protected LedgerService(Address address, ILedgerRpcApiAdapter rpcApi, ILedgerRpcStreamAdapter? stream = null)
        {
            RpcApi = rpcApi ?? throw new ArgumentNullException(nameof(rpcApi));
            Stream = stream;
            Address = address;
        }


    }
}
