namespace dotNetLedger.Currency
{
    internal readonly record struct LedgerCurrency(string Name, string Symbol, uint Decimals)
        : ICurrency;
}
