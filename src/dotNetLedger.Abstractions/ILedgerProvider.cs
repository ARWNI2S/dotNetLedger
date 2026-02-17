using dotNetLedger.Adapters;

namespace dotNetLedger
{
    public interface ILedgerProvider
    {
        ILedgerRpcApiAdapter RpcApiAdapter { get; }

        ILedgerRpcStreamAdapter StreamAdapter { get; }

        ICollection<ILedgerAdapter> Adapters { get; }

        ILedgerAdapter GetAdapter<T>()
            where T : ILedgerAdapter;

        ILedgerAdapter GetAdapter(Type adapterType);
    }
}
