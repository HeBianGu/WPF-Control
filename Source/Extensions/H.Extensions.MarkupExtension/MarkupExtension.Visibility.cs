// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Windows;
using System.Windows.Markup;

namespace H.Extensions.MarkupExtension;

[MarkupExtensionReturnType(typeof(Visibility))]
public class GetVisibilityExtension : GetValueExtensionBase
{
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        if (Enum.TryParse<Visibility>(this.Value, out Visibility result))
        {
            return result;
        }
        return Visibility.Visible;
    }
}
