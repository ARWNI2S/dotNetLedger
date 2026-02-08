namespace dotNetLedger.Transactions
{
    public readonly record struct TxId(string Value)
    {
        public override string ToString() => Value;
    }
}
