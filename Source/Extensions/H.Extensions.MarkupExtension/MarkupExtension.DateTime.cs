using System.Windows.Markup;

namespace H.Extensions.MarkupExtension;

[MarkupExtensionReturnType(typeof(DateTime))]
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
