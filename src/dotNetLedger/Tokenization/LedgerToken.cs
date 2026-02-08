namespace dotNetLedger.Tokenization
{
    internal readonly record struct LedgerToken(Address Address, string Name, string Symbol, uint Decimals)
        : IToken;
}
