namespace dotNetLedger
{
    public readonly record struct Address(string Value)
    {
        public override string ToString() => Value;
    }
}
