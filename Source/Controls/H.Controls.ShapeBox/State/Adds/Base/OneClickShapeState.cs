// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.ShapeBox.Shapes.Base;

namespace H.Controls.ShapeBox.State.Adds.Base
{
    public abstract class OneClickShapeState<T> : AddShapeState<T> where T : IShape
    {
        protected override void OnClick(Queue<Point> points)
        {
            if (points.Count == 1)
                this.OneClick(points.ElementAt(0));
        }
        protected virtual void OneClick(Point p)
        {
            this.Sumit();
        }

        protected override void OnPreviewMouseMove(Point p)
        {
            if (this._clickPoints.Count == 0)
                this.ClickPreviewMove(p);
        }
        protected abstract void ClickPreviewMove(Point p);
    }
}
