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
    public abstract class PreviewShapeStateBase : StateBase
    {
        protected abstract IPreviewShape GetPreviewShape();

        protected virtual IPreviewShapeView GetPreviewShapeView()
        {
            return this.View as IPreviewShapeView;
        }
        public override void MouseLeave(object sender, MouseEventArgs e)
        {
            base.MouseLeave(sender, e);
            this.GetPreviewShapeView()?.DrawPreviewShape();
        }

        public override void MouseMove(object sender, MouseEventArgs e)
        {
            base.MouseMove(sender, e);
            this.UpdatePreviewShape();
        }

        void UpdatePreviewShape()
        {
            var shape = this.GetPreviewShape();
            if (shape == null)
                return;
            this.GetPreviewShapeView()?.DrawPreviewShape(shape);
        }

        public override void Exit()
        {
            this.GetPreviewShapeView()?.DrawPreviewShape();
            base.Exit();
        }

        public override void ScaleChanged()
        {
            base.ScaleChanged();
            this.UpdatePreviewShape();
        }
    }
}
