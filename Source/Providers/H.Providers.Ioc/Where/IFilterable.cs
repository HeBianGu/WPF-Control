namespace H.Providers.Ioc
{
    public interface IFilterable
    {
        bool IsMatch(object obj);
    }
}
