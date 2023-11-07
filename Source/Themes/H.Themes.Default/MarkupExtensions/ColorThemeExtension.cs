// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Windows.Markup;

namespace H.Themes.Default
{
    public class ColorThemeExtension : MarkupExtension
    {
        public ColorThemeType Type { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this.Type.GetResource();
        }
    }
}
