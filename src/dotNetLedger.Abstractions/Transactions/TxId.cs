namespace dotNetLedger.Transactions
{
    public readonly struct TxId(string value)
    {
        public string Value { get; } = value;

        public override string ToString() => Value;
    }
}
