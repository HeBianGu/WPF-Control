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
    [Icon(FontIcons.RectangularClipping)]
    [Display(Name = "绘制矩形")]
    public class AddRectShapeState : TwoClickAddSahpeState<RectShape>
    {
        public AddRectShapeState()
        {
            this.Shape = new RectShape() { UseDimension = true };
        }

        protected override void TwoClick(Point p)
        {
            var first = this._clickPoints.ElementAt(0);
            this.Shape.Rect = new Rect(first,p);
            this.UpdateStateShape();
            this.Sumit();
        }

        protected override void ClickPreviewMove(Point p)
        {
            var first = this._clickPoints.ElementAt(0);
            this.Shape.Rect = new Rect(first, p);
            this.UpdateStateShape();
        }

        public override IShapeStyleSetting GetShapeStyleSetting()
        {
            return RectShapeStyleSetting.Instance;
        }
    }
}
