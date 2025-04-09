// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control



using H.Controls.Adorner.Adorner.ControlTemplateAdorners;
using System.Windows;

namespace H.Controls.Adorner.Draggable;

public class DragControlTemplateAdorner : ControlTemplateAdorner, IDraggableAdorner
{
    //private Brush vbrush;
    private Point location;
    public Point Offset { get; set; }
    public DraggableAdornerMode DropAdornerMode { get; set; }
    public DragControlTemplateAdorner(UIElement adornedElement, Point offset) : base(adornedElement)
    {
        this.Offset = offset;

    }

    public void UpdatePosition(Point location)
    {
        this.location = location;
        this.InvalidateArrange();
    }

    protected override void RefreshLayout()
    {
        Point point = new Point();
        point.X = this.location.X + (this.AdornedElement.DesiredSize.Width - this._contentControl.DesiredSize.Width) / 2;
        point.Y = this.location.Y + (this.AdornedElement.DesiredSize.Height - this._contentControl.DesiredSize.Height) / 2;
        point.Offset(-Offset.X, -Offset.Y);
        this._contentControl.Arrange(new Rect(point, this._contentControl.DesiredSize));
    }

    public object GetData()
    {
        return this.AdornedElement.GetDataContext();
    }
}
