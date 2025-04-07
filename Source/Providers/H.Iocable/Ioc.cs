using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace H.Iocable;

public static class Ioc
{
    private static IServiceProvider _services = null;
    public static IServiceProvider Services => _services;
    private static IServiceCollection _serviceCollection = null;
    public static void Build(IServiceCollection serviceCollection)
    {
        _services = serviceCollection.BuildServiceProvider();
        _serviceCollection = serviceCollection;
    }

    public static T GetService<T>(bool throwIfNone = true)
    {
        if (_services == null)
            return throwIfNone ? throw new ArgumentNullException(typeof(T).FullName, $"请先初始化ApplicationBase.ConfigureServices后再获取Ioc接口") : default;
        return GetService<T>(typeof(T), throwIfNone);
    }

    public static T GetService<T>(Type type, bool throwIfNone = true)
    {
        string message = $"此接口为依赖注入接口，请先在ApplicationBase中注册<{typeof(T).Name}>服务";
        T r = (T)_services.GetService(type);
        if (r == null && throwIfNone)
        {
            System.Diagnostics.Debug.WriteLine(type);
            throw new ArgumentNullException(typeof(T).FullName, message);
        }
        return r;
    }

    public static bool Exist<T>()
    {
        return GetService<T>(throwIfNone: false) != null;
    }

    public static void ConfigureServices(Action<IServiceCollection> action)
    {
        if (_serviceCollection == null)
            return;
        action?.Invoke(_serviceCollection);
        //Build(_serviceCollection);
    }

    public static IEnumerable<ServiceDescriptor> FindAll(Func<ServiceDescriptor, bool> predicate = null)
    {
        foreach (ServiceDescriptor item in _serviceCollection)
        {
            if (predicate?.Invoke(item) != false)
                yield return item;
        }
    }

    //public static void BuildAll()
    //{
    //    foreach (ServiceDescriptor item in _serviceCollection)
    //    {
    //       Ioc.Services.GetService(item.ServiceType);
    //    }
    //}

    public static IEnumerable<T> GetAssignableFromServices<T>(Func<T, bool> predicate = null)
    {
        foreach (ServiceDescriptor item in _serviceCollection)
        {
            if (typeof(T).IsAssignableFrom(item.ServiceType))
            {
                if (item.ImplementationInstance == null)
                {
                    IEnumerable<object> instances = Services.GetServices(item.ServiceType);
                    foreach (object instance in instances)
                    {
                        if (instance is T it)
                        {
                            if (predicate?.Invoke(it) != false)
                                yield return it;
                        }
                    }

                }
                else
                {
                    if (item.ImplementationInstance is T t)
                    {
                        if (predicate?.Invoke(t) != false)
                            yield return (T)item.ImplementationInstance;
                    }
                }
            }
        }
    }

    //public static IEnumerable<T> GetImplementationInstances<T>(Func<T, bool> predicate = null)
    //{
    //    foreach (ServiceDescriptor item in _serviceCollection)
    //    {
    //        if (item.ImplementationInstance is T t)
    //        {
    //            if (predicate?.Invoke(t) != false)
    //                yield return (T)item.ImplementationInstance;
    //        }
    //    }
    //}
}

public abstract class Ioc<T, Interface> where T : class, Interface
{
    public static T Instance => Ioc.GetService<Interface>() as T;
}

public abstract class Ioc<Interface>
{
    public static Interface Instance => Ioc.GetService<Interface>(false);
}
