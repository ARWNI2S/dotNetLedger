using dotNetLedger.Transactions;

namespace dotNetLedger.Database.Transactions
{
    internal class DbTransaction : TransactionBase
    {
        public DbTransaction(Transaction transaction)
            : base(transaction)
        {
        }

        public DbTransaction(TxId id, Address from, Address to, TransactionType type)
            : base(id, from, to, type)
        {
        }
    }
}
