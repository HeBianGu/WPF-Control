// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.ShapeBox.Shapes.Base;
using System.Windows.Input;

namespace H.Controls.ShapeBox.State.Adds;

public class AddPointsMouseMoveShapeState<T> : AddMouseMoveShapeState<T> where T : IPointsShape, new()
{
    public AddPointsMouseMoveShapeState()
    {

    }

    public AddPointsMouseMoveShapeState(IShapes shapes) : base(shapes)
    {

    }

    public override IShapeStyleSetting GetShapeStyleSetting()
    {
        return PolygonShapeStyleSetting.Instance;
    }

    public override void MouseMove(object sender, MouseEventArgs e)
    {
        base.MouseMove(sender, e);
        this.Shape.Points = new Points(this._clickPoints);
        this.RefreshStateShapes();
    }
}
