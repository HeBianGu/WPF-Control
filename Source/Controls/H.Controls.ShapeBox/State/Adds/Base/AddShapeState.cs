// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.ShapeBox.Shapes.Base;
using H.Controls.ShapeBox.State.Base;
using System.Windows.Input;

namespace H.Controls.ShapeBox.State.Adds.Base
{
    public abstract class AddShapeState<T> : ShapeState<T> where T : IShape
    {
        public Queue<Point> _clickPoints = new Queue<Point>();
        public override void MouseDown(object sender, MouseButtonEventArgs e)
        {
            base.MouseDown(sender, e);
            if (e.ChangedButton == MouseButton.Right)
            {
                this.Cancel();
                return;
            }
            if (e.ChangedButton != MouseButton.Left)
                return;
            var p = e.GetPosition(this.GetElementView());
            this._clickPoints.Enqueue(p);
            this.OnClick(this._clickPoints);
        }

        public override void MouseMove(object sender, MouseEventArgs e)
        {
            base.MouseMove(sender, e);
            if (this._clickPoints == null)
                return;
            var p = e.GetPosition(sender as FrameworkElement);
            this.OnPreviewMouseMove(p);
        }

        protected abstract void OnClick(Queue<Point> points);

        protected abstract void OnPreviewMouseMove(Point p);

        protected virtual void Sumit()
        {
            this.Clear();
            this.ClearStateShape();
        }

        public virtual void Cancel()
        {
            this.Clear();
            this.ClearStateShape();
        }

        protected override void Clear()
        {
            base.Clear();
            this._clickPoints.Clear();
        }
    }
}
