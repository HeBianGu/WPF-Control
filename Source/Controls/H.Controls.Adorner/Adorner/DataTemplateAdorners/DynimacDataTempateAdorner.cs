// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using System.Windows;

namespace H.Controls.Adorner.Adorner.DataTemplateAdorners;

public class DynimacDataTempateAdorner : DataTemplateAdorner, IDynimacAdorner
{
    private Point location;
    public Point Offset { get; set; }
    public DynimacDataTempateAdorner(UIElement adornedElement) : base(adornedElement)
    {

    }

    public DynimacDataTempateAdorner(UIElement adornedElement, object data) : base(adornedElement, data)
    {

    }

    public void UpdatePosition(Point location)
    {
        this.location = location;
        this._contentPresenter.Arrange(new Rect(this.location, this._contentPresenter.DesiredSize));
    }


    protected override Size ArrangeOverride(Size finalSize)
    {
        Size size = base.ArrangeOverride(finalSize);
        this.UpdatePosition(this.location);
        return size;

    }
}
