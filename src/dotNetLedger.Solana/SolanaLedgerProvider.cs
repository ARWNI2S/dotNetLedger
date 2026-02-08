using dotNetLedger.Commons;
using dotNetLedger.Solana.Commons;

namespace dotNetLedger.Solana
{
    public class SolanaLedgerProvider : ILedgerProvider
    {
        public SolanaLedgerAdapter LedgerAdapter { get; } = default!;

        ILedgerAdapter ILedgerProvider.LedgerAdapter => LedgerAdapter;

        internal SolanaLedgerProvider(SolanaNetwork networkType)
        {
        }
    }
}
