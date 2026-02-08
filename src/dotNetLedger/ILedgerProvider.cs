using dotNetLedger.Commons;

namespace dotNetLedger
{
    public interface ILedgerProvider
    {
        ILedgerAdapter LedgerAdapter { get; }
    }
}
