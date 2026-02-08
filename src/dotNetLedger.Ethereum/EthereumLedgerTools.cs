namespace dotNetLedger.Ethereum
{
    public class EthereumLedgerTools
    {
        public static ILedgerProvider GetProvider() => new EthereumLedgerProvider();
    }
}
