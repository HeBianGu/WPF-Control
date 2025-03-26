// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control
using H.Controls.Adorner.Adorner.Base;
using System.Windows;
using System.Windows.Media;

namespace H.Controls.Adorner.Draggable;

public class DraggableAdorner : AdornerBase, IDraggableAdorner
{
    public static ComponentResourceKey ListBoxItemAllowDropKey => new ComponentResourceKey(typeof(DraggableAdorner), "S.ListBoxItem.AllowDrop");
    public static ComponentResourceKey ListBoxItemAllowDropBothKey => new ComponentResourceKey(typeof(DraggableAdorner), "S.ListBoxItem.AllowDrop.Both");

    private Brush vbrush;
    private Point location;
    public Point Offset { get; set; }
    public DraggableAdornerMode DropAdornerMode { get; set; }

    private readonly object _data;
    public DraggableAdorner(UIElement adornedElement, Point offset) : base(adornedElement)
    {
        this.Offset = offset;
        vbrush = new VisualBrush(this.AdornedElement);
        vbrush.Opacity = AdornerSetting.Instance.DragAornerOpacity;
        this._data = adornedElement.GetDataContext();
    }

    public void UpdatePosition(Point location)
    {
        this.location = location;
        this.InvalidateVisual();
    }

    protected override void OnRender(DrawingContext dc)
    {
        Point p = location;
        if (this.DropAdornerMode == DraggableAdornerMode.OnlyY)
        {
            p = new Point(0, p.Y);
            p.Offset(0, -this.Offset.Y);
        }
        else if (this.DropAdornerMode == DraggableAdornerMode.OnlyX)
        {
            p = new Point(p.X, 0);
            p.Offset(-this.Offset.X, 0);
        }
        else
        {
            p.Offset(-this.Offset.X, -this.Offset.Y);
        }

        dc.DrawRectangle(vbrush, null, new Rect(p, this.RenderSize));
    }
    public object GetData()
    {
        //return this.AdornedElement.GetDataContext();
        return this._data;
    }
}
