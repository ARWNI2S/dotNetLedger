namespace dotNetLedger.Transactions
{
    public readonly record struct Transaction(TxId Id, Address From, Address To, TransactionType Type);

    public abstract class TransactionBase : IEquatable<TransactionBase>, IEquatable<Transaction>
    {
        protected TransactionBase(Transaction transaction)
        {
            Id = transaction.Id;
            From = transaction.From;
            To = transaction.To;
            Type = transaction.Type;
        }

        protected TransactionBase(TxId id, Address from, Address to, TransactionType type)
        {
            Id = id;
            From = from;
            To = to;
            Type = type;
        }

        public TxId Id { get; }
        public Address From { get; }
        public Address To { get; }
        public TransactionType Type { get; }

        // TODO: Si realmente no hay implementación por defecto, mejor abstract que "throw"?
        public virtual void Execute() => throw new NotImplementedException();
        public virtual bool CheckSigned() => throw new NotImplementedException();
        public virtual ReadOnlyMemory<byte> TrySignTransaction(out bool isSigned) => throw new NotImplementedException();
        public virtual void Validate() => throw new NotImplementedException();

        public ReadOnlyMemory<byte> GetBytes()
        {
            // TODO: esto debería delegar en un serializer determinista.
            return ReadOnlyMemory<byte>.Empty;
        }

        // --- Igualdad ---

        public bool Equals(Transaction other)
            => Id.Equals(other.Id)
            && From.Equals(other.From)
            && To.Equals(other.To)
            && Type.Equals(other.Type)
            && EqualsAdditional(other);

        public bool Equals(TransactionBase? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            // Opción A: evita igualdad entre derivadas distintas
            if (GetType() != other.GetType()) return false;

            return Id.Equals(other.Id)
                && From.Equals(other.From)
                && To.Equals(other.To)
                && Type.Equals(other.Type)
                && EqualsAdditional(other);
        }

        public override bool Equals(object? obj)
            => obj is TransactionBase tb ? Equals(tb)
             : obj is Transaction t ? Equals(t)
             : false;

        public override int GetHashCode()
        {
            var hash = new HashCode();

            // Opción A: el tipo runtime entra en el hash para evitar colisiones entre derivadas
            hash.Add(GetType());
            hash.Add(Id);
            hash.Add(From);
            hash.Add(To);
            hash.Add(Type);

            AddHashAdditional(ref hash);

            return hash.ToHashCode();
        }

        public static implicit operator Transaction(TransactionBase tx)
            => new(tx.Id, tx.From, tx.To, tx.Type);
        //public static explicit operator Transaction(TransactionBase tx) => tx.Core;

        public static bool operator ==(TransactionBase? left, TransactionBase? right)
            => left is null ? right is null : left.Equals(right);

        public static bool operator !=(TransactionBase? left, TransactionBase? right)
            => !(left == right);

        // --- Extensión para derivadas ---
        // Si una transacción derivada tiene más campos semánticos, sobreescribe esto.

        protected virtual bool EqualsAdditional(Transaction other) => true;

        protected virtual bool EqualsAdditional(TransactionBase other) => true;

        protected virtual void AddHashAdditional(ref HashCode hash) { }
    }
}
