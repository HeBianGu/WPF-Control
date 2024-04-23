// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Windows;

namespace H.Themes.Default
{
    public static class LayoutKeys
    {
        public static ComponentResourceKey RowHeight => new ComponentResourceKey(typeof(LayoutKeys), "S.Layout.RowHeight");
        public static ComponentResourceKey ItemHeight => new ComponentResourceKey(typeof(LayoutKeys), "S.Layout.ItemHeight");
        public static ComponentResourceKey WindowCaptionHeight => new ComponentResourceKey(typeof(LayoutKeys), "S.Layout.WindowCaptionHeight");
        public static ComponentResourceKey CornerRadius => new ComponentResourceKey(typeof(LayoutKeys), "S.Layout.CornerRadius");
    }

    //public class SystemParametersExtension : MarkupExtension
    //{
    //    public double Value { get; set; }
    //    public override object ProvideValue(IServiceProvider serviceProvider)
    //    {
    //        return this.Value.ToString();
    //    }
    //}
}
