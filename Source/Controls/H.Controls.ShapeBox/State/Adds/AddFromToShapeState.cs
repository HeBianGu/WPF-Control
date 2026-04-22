// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.ShapeBox.State.Adds;
public class AddFromToShapeState<T> : TwoClickAddSahpeState<T> where T : IFromToShape, new()
{
    public AddFromToShapeState()
    {

    }

    public AddFromToShapeState(IShapes shapes) : base(shapes)
    {

    }
    protected override T CreateShape()
    {
        return new T();
    }
    protected override void OneClick(Point p)
    {
        base.OneClick(p);
        this.Shape.From = p;
        this.Shape.To = p;
        this.RefreshStateShapes();
    }
    protected override void TwoClick(Point p)
    {
        this.Shape.To = p;
        this.RefreshStateShapes();
        this.Sumit();
    }

    protected override void ClickPreviewMove(Point p)
    {
        this.Shape.To = p;
        this.RefreshStateShapes();
    }
}
