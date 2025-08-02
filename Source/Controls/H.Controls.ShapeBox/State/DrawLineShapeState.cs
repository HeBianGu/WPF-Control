// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.ShapeBox.Shapes.Base;
using H.Controls.ShapeBox.Shapes;
using System.Windows.Input;
using H.Controls.ShapeBox.State.Base;

namespace H.Controls.ShapeBox.State
{
    public class DrawLineShapeState : PreviewShapeStateBase
    {
        protected LineShape _lineShape = new LineShape();
        protected Point? _first;

        protected override IPreviewShape GetPreviewShape()
        {
            return this._lineShape;
        }
        public override void MouseMove(object sender, MouseEventArgs e)
        {
            base.MouseMove(sender, e);
            if (this._first == null)
                return;
            var p = e.GetPosition(sender as FrameworkElement);
            this._lineShape.To = p;
            this.DrawStateShape(this._lineShape);
        }

        public override void ScaleChanged()
        {
            base.ScaleChanged();
            this.DrawStateShape(this._lineShape);
        }

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

            var p = e.GetPosition(sender as FrameworkElement);
            if (this._first == null)
            {
                this._first = p;
                this._lineShape.From = p;
                this._lineShape.To = p;
                this.DrawStateShape(this._lineShape);
                return;
            }
            else
            {
                this._lineShape.To = p;
                this.Sumit();
                this.Clear();
            }
        }



        protected override void Clear()
        {
            base.Clear();
            this._first = null;
        }
    }
}
