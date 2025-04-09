using System.Windows.Markup;

namespace H.Extensions.FontIcon;

public class GetIconSegoesExtension : MarkupExtension
{
    public int From { get; set; } = 0xE700;
    public int To { get; set; } = 0xE900;
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        var ds = IconSegoeProvider.GetIconSegoes(this.From, this.To);
        return new IconSegoes(ds);
    }
}
