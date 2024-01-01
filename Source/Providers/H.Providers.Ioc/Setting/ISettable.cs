namespace H.Providers.Ioc
{
    public interface ISettable
    {
        int Order { get; }
        string Name { get; }
        string GroupName { get; }
    }
}
