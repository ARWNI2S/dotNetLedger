namespace dotNetLedger.Bitcoin
{
    public class BitcoinLedgerTools
    {
        public static ILedgerProvider GetProvider() => new BitcoinLedgerProvider();
    }
}
