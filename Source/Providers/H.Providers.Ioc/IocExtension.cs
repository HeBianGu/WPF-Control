using System;
using System.Windows.Markup;

namespace H.Providers.Ioc
{
    //public abstract class IocInstance<Setting, Interface> where Setting : class, Interface, new()
    //{
    //    public static Setting Instance => Ioc.Services.GetService(typeof(Interface)) as Setting;
    //}

    public class IocExtension : MarkupExtension
    {
        public Type Type { get; set; }
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            //var sss = serviceProvider.GetService(this.Type);
            return System.Ioc.GetService<object>(this.Type);
        }
    }
}
