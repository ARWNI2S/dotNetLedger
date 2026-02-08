using dotNetLedger.Commons;
using dotNetLedger.Ethereum.Commons;

namespace dotNetLedger.Ethereum
{
    public class EthereumLedgerProvider : ILedgerProvider
    {
        public EthereumAdapter LedgerAdapter { get; } = default!;

        ILedgerCommonAdapter ILedgerProvider.LedgerAdapter => LedgerAdapter;

        internal EthereumLedgerProvider()
        {
        }

    }
}
