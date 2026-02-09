using dotNetLedger.Transactions;

namespace dotNetLedger.Solana.Transactions
{
    internal class SolanaTransaction : TransactionBase
    {
        public SolanaTransaction(Transaction transaction)
            : base(transaction)
        {
        }

        public SolanaTransaction(TxId id, Address from, Address to, TransactionType type)
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
