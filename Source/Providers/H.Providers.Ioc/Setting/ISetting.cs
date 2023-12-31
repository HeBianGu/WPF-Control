namespace H.Providers.Ioc
{
    public interface ISetting
    {
        int Order { get; }
        string Name { get; }
        string GroupName { get; }
    }
}
