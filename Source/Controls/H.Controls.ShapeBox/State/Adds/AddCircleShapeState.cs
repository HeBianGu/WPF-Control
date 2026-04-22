// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using H.Common.Attributes;
global using H.Controls.ShapeBox.Settables;
global using H.Controls.ShapeBox.Shapes;
global using H.Controls.ShapeBox.State.Adds.Base;
global using H.Extensions.FontIcon;
global using System.ComponentModel.DataAnnotations;

namespace H.Controls.ShapeBox.State.Adds;
[Icon(FontIcons.CircleRing)]
[Display(Name = "绘制圆")]
public class AddCircleShapeState : TwoClickAddSahpeState<CircleShape>
{
    public AddCircleShapeState()
    {

    }

    public AddCircleShapeState(IShapes shapes) : base(shapes)
    {

    }
    protected override CircleShape CreateShape()
    {
        return new CircleShape() { UseDimension = true };
    }
    protected override void OneClick(Point p)
    {
        base.OneClick(p);
        this.Shape.Center = p;
        this.RefreshStateShapes();
    }
    protected override void TwoClick(Point p)
    {
        this.Shape.Radius = (p - this.Shape.Center).Length;
        this.RefreshStateShapes();
        this.Sumit();
    }

    protected override void ClickPreviewMove(Point p)
    {
        this.Shape.Radius = (p - this.Shape.Center).Length;
        this.RefreshStateShapes();
    }

    public override IShapeStyleSetting GetShapeStyleSetting()
    {
        return CircleShapeStyleSetting.Instance;
    }
}
