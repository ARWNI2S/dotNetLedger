namespace dotNetLedger.Currency
{
    public interface ICurrency
    {
        string Name { get; }

        string Symbol { get; }

        uint Decimals { get; }
    }
}
