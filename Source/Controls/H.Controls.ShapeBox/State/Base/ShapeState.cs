// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.ShapeBox.Shapes.Base;

namespace H.Controls.ShapeBox.State.Base
{
    public abstract class ShapeState<T> : HandleShapeStateBase where T : IShape
    {
        public T Shape { get; set; }
        protected override IPreviewShape GetPreviewShape()
        {
            return this.Shape as IPreviewShape;
        }
        public override void ScaleChanged()
        {
            base.ScaleChanged();
            this.DrawStateShape(this.Shape);
        }

        public override void Enter()
        {
            base.Enter();
            this.DrawStateShape(this.Shape);
        }
        public override void Exit()
        {
            base.Exit();
            this.DrawStateShape();
        }
    }
}
