using dotNetLedger.Bitcoin.Commons;
using dotNetLedger.Commons;

namespace dotNetLedger.Bitcoin
{
    public class BitcoinLedgerProvider : ILedgerProvider
    {
        public BitcoinAdapter LedgerAdapter { get; } = default!;

        ILedgerAdapter ILedgerProvider.LedgerAdapter => LedgerAdapter;

        internal BitcoinLedgerProvider()
        {
        }

    }
}
