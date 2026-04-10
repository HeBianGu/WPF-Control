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

                // Additional palette colors
                (Color)ColorConverter.ConvertFromString("#2F4554"),
                (Color)ColorConverter.ConvertFromString("#61A0A8"),
                (Color)ColorConverter.ConvertFromString("#D48265"),
                (Color)ColorConverter.ConvertFromString("#749F83"),
                (Color)ColorConverter.ConvertFromString("#CA8622"),
                (Color)ColorConverter.ConvertFromString("#BDA29A"),
                (Color)ColorConverter.ConvertFromString("#6E7074"),
                (Color)ColorConverter.ConvertFromString("#546570"),
                (Color)ColorConverter.ConvertFromString("#C4CCD3"),

                (Color)ColorConverter.ConvertFromString("#4E79A7"),
                (Color)ColorConverter.ConvertFromString("#F28E2B"),
                (Color)ColorConverter.ConvertFromString("#E15759"),
                (Color)ColorConverter.ConvertFromString("#76B7B2"),
                (Color)ColorConverter.ConvertFromString("#59A14F"),
                (Color)ColorConverter.ConvertFromString("#EDC948"),
                (Color)ColorConverter.ConvertFromString("#B07AA1"),
                (Color)ColorConverter.ConvertFromString("#FF9DA7"),
                (Color)ColorConverter.ConvertFromString("#9C755F"),
                (Color)ColorConverter.ConvertFromString("#BAB0AC"),

                (Color)ColorConverter.ConvertFromString("#1F77B4"),
                (Color)ColorConverter.ConvertFromString("#FF7F0E"),
                (Color)ColorConverter.ConvertFromString("#2CA02C"),
                (Color)ColorConverter.ConvertFromString("#D62728"),
                (Color)ColorConverter.ConvertFromString("#9467BD"),
                (Color)ColorConverter.ConvertFromString("#8C564B"),
                (Color)ColorConverter.ConvertFromString("#E377C2"),
                (Color)ColorConverter.ConvertFromString("#7F7F7F"),
                (Color)ColorConverter.ConvertFromString("#BCBD22"),
                (Color)ColorConverter.ConvertFromString("#17BECF"),
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
