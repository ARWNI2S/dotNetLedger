namespace dotNetLedger
{
    public interface ILedger
    {
        string Name { get; }

        string? Description { get; }

        int NetworkId { get; }

        int? ChainId { get; }

        string? Version { get; }

        ICurrency Currency { get; }
    }
}
