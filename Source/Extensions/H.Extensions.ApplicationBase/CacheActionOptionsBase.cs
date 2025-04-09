namespace H.Extensions.ApplicationBase;

public abstract class CacheActionOptionsBase
{
    protected List<object> CacheActionOptions { get; } = new List<object>();
    public T GetConfigOptions<T>()
    {
        return this.CacheActionOptions.OfType<T>().FirstOrDefault();
    }

    protected void ConfigOptions<T>(Action<T> action)
    {
        this.CacheActionOptions.Add(action);
    }
}
