// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control
using H.Controls.Adorner.Adorner.DataTemplateAdorners;
using System.Windows;

namespace H.Controls.Adorner.Draggable;

public class DragableDataTemplateAdorner : DataTemplateAdorner, IDraggableAdorner
{
    private Point _location;
    public Point Offset { get; set; }
    public DraggableAdornerMode DropAdornerMode { get; set; }
    public DragableDataTemplateAdorner(UIElement adornedElement, Point offset) : base(adornedElement)
    {
        this.Offset = offset;
    }

    public DragableDataTemplateAdorner(UIElement adornedElement, object data, Point offset) : base(adornedElement, data)
    {
        this.Offset = offset;
    }

    public void UpdatePosition(Point location)
    {
        this._location = location;
        this.InvalidateArrange();
    }

    protected override void RefreshLayout()
    {
        Point point = new Point();
        //point.X = this.location.X + ((this.AdornedElement.DesiredSize.Width - this._contentPresenter.DesiredSize.Width) / 2);
        //point.Y = this.location.Y + ((this.AdornedElement.DesiredSize.Height - this._contentPresenter.DesiredSize.Height) / 2);
        point.Offset(-Offset.X, -Offset.Y);
        point.Offset(_location.X, _location.Y);
        this._contentPresenter.Arrange(new Rect(point, this._contentPresenter.DesiredSize));

    }

    //protected override void RefreshLayout()
    //{
    //    Point point = new Point();
    //    point.X = this.location.X + ((this.AdornedElement.DesiredSize.Width - this._contentPresenter.DesiredSize.Width) / 2);
    //    point.Y = this.location.Y + ((this.AdornedElement.DesiredSize.Height - this._contentPresenter.DesiredSize.Height) / 2);
    //    point.Offset(-Offset.X, -Offset.Y);
    //    this._contentPresenter.Arrange(new Rect(point, this._contentPresenter.DesiredSize));
    //}

    public object GetData()
    {
        return this._contentPresenter.Content;
    }
}
