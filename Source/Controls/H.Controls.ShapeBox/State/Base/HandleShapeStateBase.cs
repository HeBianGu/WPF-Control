// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.ShapeBox.Shapes.Base;
using System.Windows.Input;

namespace H.Controls.ShapeBox.State.Base
{
    public abstract class HandleShapeStateBase : PreviewShapeStateBase
    {
        private IHandle _hitHandle;
        public override void MouseDown(object sender, MouseButtonEventArgs e)
        {
            base.MouseDown(sender, e);
            var position = e.GetPosition(this.GetElementView());
            this._hitHandle = this.HitHandle(position);
            e.Handled = true;
        }

        public override void MouseLeave(object sender, MouseEventArgs e)
        {
            base.MouseLeave(sender, e);
            this.ClearHitHande();
        }

        public override void MouseUp(object sender, MouseButtonEventArgs e)
        {
            base.MouseUp(sender, e);
            this.ClearHitHande();
        }

        public override void MouseMove(object sender, MouseEventArgs e)
        {
            base.MouseMove(sender, e);
            if (this._hitHandle == null)
                return;
            var position = e.GetPosition(this.GetElementView());
            this._hitHandle.MoveTo(position);
            this.OnHitHandleMoved();
        }

        protected virtual void OnHitHandleMoved()
        {

        }

        protected void ClearHitHande()
        {
            this._hitHandle = null;
        }

        protected override void Clear()
        {
            base.Clear();
            this.ClearHitHande();
        }
        public virtual IHandle HitHandle(Point point)
        {
            return null;
        }
    }
}
