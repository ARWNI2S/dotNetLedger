namespace dotNetLedger.Transactions
{
    public interface ITransactionFactory<T> where T : TransactionBase
    {
        T CreateTransaction(params object[] parameters);
    }

    public class TransactionFactory<T> where T : TransactionBase
    {
        private readonly ITransactionFactory<T> _factory;
        private readonly TransactionStore _transactionStore;

        internal TransactionFactory(ITransactionFactory<T> factory,
            TransactionStore transactionStore)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
            _transactionStore = transactionStore;
        }

        public Transaction CreateTransaction(params object[] parameters)
        {
            return _transactionStore.Add(_factory.CreateTransaction(parameters));
        }
    }
}
