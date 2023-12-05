using System;

namespace H.Extensions.MarkupExtension
{
    public class GetDateTimeExtension : GetValueExtensionBase
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (DateTime.TryParse(this.Value, out DateTime result))
            {
                return result;
            }
            return DateTime.MinValue;
        }
    }
}
