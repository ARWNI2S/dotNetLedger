using dotNetLedger.Commons;

namespace dotNetLedger
{
    public interface ILedgerProvider
    {
        ILedgerCommonAdapter LedgerAdapter { get; }
    }
}
