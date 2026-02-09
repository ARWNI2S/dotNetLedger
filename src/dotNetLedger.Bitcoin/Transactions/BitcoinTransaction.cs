using dotNetLedger.Transactions;

namespace dotNetLedger.Bitcoin.Transactions
{
    internal class BitcoinTransaction : TransactionBase
    {
        public BitcoinTransaction(Transaction transaction)
            : base(transaction)
        {
        }

        public BitcoinTransaction(TxId id, Address from, Address to, TransactionType type)
            : base(id, from, to, type)
        {
        }

        public override void Execute()
        {
            throw new NotImplementedException();
        }

        public override bool CheckSigned()
        {
            throw new NotImplementedException();
        }

        public override ReadOnlyMemory<byte> TrySignTransaction(out bool isSigned)
        {
            throw new NotImplementedException();
        }

        public override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
