// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Common.Attributes;
using H.Controls.ShapeBox.Shapes;
using H.Extensions.FontIcon;
using System.ComponentModel.DataAnnotations;

namespace H.Controls.ShapeBox.State.Adds
{
    [Icon(FontIcons.Ruler)]
    [Display(Name = "绘制标尺")]
    public class AddRulerLineShapeState : AddFromToShapeState<RulerLineShape>
    {
        public AddRulerLineShapeState()
        {
            this.Shape.UseHandle = false;
            this.Shape.UseText = true;
        }

        protected override void Sumit()
        {
            base.Sumit();
            this.DrawStateShape(this.Shape);
        }
    }
}
