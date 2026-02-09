using dotNetLedger.Database.Adapters;

namespace dotNetLedger.Database
{
    public class DatabaseLedgerProvider : ILedgerProvider
    {
        public DatabaseRpcApiAdapter LedgerAdapter { get; } = default!;
    }
}
