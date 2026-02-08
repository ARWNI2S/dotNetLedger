namespace dotNetLedger.Capabilities
{
    public interface ILedgerCapability<T>
    {
        T Service { get; }
    }
}
