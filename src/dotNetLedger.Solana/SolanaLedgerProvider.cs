using dotNetLedger.Commons;
using dotNetLedger.Solana.Commons;

namespace dotNetLedger.Solana
{
    public class SolanaLedgerProvider : ILedgerProvider
    {
        public SolanaAdapter LedgerAdapter { get; } = default!;

        ILedgerCommonAdapter ILedgerProvider.LedgerAdapter => LedgerAdapter;

        internal SolanaLedgerProvider(SolanaNetwork networkType)
        {
        }
    }
}
