// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Controls.Chart2D
{
    public class ChartBrushKeys
    {
        public static readonly Color[] ChartColors = new[]
        {
                (Color)ColorConverter.ConvertFromString("#5470C6"),
                (Color)ColorConverter.ConvertFromString("#91CC75"),
                (Color)ColorConverter.ConvertFromString("#FAC858"),
                (Color)ColorConverter.ConvertFromString("#EE6666"),
                (Color)ColorConverter.ConvertFromString("#73C0DE"),
                (Color)ColorConverter.ConvertFromString("#3BA272"),
                (Color)ColorConverter.ConvertFromString("#FC8452"),
                (Color)ColorConverter.ConvertFromString("#9A60B4"),
                (Color)ColorConverter.ConvertFromString("#EA7CCC"),
            };
        public static ComponentResourceKey Palette0 => new ComponentResourceKey(typeof(ChartBrushKeys), "S.Chart.Palette.0");
        public static ComponentResourceKey Palette1=> new ComponentResourceKey(typeof(ChartBrushKeys), "S.Chart.Palette.1");
        public static ComponentResourceKey Palette2 => new ComponentResourceKey(typeof(ChartBrushKeys), "S.Chart.Palette.2");
        public static ComponentResourceKey Palette3 => new ComponentResourceKey(typeof(ChartBrushKeys), "S.Chart.Palette.3");
        public static ComponentResourceKey Palette4 => new ComponentResourceKey(typeof(ChartBrushKeys), "S.Chart.Palette.4");
        public static ComponentResourceKey Palette5 => new ComponentResourceKey(typeof(ChartBrushKeys), "S.Chart.Palette.5");
        public static ComponentResourceKey Palette6 => new ComponentResourceKey(typeof(ChartBrushKeys), "S.Chart.Palette.6");
        public static ComponentResourceKey Palette7=> new ComponentResourceKey(typeof(ChartBrushKeys), "S.Chart.Palette.7");
        public static ComponentResourceKey Palette8=> new ComponentResourceKey(typeof(ChartBrushKeys), "S.Chart.Palette.8");

        public static Color GetPaletteColor(int index)
        {
            if (ChartColors.Length == 0) return Colors.Transparent;
            var i = index % ChartColors.Length;
            if (i < 0) i += ChartColors.Length;
            return ChartColors[i];
        }

        public static SolidColorBrush GetPaletteBrush(int index)
        {
            var brush = new SolidColorBrush(GetPaletteColor(index));
            brush.Freeze();
            return brush;
        }
    }

}
