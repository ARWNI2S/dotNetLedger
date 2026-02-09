
namespace dotNetLedger.Transactions
{
    internal class TransactionStore
    {
        private readonly Dictionary<Transaction, TransactionBase> _transactions = [];

        internal Transaction Add<T>(T transaction) where T : TransactionBase
        {
            ArgumentNullException.ThrowIfNull(transaction);

            if (_transactions.ContainsValue(transaction))
                return _transactions.First(kvp => kvp.Value == transaction).Key;

            Transaction id = transaction;
            _transactions.Add(id, transaction);
            return id;
        }
    }
}