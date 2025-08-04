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
    [Icon(FontIcons.CircleRing)]
    [Display(Name = "绘制圆")]
    public class AddCircleShapeState : TwoClickAddSahpeState<CircleShape>
    {
        public AddCircleShapeState()
        {
            this.Shape = new CircleShape() { UseDimension = true };
        }

        protected override void OneClick(Point p)
        {
            base.OneClick(p);
            this.Shape.Center = p;
            this.UpdateStateShape();
        }
        protected override void TwoClick(Point p)
        {
            this.Shape.Radius = (p - this.Shape.Center).Length;
            this.UpdateStateShape();
            this.Sumit();
        }

        protected override void ClickPreviewMove(Point p)
        {
            this.Shape.Radius = (p - this.Shape.Center).Length;
            this.UpdateStateShape();
        }

        public override IShapeStyleSetting GetShapeStyleSetting()
        {
            return CircleShapeStyleSetting.Instance;
        }
    }
}
