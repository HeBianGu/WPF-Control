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
        bool HitTestPoint(IView view, Point position);
        void Draw(IView view, DrawingContext drawingContext, Pen pen, Brush fill = null);

        void MoveTo(Point point);
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

        public bool HitTestPoint(IView view, Point position)
        {
            var d = 0.5 * this.Length / view.Scale;
            var v = this.Postion - position;
            return Math.Abs(v.X) < d && Math.Abs(v.Y) < d;
        }

        public abstract void MoveTo(Point point);
    }

    public class ActionHandle : HandleBase
    {
        private Action<Point> _moveToAction;
        public ActionHandle(Action<Point> moveToAction, Point postion) : base(postion)
        {
            this._moveToAction = moveToAction;
        }

        public override void MoveTo(Point point)
        {
            this._moveToAction?.Invoke(point);
        }
    }
}
