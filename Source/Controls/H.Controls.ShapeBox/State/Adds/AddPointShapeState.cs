// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Common.Attributes;
using H.Controls.ShapeBox.Shapes;
using H.Controls.ShapeBox.State.Adds.Base;
using H.Extensions.FontIcon;
using System.ComponentModel.DataAnnotations;

namespace H.Controls.ShapeBox.State.Adds
{
    [Icon(FontIcons.CalculatorMultiply)]
    [Display(Name = "绘制点")]
    public class AddPointShapeState : OneClickAddShapeState<PointShape>
    {
        public AddPointShapeState()
        {
            this.Shape = new PointShape();
        }

        protected override void OneClick(Point p)
        {
            this.Shape.Point = p;
            base.OneClick(p);
        }
        protected override void ClickPreviewMove(Point p)
        {
            //this.Shape.Point = p;
            //this.DrawStateShape(this.Shape);
        }
    }
}
