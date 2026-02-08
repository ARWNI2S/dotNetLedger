
namespace dotNetLedger.Solana
{
    public class SolanaLedgerTools
    {
        public static ILedgerProvider GetProvider(SolanaNetwork networkType) => new SolanaLedgerProvider(networkType);
    }
}
