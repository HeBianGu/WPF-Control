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
    public interface IHandleShape
    {
        bool UseHandle { get; set; }

        IHandle HitIHandle(IView view, Point position);
    }

    public abstract class HandleShapeBase : SelectableShapeBase, IHandleShape
    {
        protected HandleShapeBase()
        {
            this.Handles = this.CreateHandles().ToList();
        }
        public bool UseHandle { get; set; }

        public override void Drawing(IView view, DrawingContext dc, Pen pen, Brush fill = null)
        {
            if (this.UseHandle)
                this.DrawHandle(view, dc, pen, fill);
        }
        public virtual void DrawHandle(IView view, DrawingContext drawingContext, Pen pen, Brush fill = null)
        {
            foreach (var item in this.CreateHandles())
            {
                item.Draw(view, drawingContext, pen, fill);
            }
        }

        public List<IHandle> Handles { get; set; }

        protected virtual IEnumerable<IHandle> CreateHandles()
        {
            return Enumerable.Empty<IHandle>();
        }

        public IHandle HitIHandle(IView view, Point position)
        {
            foreach (var handle in this.CreateHandles())
            {
                if (handle.HitTestPoint(view, position))
                    return handle;
            }
            return null;
        }
    }
}
