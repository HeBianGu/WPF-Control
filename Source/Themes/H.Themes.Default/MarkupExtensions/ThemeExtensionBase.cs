// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Windows;
using System.Windows.Markup;

namespace H.Themes.Default
{
    public abstract class ThemeExtensionBase : MarkupExtension
    {
        private const string Format = "pack://application:,,,/H.Themes.Default;component/ResourceKeys/{0}/{1}.xaml";
        private const string Format3 = "pack://application:,,,/H.Themes.Default;component/ResourceKeys/{0}/{1}/{2}.xaml";

        protected virtual string GetFormat(string arg1, string arg2) => string.Format(Format, arg1, arg2);
        protected virtual Uri GetUri(string arg1, string arg2) => new Uri(GetFormat(arg1, arg2));
        protected virtual ResourceDictionary GetResource(string arg1, string arg2) => new ResourceDictionary() { Source = GetUri(arg1, arg2) };

        protected virtual string GetFormat(string arg1, string arg2, string arg3) => string.Format(Format3, arg1, arg2, arg3);
        protected virtual Uri GetUri(string arg1, string arg2, string arg3) => new Uri(GetFormat(arg1, arg2, arg3));
        protected virtual ResourceDictionary GetResource(string arg1, string arg2, string arg3) => new ResourceDictionary() { Source = GetUri(arg1, arg2, arg3) };
    }
}
