using dotNetLedger.Transactions;

namespace dotNetLedger.Ethereum.Transactions
{
    internal class EthereumTransaction : TransactionBase
    {
        public EthereumTransaction(Transaction transaction)
            : base(transaction)
        {
        }

        public EthereumTransaction(TxId id, Address from, Address to, TransactionType type)
            : base(id, from, to, type)
        {
        }

    }
}
