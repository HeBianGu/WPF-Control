using H.Common.Interfaces;
using System.Windows;
using System.Windows.Media;

namespace H.Extensions.IocPresenter;

public class IocLayoutPresenterBase<T, Interface> : IocTitlePresenterBase<T, Interface>, ILayoutable where T : class, Interface, new()
{
    public double Height { get; set; } = double.NaN;
    public double Width { get; set; } = double.NaN;
    public double MinHeight { get; set; } = double.NaN;
    public double MinWidth { get; set; } = double.NaN;
    public double MaxHeight { get; set; } = double.NaN;
    public double MaxWidth { get; set; } = double.NaN;
    public Thickness Margin { get; set; } = new Thickness(10, 6, 10, 6);
    public Thickness Padding { get; set; } = new Thickness(10, 6, 10, 6);
    public HorizontalAlignment HorizontalAlignment { get; set; } = HorizontalAlignment.Stretch;
    public HorizontalAlignment HorizontalContentAlignment { get; set; }
    public VerticalAlignment VerticalAlignment { get; set; } = VerticalAlignment.Stretch;
    public VerticalAlignment VerticalContentAlignment { get; set; }
    public Brush Background { get; set; }
    public Brush BorderBrush { get; set; }
    public Thickness BorderThickness { get; set; }
    public double Opacity { get; set; } = 1;
    public bool IsEnabled { get; set; } = true;
}
