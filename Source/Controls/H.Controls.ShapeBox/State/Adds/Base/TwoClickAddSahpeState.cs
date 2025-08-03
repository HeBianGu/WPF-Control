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
    public abstract class TwoClickAddSahpeState<T> : OneClickAddShapeState<T> where T : IShape
    {
        protected override void OnClick(Queue<Point> points)
        {
            base.OnClick(points);
            if (points.Count == 2)
            {
                this.TwoClick(points.ElementAt(1));
            }
        }
        protected override void OneClick(Point p)
        {

        }
        protected virtual void TwoClick(Point p)
        {
            this.Sumit();
        }
        protected override void OnPreviewMouseMove(Point p)
        {
            if (this._clickPoints.Count == 1)
                this.ClickPreviewMove(p);
        }
    }
}
