// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")


namespace H.Controls.Adorner.Draggable;

public class DragableDataTemplateAdorner : DataTemplateAdorner, IDraggableAdorner
{
    private Pen _borderPen = null;
    private Point _location;
    public Point Offset { get; set; }
    public DraggableAdornerMode DropAdornerMode { get; set; }
    public double BorderPadding { get; set; } = 2;
    public DragableDataTemplateAdorner(UIElement adornedElement, Point offset) : base(adornedElement)
    {
        this._borderPen = this.CreateBorderPen();
        this.Offset = offset;
    }

    public DragableDataTemplateAdorner(UIElement adornedElement, object data, Point offset) : base(adornedElement, data)
    {
        this._borderPen = this.CreateBorderPen();
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
        this.InvalidateVisual();

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

    protected virtual Pen CreateBorderPen()
    {
        var r = new Pen(new SolidColorBrush(Colors.LightSkyBlue), 2) { DashStyle = DashStyles.Dash };
        r.Freeze();
        return r;
    }

    protected override void OnRender(DrawingContext drawingContext)
    {
        base.OnRender(drawingContext);
        Rect rect = new Rect(this.RenderSize);
        rect.Offset(-Offset.X, -Offset.Y);
        rect.Offset(_location.X, _location.Y);
        if (this._borderPen == null)
            return;
        drawingContext.DrawRoundedRectangle(null, this._borderPen, rect.GetPadding(this.BorderPadding), 2, 2);
    }
}
