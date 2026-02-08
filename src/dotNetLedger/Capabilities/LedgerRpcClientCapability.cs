namespace dotNetLedger.Capabilities
{
    internal class LedgerRpcClientCapability : DefaultLedgerCapability, ILedgerCapability<ILedgerRpcClientService>
    {
        public LedgerRpcClientCapability(ILedgerRpcClientService service) : base(service)
        {
        }

        ILedgerRpcClientService ILedgerCapability<ILedgerRpcClientService>.Service => (ILedgerRpcClientService)Service;
    }
}
