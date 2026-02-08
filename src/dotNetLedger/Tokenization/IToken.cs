using dotNetLedger.Currency;

namespace dotNetLedger.Tokenization
{
    public interface IToken : ICurrency
    {
        Address Address { get; }
    }
}
