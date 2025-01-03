namespace H.Services.Common
{
    public interface ILayoutable
    {
        Brush Background { get; set; }
        Brush BorderBrush { get; set; }
        Thickness BorderThickness { get; set; }
        double Height { get; set; }
        HorizontalAlignment HorizontalAlignment { get; set; }
        HorizontalAlignment HorizontalContentAlignment { get; set; }
        bool IsEnabled { get; set; }
        Thickness Margin { get; set; }
        double MinHeight { get; set; }
        double MinWidth { get; set; }
        double Opacity { get; set; }
        Thickness Padding { get; set; }
        VerticalAlignment VerticalAlignment { get; set; }
        VerticalAlignment VerticalContentAlignment { get; set; }
        double Width { get; set; }
    }

    public static class LayoutableExtension
    {
        public static void CopyTo(this ILayoutable from, ILayoutable to)
        {
            to.HorizontalAlignment = from.HorizontalAlignment;
            to.VerticalAlignment = from.VerticalAlignment;
            to.HorizontalContentAlignment = from.HorizontalContentAlignment;
            to.VerticalContentAlignment = from.VerticalContentAlignment;
            to.Height = from.Height;
            to.Width = from.Width;
            to.Padding = from.Padding;
            to.Margin = from.Margin;
            to.MinWidth = from.MinWidth;
            to.MinHeight = from.MinHeight;
            to.BorderBrush = from.BorderBrush;
            to.BorderThickness = from.BorderThickness;
            to.Background = from.Background;
            to.IsEnabled = from.IsEnabled;
            to.Opacity = from.Opacity;
        }
    }
}
