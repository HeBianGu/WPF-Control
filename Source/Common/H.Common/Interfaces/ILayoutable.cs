namespace H.Common.Interfaces;

/// <summary>
/// 定义布局相关的属性和方法。
/// </summary>
public interface ILayoutable
{
    /// <summary>
    /// 获取或设置背景画刷。
    /// </summary>
    Brush Background { get; set; }

    /// <summary>
    /// 获取或设置边框画刷。
    /// </summary>
    Brush BorderBrush { get; set; }

    /// <summary>
    /// 获取或设置边框厚度。
    /// </summary>
    Thickness BorderThickness { get; set; }

    /// <summary>
    /// 获取或设置高度。
    /// </summary>
    double Height { get; set; }

    /// <summary>
    /// 获取或设置水平对齐方式。
    /// </summary>
    HorizontalAlignment HorizontalAlignment { get; set; }

    /// <summary>
    /// 获取或设置水平内容对齐方式。
    /// </summary>
    HorizontalAlignment HorizontalContentAlignment { get; set; }

    /// <summary>
    /// 获取或设置是否启用。
    /// </summary>
    bool IsEnabled { get; set; }

    /// <summary>
    /// 获取或设置外边距。
    /// </summary>
    Thickness Margin { get; set; }

    /// <summary>
    /// 获取或设置最小高度。
    /// </summary>
    double MinHeight { get; set; }

    /// <summary>
    /// 获取或设置最小高度。
    /// </summary>
    double MaxHeight { get; set; }

    /// <summary>
    /// 获取或设置最小宽度。
    /// </summary>
    double MinWidth { get; set; }


    /// <summary>
    /// 获取或设置最小宽度。
    /// </summary>
    double MaxWidth { get; set; }

    /// <summary>
    /// 获取或设置不透明度。
    /// </summary>
    double Opacity { get; set; }

    /// <summary>
    /// 获取或设置内边距。
    /// </summary>
    Thickness Padding { get; set; }

    /// <summary>
    /// 获取或设置垂直对齐方式。
    /// </summary>
    VerticalAlignment VerticalAlignment { get; set; }

    /// <summary>
    /// 获取或设置垂直内容对齐方式。
    /// </summary>
    VerticalAlignment VerticalContentAlignment { get; set; }

    /// <summary>
    /// 获取或设置宽度。
    /// </summary>
    double Width { get; set; }
}

public static class LayoutableExtension
{
    /// <summary>
    /// 将布局属性从一个对象复制到另一个对象。
    /// </summary>
    /// <param name="from">要复制属性的源对象。</param>
    /// <param name="to">要复制属性的目标对象。</param>
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
        to.MaxHeight = from.MaxHeight;
        to.MaxWidth = from.MaxWidth;
        to.BorderBrush = from.BorderBrush;
        to.BorderThickness = from.BorderThickness;
        to.Background = from.Background;
        to.IsEnabled = from.IsEnabled;
        to.Opacity = from.Opacity;
    }
}
