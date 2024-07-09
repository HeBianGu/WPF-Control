namespace H.Services.Common
{
    public interface ISettable
    {
        int Order { get; }
        string Name { get; }
        string GroupName { get; }
    }
}
