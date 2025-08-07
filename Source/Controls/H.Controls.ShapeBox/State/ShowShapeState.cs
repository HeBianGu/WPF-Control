// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Common.Attributes;
using H.Controls.ShapeBox.Shapes.Base;
using H.Controls.ShapeBox.State.Base;
using H.Extensions.FontIcon;
using System.ComponentModel.DataAnnotations;

namespace H.Controls.ShapeBox.State
{

    [Icon(FontIcons.Unit)]
    [Display(Name = "显示对象")]
    public class ShowShapeState<T> : ShapeState<T> where T : IShape
    {
        public override IHandle HitHandle(Point point)
        {
            if (this.Shape is IHandleShape handleShape)
                return handleShape.HitIHandle(this.View, point);
            return null;
        }

        protected override void OnHitHandleMoved()
        {
            base.OnHitHandleMoved();
            this.DrawStateShape(this.Shape);
        }
    }
}
