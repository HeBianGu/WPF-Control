// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

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
