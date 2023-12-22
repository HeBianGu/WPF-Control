using Microsoft.Extensions.DependencyInjection;

namespace System
{
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
            T r = (T)_services.GetService(typeof(T));
            if (r == null && throwIfNone)
            {
                Ioc.GetService<T>(throwIfNone);
                System.Diagnostics.Debug.WriteLine(typeof(T));
                throw new ArgumentNullException($"请先注册<{typeof(T)}>接口");
            }
            return r;
        }
    }
}
