// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.ShapeBox.State.Adds;
[Icon(FontIcons.RectangularClipping)]
[Display(Name = "绘制矩形")]
public class AddRectShapeState : AddRectShapeState<RectShape>
{
    public AddRectShapeState()
    {

    }

    public AddRectShapeState(IShapes shapes) : base(shapes)
    {

    }
    protected override RectShape CreateShape()
    {
        return new RectShape() { UseDimension = true, UseCross = true, UseHandle = false };
    }
}

public abstract class AddRectShapeState<T> : TwoClickAddSahpeState<T> where T : IRectShape
{
    public AddRectShapeState()
    {

    }

    public AddRectShapeState(IShapes shapes) : base(shapes)
    {

    }
    protected override void TwoClick(Point p)
    {
        var first = this._clickPoints.ElementAt(0);
        this.Shape.Rect = new Rect(first, p);
        this.RefreshStateShapes();
        this.Sumit();
    }

    protected override void ClickPreviewMove(Point p)
    {
        var first = this._clickPoints.ElementAt(0);
        this.Shape.Rect = new Rect(first, p);
        this.RefreshStateShapes();
    }

    public override IShapeStyleSetting GetShapeStyleSetting()
    {
        return RectShapeStyleSetting.Instance;
    }
}

