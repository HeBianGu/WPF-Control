// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")
using H.Controls.ShapeBox.Drawings;
using System.Windows.Media;

namespace H.Controls.ShapeBox.Shapes.Base
{
    public interface IHandle
    {
        Point Postion { get; }
        void Draw(IView view, DrawingContext drawingContext, Pen pen, Brush fill = null);
    }

    public abstract class HandleBase : IHandle
    {
        public HandleBase(Point postion)
        {
            this.Postion = postion;
        }

        public Point Postion { get; set; }
        public double Length { get; set; } = 6.0;

        public virtual void Draw(IView view, DrawingContext drawingContext, Pen pen, Brush fill = null)
        {
            drawingContext.DrawHandle(this.Postion, pen.Brush, pen.Thickness, this.Length / view.Scale);
        }
    }

    public class ActionHandle : HandleBase
    {
        public ActionHandle(Point postion) : base(postion)
        {

        }
    }
}
