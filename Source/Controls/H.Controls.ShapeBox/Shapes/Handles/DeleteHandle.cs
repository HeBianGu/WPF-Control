// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.ShapeBox.Shapes.Handles;

public class DeleteHandle : HandleBase, IDeleteHandle
{
    public DeleteHandle(Point postion, IShape shape) : base(postion, shape)
    {
        var fill = new SolidColorBrush(Colors.Gray) { Opacity = 0.5 };
        fill.Freeze();
        this.DeleteFill = fill;
        this.DeleteStroke = Brushes.Red;
        this.Length = 10;
    }
    public Brush DeleteFill { get; set; }
    public Brush DeleteStroke { get; set; }

    public override void Draw(IView view, DrawingContext drawingContext, Pen pen, Brush fill = null)
    {
        double len = 0.5 * this.Length / view.Scale;
        //drawingContext.DrawRoundedRectangle(this.DeleteFill, null, this.Postion.ToRect(len), 2 / view.Scale, 2 / view.Scale);
        drawingContext.DrawCircle(this.Postion, null, len, this.DeleteFill);
        drawingContext.DrawCross(this.Postion, this.DeleteStroke, pen.Thickness, 0.5 * len, 0.5 * len, 45);
    }
}
