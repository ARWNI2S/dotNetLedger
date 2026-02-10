namespace dotNetLedger.Servicing
{
    public interface ILedgerContext
    {
    }

    public interface IAddressableContext : ILedgerContext, ILedgerAddressable
    {
    }
}
