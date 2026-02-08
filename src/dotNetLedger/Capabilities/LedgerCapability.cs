namespace dotNetLedger.Capabilities
{
    public abstract class LedgerCapability
    {
        public object Service { get; }

        protected LedgerCapability(object service)
        {
            Service = service;
        }

    }

    public abstract class DefaultLedgerCapability : LedgerCapability
    {
        protected DefaultLedgerCapability(object service)
            : base(service) { }
    }
}
