// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.ShapeBox.Shapes.Handles;

public class ActionCircleHandle : ActionHandle
{
    public ActionCircleHandle(Action<Point> moveToAction, Point postion, IShape shape) : base(moveToAction, postion, shape)
    {

    }

    public override void Draw(IView view, DrawingContext drawingContext, Pen pen, Brush fill = null)
    {
        drawingContext.DrawCircle(this.Postion, pen, 0.5 * this.Length / view.Scale, Brushes.White);
    }
}
