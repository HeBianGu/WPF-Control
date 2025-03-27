using Microsoft.Extensions.DependencyInjection;

namespace H.Services.Common;

public static class DbIoc
{
    private static ServiceCollection _sc;
    private static IServiceProvider _services;
    public static IServiceProvider Services => _services;
    public static void ConfigureServices(Action<IServiceCollection> action)
    {
        _sc = new ServiceCollection();
        action?.Invoke(_sc);
        _services = _sc.BuildServiceProvider();
    }

    public static void Rebuild()
    {
        _services = _sc.BuildServiceProvider();
    }

    public static T GetService<T>(bool throwIfNone = true)
    {
        T r = (T)_services?.GetService(typeof(T));
        return r == null && throwIfNone ? Ioc.GetService<T>(throwIfNone) : r;
    }
}
