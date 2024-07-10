using System;
using System.Windows.Markup;

namespace H.Ioc.Iocs
{
    public class IocExtension : MarkupExtension
    {
        public Type Type { get; set; }
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Ioc.GetService<object>(this.Type);
        }
    }
}
