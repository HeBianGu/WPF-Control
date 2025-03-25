namespace H.Common.Interfaces;

/// <summary>
/// 表示可设计的接口。
/// </summary>
public interface IDesignable : ILayoutable
{
    /// <summary>
    /// 获取或设置列索引。
    /// </summary>
    int Column { get; set; }

    /// <summary>
    /// 获取或设置列跨度。
    /// </summary>
    int ColumnSpan { get; set; }

    /// <summary>
    /// 获取或设置鼠标是否悬停在上面。
    /// </summary>
    bool IsMouseOver { get; set; }

    /// <summary>
    /// 获取或设置是否被选中。
    /// </summary>
    bool IsSelected { get; set; }

    /// <summary>
    /// 获取或设置是否可见。
    /// </summary>
    bool IsVisible { get; set; }

    /// <summary>
    /// 获取或设置行索引。
    /// </summary>
    int Row { get; set; }

    /// <summary>
    /// 获取或设置行跨度。
    /// </summary>
    int RowSpan { get; set; }

    /// <summary>
    /// 创建当前对象的副本。
    /// </summary>
    /// <returns>当前对象的副本。</returns>
    object Clone();
}
