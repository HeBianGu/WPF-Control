using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace System
{
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
            {
                if (throwIfNone)
                    throw new ArgumentNullException($"请先注册使用ApplicationBase注册<IServiceCollection>接口");
                else
                    return default(T);
            }
            T r = (T)_services.GetService(typeof(T));
            if (r == null && throwIfNone)
            {
                System.Diagnostics.Debug.WriteLine(typeof(T));
                throw new ArgumentNullException($"请先注册<{typeof(T)}>接口");
            }
            return r;
        }

        public static T GetService<T>(Type type, bool throwIfNone = true)
        {
            T r = (T)_services.GetService(type);
            if (r == null && throwIfNone)
            {
                System.Diagnostics.Debug.WriteLine(type);
                throw new ArgumentNullException($"请先注册<{type}>接口");
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
                        var instances = Ioc.Services.GetServices(item.ServiceType);
                        foreach (var instance in instances)
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

    public abstract class Ioc<T, Interface> where T : class, Interface, new()
    {
        public static T Instance => Ioc.GetService<Interface>() as T;
    }

    public abstract class Ioc<Interface>
    {
        public static Interface Instance
        {
            get
            {
                object r = Ioc.Services?.GetService(typeof(Interface));
                return r == null ? default : (Interface)r;
            }
        }
    }
}
