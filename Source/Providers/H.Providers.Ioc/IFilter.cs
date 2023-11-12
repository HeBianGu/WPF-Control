namespace H.Providers.Ioc
{
    public interface IFilter
    {
        bool IsMatch(object obj);
    }
}
