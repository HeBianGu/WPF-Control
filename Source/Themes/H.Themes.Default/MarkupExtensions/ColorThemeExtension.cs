// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Windows;

namespace H.Themes.Default
{
    public class ColorThemeExtension : ThemeExtensionBase
    {
        public ColorThemeType Type { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return GetColorResource();
        }

        protected virtual ResourceDictionary GetColorResource()
        {
            if (Type == ColorThemeType.Default)
                return this.GetResource("Colors", Type.ToString());
            else if (Type == ColorThemeType.Dark)
                return this.GetResource("Colors", Type.ToString());
            else if (Type == ColorThemeType.DarkGray)
                return this.GetResource("Colors", "Darks", "Gray");
            else
                return this.GetResource("Colors", "Default");

        }
    }
}
