using System;
using System.Windows.Markup;

namespace H.Providers.Ioc
{
    public class IocExtension : MarkupExtension
    {
        public Type Type { get; set; }
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return System.Ioc.GetService<object>(this.Type);
        }
    }
}
