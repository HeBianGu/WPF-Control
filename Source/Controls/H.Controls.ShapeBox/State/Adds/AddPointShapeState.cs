// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.ShapeBox.State.Adds;
[Icon(FontIcons.CalculatorMultiply)]
[Display(Name = "绘制点")]
public class AddPointShapeState : OneClickAddShapeState<PointShape>
{
    public AddPointShapeState()
    {

    }

    public AddPointShapeState(IShapes shapes) : base(shapes)
    {

    }
    protected override PointShape CreateShape()
    {
        return new PointShape();
    }
    protected override void OneClick(Point p)
    {
        this.Shape.Point = p;
        base.OneClick(p);
    }
    protected override void ClickPreviewMove(Point p)
    {
        //this.Shape.Point = p;
        //this.DrawStateShape(this.Shape);
    }

    public override IShapeStyleSetting GetShapeStyleSetting()
    {
        return PointShapeStyleSetting.Instance;
    }
}
