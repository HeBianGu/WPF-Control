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
using System.Windows.Media.Imaging;
using System.Windows.Media;
using H.Controls.ShapeBox.Shapes.Base;

namespace H.Controls.ShapeBox.State.Adds
{
    [Icon(FontIcons.Eyedropper)]
    [Display(Name = "拾取像素点")]
    public class AddPickPixelShapeState : OneClickAddShapeState<PixelColorPointShape>
    {
        private PixelColorPointShape _previewShape = new PixelColorPointShape();
        public AddPickPixelShapeState()
        {
            this.Shape = new PixelColorPointShape();
        }

        protected override void OneClick(Point p)
        {
            this.Shape.Point = p;
            this.Shape.Color = this._previewShape.Color;
            base.OneClick(p);
            this.DrawStateShape(this.Shape, this._previewShape);
        }
        protected override void ClickPreviewMove(Point p)
        {
            this._previewShape.Point = p;
            if (this.View is IImageView imageView)
                this._previewShape.Color = imageView.PickColor(p);
            this.DrawPreviewShape();
        }

        protected override IPreviewShape GetPreviewShape()
        {
            return this._previewShape;
        }
    }
}
