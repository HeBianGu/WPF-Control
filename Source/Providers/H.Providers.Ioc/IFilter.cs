namespace H.Providers.Ioc
{
    public interface IFilter
    {
        bool IsMatch(object obj);
    }

    public interface IDisplayFilter: IFilter
    {
        string DisplayName { get; }
    }
}
