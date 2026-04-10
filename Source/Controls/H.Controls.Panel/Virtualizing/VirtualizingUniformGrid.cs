// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;

namespace H.Controls.Panel.Virtualizing;
        
/// <summary>
/// 虚拟化的等分网格面板：将 Items 按 Rows x Columns 均匀排列。
/// 通过 IItemContainerGenerator 仅生成可见范围内的容器，减少 UI 开销。
/// 实现 IScrollInfo，用于与 ScrollViewer 协作完成滚动与视口信息更新。
/// </summary>
public class VirtualizingUniformGrid : VirtualizingPanel, IScrollInfo
{
    // 整体内容范围（逻辑尺寸），由可见单元格尺寸与项目数量计算得到，用于滚动边界。
    private Size _extent;

    // 视口尺寸（可用布局大小），通常来自 ScrollViewer 的可视区域。
    private Size _viewport;

    // 当前滚动偏移（相对于内容左上角的偏移量，单位为 DIP）。
    private Point _offset;

    // 控制是否允许横向/纵向滚动（由外部 ScrollViewer 设置）。
    private bool _canHorizontallyScroll;
    private bool _canVerticallyScroll;

    // 鼠标滚轮与按行滚动基础步长（像素）。
    private int _scrollLength = 25;

    /// <summary>
    /// 列数，必须 >= 1；用于计算子元素的宽度与网格定位。
    /// </summary>
    public int Columns
    {
        get { return (int)this.GetValue(ColumnsProperty); }
        set { this.SetValue(ColumnsProperty, value); }
    }

    // 当 Columns 变化时影响测量与排列，触发重布局。
    public static readonly DependencyProperty ColumnsProperty = DependencyProperty.Register(nameof(Columns), typeof(int), typeof(VirtualizingUniformGrid),
        new FrameworkPropertyMetadata(1, FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsMeasure));

    /// <summary>
    /// 行数，必须 >= 1；用于计算子元素的高度与网格定位。
    /// </summary>
    public int Rows
    {
        get { return (int)this.GetValue(RowsProperty); }
        set { this.SetValue(RowsProperty, value); }
    }

    // 当 Rows 变化时影响测量与排列，触发重布局。
    public static readonly DependencyProperty RowsProperty = DependencyProperty.Register(nameof(Rows), typeof(int), typeof(VirtualizingUniformGrid),
        new FrameworkPropertyMetadata(1, FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsMeasure));

    /// <summary>
    /// 滚动方向：
    /// Horizontal：按“页”为单位横向滚动（每页为 Rows x Columns 个元素）。
    /// Vertical：纵向连续滚动。
    /// </summary>
    public Orientation Orientation
    {
        get { return (Orientation)this.GetValue(OrientationProperty); }
        set { this.SetValue(OrientationProperty, value); }
    }

    public static readonly DependencyProperty OrientationProperty = DependencyProperty.RegisterAttached(nameof(Orientation), typeof(Orientation), typeof(VirtualizingUniformGrid),
        new FrameworkPropertyMetadata(Orientation.Vertical, FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsMeasure));

    /// <summary>
    /// Items 发生变化时的处理：
    /// 对 Remove/Replace/Move，移除对应已实现的子元素，保持可视树与生成器一致。
    /// Add 情况下由 Measure 阶段的生成器自动处理。
    /// </summary>
    protected override void OnItemsChanged(object sender, ItemsChangedEventArgs args)
    {
        switch (args.Action)
        {
            case NotifyCollectionChangedAction.Remove:
            case NotifyCollectionChangedAction.Replace:
            case NotifyCollectionChangedAction.Move:
                RemoveInternalChildRange(args.Position.Index, args.ItemUICount);
                break;
        }
    }

    /// <summary>
    /// 测量阶段核心流程：
    /// 1. 更新滚动信息（_extent/_viewport）。
    /// 2. 计算当前可见的项目索引范围。
    /// 3. 通过生成器仅生成并测量该范围内的容器。
    /// 4. 清理不在范围内的已实现容器，降低内存与布局开销。
    /// </summary>
    protected override Size MeasureOverride(Size availableSize)
    {
        UpdateScrollInfo(availableSize);

        int firstVisibleItemIndex, lastVisibleItemIndex;
        GetVisibleRange(out firstVisibleItemIndex, out lastVisibleItemIndex);

        UIElementCollection children = this.InternalChildren;
        IItemContainerGenerator generator = this.ItemContainerGenerator;
        // 从可见范围起点位置开始生成
        GeneratorPosition startPos = generator.GeneratorPositionFromIndex(firstVisibleItemIndex);
        int childIndex = (startPos.Offset == 0) ? startPos.Index : startPos.Index + 1;

        using (generator.StartAt(startPos, GeneratorDirection.Forward, true))
        {
            for (int itemIndex = firstVisibleItemIndex; itemIndex <= lastVisibleItemIndex; ++itemIndex, ++childIndex)
            {
                bool newlyRealized;
                // 生成下一个子元素（可能是新建或重用）
                UIElement child = generator.GenerateNext(out newlyRealized) as UIElement;
                childIndex = Math.Max(0, childIndex);

                if (newlyRealized)
                {
                    // 新生成的容器插入到正确的可视索引位置，保持与生成器同步
                    if (childIndex >= children.Count)
                    {
                        base.AddInternalChild(child);
                    }
                    else
                    {
                        base.InsertInternalChild(childIndex, child);
                    }

                    generator.PrepareItemContainer(child);
                }
                else
                {
                    // 防御性检查：生成器返回的容器与可视树索引一致
                    Trace.Assert(child == children[childIndex], "Wrong child was generated");
                }

                // 使用单元格尺寸对每个子容器进行测量
                child.Measure(GetChildSize(availableSize));
            }
        }

        // 清理不在可见范围内的已实现容器
        CleanUpItems(firstVisibleItemIndex, lastVisibleItemIndex);

        // 当高度为无界（例如未置于 ScrollViewer 中）时返回一个默认尺寸
        if (availableSize.Height.Equals(double.PositiveInfinity))
        {
            Debug.WriteLine(_extent);
            return new Size(200, 200);
        }

        return availableSize;
    }

    /// <summary>
    /// 排列阶段：
    /// 按索引计算行列，结合滚动偏移与方向，将每个已实现子元素放到对应单元格位置。
    /// </summary>
    protected override Size ArrangeOverride(Size finalSize)
    {
        IItemContainerGenerator generator = this.ItemContainerGenerator;

        UpdateScrollInfo(finalSize);

        for (int i = 0; i < this.Children.Count; i++)
        {
            UIElement child = this.Children[i];
            int itemIndex = generator.IndexFromGeneratorPosition(new GeneratorPosition(i, 0));
            ArrangeChild(itemIndex, child, finalSize);
        }

        return finalSize;
    }

    /// <summary>
    /// 清理指定可见范围之外的子容器，避免占用多余的可视元素与布局开销。
    /// </summary>
    private void CleanUpItems(int minDesiredGenerated, int maxDesiredGenerated)
    {
        UIElementCollection children = this.InternalChildren;
        IItemContainerGenerator generator = this.ItemContainerGenerator;

        for (int i = children.Count - 1; i >= 0; i--)
        {
            GeneratorPosition childGeneratorPos = new GeneratorPosition(i, 0);
            int itemIndex = generator.IndexFromGeneratorPosition(childGeneratorPos);
            if (itemIndex < minDesiredGenerated || itemIndex > maxDesiredGenerated)
            {
                generator.Remove(childGeneratorPos, 1);
                RemoveInternalChildRange(i, 1);
            }
        }
    }

    /// <summary>
    /// 计算整体内容范围（_extent）：
    /// - Horizontal 模式：宽度随页数增长；高度与视口高度一致。
    /// - Vertical 模式：高度随页数增长；宽度与视口宽度一致。
    /// </summary>
    private Size MeasureExtent(Size availableSize, int itemsCount)
    {
        Size childSize = GetChildSize(availableSize);

        if (this.Orientation == Orientation.Horizontal)
        {
            // 横向分页：每页 Columns * Rows 个元素；总宽度为页数 * 视口宽度
            return new Size((this.Columns * childSize.Width) * Math.Ceiling((double)itemsCount / (this.Columns * this.Rows)), _viewport.Height);
        }
        else
        {
            // 纵向滚动：总高度为页数 * 每页高度
            var pageHeight = (this.Rows * childSize.Height);

            var sizeWidth = _viewport.Width;
            var sizeHeight = pageHeight * Math.Ceiling((double)itemsCount / (this.Rows * this.Columns));

            return new Size(sizeWidth, sizeHeight);
        }
    }

    /// <summary>
    /// 根据项目索引计算其行列位置，并结合滚动偏移与方向放置到对应的矩形区域。
    /// </summary>
    private void ArrangeChild(int index, UIElement child, Size finalSize)
    {
        int row = index / this.Columns;
        int column = index % this.Columns;

        double xPosition, yPosition;

        int currentPage;
        Size childSize = GetChildSize(finalSize);

        if (this.Orientation == Orientation.Horizontal)
        {
            // 横向分页：currentPage 为 index 所在的页；xPosition 以页宽作为起点再加列偏移
            currentPage = (int)Math.Floor((double)index / (this.Columns * this.Rows));

            xPosition = (currentPage * this._viewport.Width) + (column * childSize.Width);
            yPosition = (row % this.Rows) * childSize.Height;

            // 减去滚动偏移，实现滚动效果
            xPosition -= this._offset.X;
            yPosition -= this._offset.Y;
        }
        else
        {
            // 纵向滚动：x 为列偏移，y 为行偏移
            xPosition = (column * childSize.Width) - this._offset.X;
            yPosition = (row * childSize.Height) - this._offset.Y;
        }

        child.Arrange(new Rect(xPosition, yPosition, childSize.Width, childSize.Height));
    }

    /// <summary>
    /// 计算单元格尺寸：将可用尺寸按 Columns/Rows 等分。
    /// </summary>
    private Size GetChildSize(Size availableSize)
    {
        double width = availableSize.Width / this.Columns;
        double height = availableSize.Height / this.Rows;

        return new Size(width, height);
    }

    /// <summary>
    /// 计算可见范围的首尾项目索引：
    /// - 使用 _offset 和 _viewport 推导当前页码（或可见区）。
    /// - 预取两页（pageSize * 2）以降低滚动时的抖动与频繁生成。
    /// </summary>
    private void GetVisibleRange(out int firstVisibleItemIndex, out int lastVisibleItemIndex)
    {
        Size childSize = GetChildSize(this._extent); // 这里的 childSize 仅用于概念一致性（未直接使用）

        int pageSize = this.Columns * this.Rows;
        int pageNumber = this.Orientation == System.Windows.Controls.Orientation.Horizontal ?
            (int)Math.Floor((double)this._offset.X / this._viewport.Width) :
            (int)Math.Floor((double)this._offset.Y / this._viewport.Height);

        firstVisibleItemIndex = (pageNumber * pageSize);
        // 额外加载一页（2 * pageSize）以优化滚动体验
        lastVisibleItemIndex = firstVisibleItemIndex + (pageSize * 2) - 1;

        ItemsControl itemsControl = ItemsControl.GetItemsOwner(this);
        int itemCount = itemsControl.HasItems ? itemsControl.Items.Count : 0;

        // 防止越界
        if (lastVisibleItemIndex >= itemCount)
        {
            lastVisibleItemIndex = itemCount - 1;
        }
    }

    #region - IScrollInfo -

    // 以下为 IScrollInfo 必须实现的属性与方法，用于与 ScrollViewer 协作进行滚动与视口更新。
    // ScrollViewer 在绑定到该面板时，会调用这些接口以获取滚动状态与执行滚动命令。

    public bool CanHorizontallyScroll
    {
        get { return _canHorizontallyScroll; }
        set { _canHorizontallyScroll = value; }
    }

    public bool CanVerticallyScroll
    {
        get { return _canVerticallyScroll; }
        set { _canVerticallyScroll = value; }
    }

    public double ExtentHeight
    {
        get { return this._extent.Height; }
    }

    public double ExtentWidth
    {
        get { return this._extent.Width; }
    }

    public double HorizontalOffset
    {
        get { return this._offset.X; }
    }

    public double VerticalOffset
    {
        get { return this._offset.Y; }
    }

    private ScrollViewer _owner;
    /// <summary>
    /// 所属的 ScrollViewer（由框架设置），用于通知滚动信息变更。
    /// </summary>
    public ScrollViewer ScrollOwner
    {
        get { return this._owner; }
        set { this._owner = value; }
    }

    public double ViewportHeight
    {
        get { return _viewport.Height; }
    }

    public double ViewportWidth
    {
        get { return _viewport.Width; }
    }

    // 基本滚动命令：按固定像素步长移动
    public void LineLeft()  { this.SetHorizontalOffset(this._offset.X - _scrollLength); }
    public void LineRight() { this.SetHorizontalOffset(this._offset.X + _scrollLength); }
    public void LineUp()    { this.SetVerticalOffset(this._offset.Y - _scrollLength); }
    public void LineDown()  { this.SetVerticalOffset(this._offset.Y + _scrollLength); }

    public Rect MakeVisible(Visual visual, Rect rectangle)
    {
        // 可根据需要将指定 Visual 可见（例如跳转到项），当前未实现（返回空矩形）。
        return new Rect();
    }

    // 鼠标滚轮：根据方向选择水平或垂直偏移
    public void MouseWheelDown()
    {
        if (this.Orientation == Orientation.Horizontal)
        {
            this.SetHorizontalOffset(this._offset.X + _scrollLength);
        }
        else
        {
            this.SetVerticalOffset(this._offset.Y + _scrollLength);
        }
    }

    public void MouseWheelUp()
    {
        if (this.Orientation == System.Windows.Controls.Orientation.Horizontal)
        {
            this.SetHorizontalOffset(this._offset.X - _scrollLength);
        }
        else
        {
            this.SetVerticalOffset(this._offset.Y - _scrollLength);
        }
    }

    public void MouseWheelLeft()  { return; }
    public void MouseWheelRight() { return; }

    // Page 级滚动：以视口宽度为步长（注意：这里 PageDown/Up 使用 Width，保持与现有逻辑一致）
    public void PageDown()  { this.SetVerticalOffset(this._offset.Y + _viewport.Width); }
    public void PageUp()    { this.SetVerticalOffset(this._offset.Y - _viewport.Width); }
    public void PageLeft()  { this.SetHorizontalOffset(this._offset.X - _viewport.Width); }
    public void PageRight() { this.SetHorizontalOffset(this._offset.X + _viewport.Width); }

    /// <summary>
    /// 设置横向偏移并触发重测量与滚动信息更新。
    /// </summary>
    public void SetHorizontalOffset(double offset)
    {
        _offset.X = Math.Max(0, offset); // 防止负偏移

        if (_owner != null)
        {
            _owner.InvalidateScrollInfo(); // 通知 ScrollViewer 更新滚动条
        }

        InvalidateMeasure(); // 请求重新布局（将触发 Measure/Arrange）
    }

    /// <summary>
    /// 设置纵向偏移并触发重测量与滚动信息更新。
    /// </summary>
    public void SetVerticalOffset(double offset)
    {
        _offset.Y = Math.Max(0, offset);

        if (_owner != null)
        {
            _owner.InvalidateScrollInfo();
        }

        InvalidateMeasure();
    }

    /// <summary>
    /// 同步 _extent 与 _viewport：
    /// - 根据当前可用尺寸与 Items 数量计算内容范围。
    /// - 若发生变化，通知 ScrollViewer 更新滚动条。
    /// </summary>
    private void UpdateScrollInfo(Size availableSize)
    {
        ItemsControl itemsControl = ItemsControl.GetItemsOwner(this);
        int itemCount = itemsControl.HasItems ? itemsControl.Items.Count : 0;

        Size extent = MeasureExtent(availableSize, itemCount);
        if (extent != _extent)
        {
            _extent = extent;
            if (_owner != null)
                _owner.InvalidateScrollInfo();
        }

        if (availableSize != _viewport)
        {
            _viewport = availableSize;
            if (_owner != null)
                _owner.InvalidateScrollInfo();
        }
    }
    #endregion
}
