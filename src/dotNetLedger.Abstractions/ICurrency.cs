namespace dotNetLedger
{
    public interface ICurrency
    {
        string Name { get; }

        string Symbol { get; }

        int DecimalPlaces { get; }

        ILedger Ledger { get; }
    }
}
