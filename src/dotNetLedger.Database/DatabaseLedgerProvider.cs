using dotNetLedger.Commons;
using dotNetLedger.Database.Commons;

namespace dotNetLedger.Database
{
    public class DatabaseLedgerProvider : ILedgerProvider
    {
        public DatabaseLedgerAdapter LedgerAdapter { get; } = default!;

        ILedgerCommonAdapter ILedgerProvider.LedgerAdapter => LedgerAdapter;
    }
}
