namespace dotNetLedger
{
    public readonly struct Address(string Value)
    {
        public override string ToString() => Value;
    }

    public interface IAddressable
    {
        Address Address { get; }
    }

    //public interface IRouted
    //{
    //    string? Route { get; }
    //}

}
