namespace RegLib.Elements
{
    public interface IRegElement
    {
        string Name { get; }
    }

    public interface IReadOnlyRegElement : IRegElement
    {

    }

    public interface IWritableRegElement : IRegElement
    {

    }
}
