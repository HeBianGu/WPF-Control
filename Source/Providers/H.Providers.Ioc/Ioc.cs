using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;

namespace System
{
    public class Ioc
    {
        private static IServiceProvider _services = null;
        public static IServiceProvider Services => _services;
        public static void Build(IServiceProvider services)
        {
            _services = services;
        }

        public static T GetService<T>()
        {
            return (T)_services.GetService(typeof(T));
        }

        public static T GetService<T>(Type type)
        {
            return (T)_services.GetService(type);
        }
    }

    public abstract class IocInstance<Setting, Interface> where Setting : class, Interface, new()
    {
        public static Setting Instance => Ioc.Services.GetService(typeof(Interface)) as Setting;
    }

    public abstract class IocInstance<Interface>
    {
        public static Interface Instance => (Interface)Ioc.Services.GetService(typeof(Interface));
    }
}
