namespace dotNetLedger.Transactions
{
    public readonly record struct Transaction(TxId Id, Address From, Address To, TransactionType Type);
}
