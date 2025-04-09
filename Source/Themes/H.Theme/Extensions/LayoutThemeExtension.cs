// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Windows.Markup;

namespace H.Themes.Extensions;

public class LayoutThemeExtension : MarkupExtension
{
    public LayoutThemeType Type { get; set; }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return this.Type.GetLayoutResource();
    }
}
