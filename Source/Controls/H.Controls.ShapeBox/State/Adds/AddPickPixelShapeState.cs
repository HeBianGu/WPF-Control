// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")


namespace H.Controls.ShapeBox.State.Adds;
[Icon(FontIcons.Eyedropper)]
[Display(Name = "拾取像素")]
public class AddPickPixelShapeState : OneClickAddShapeState<PixelColorPointShape>
{
    private PixelColorPointShape _previewShape = new PixelColorPointShape();
    public AddPickPixelShapeState()
    {

    }

    public AddPickPixelShapeState(IShapes shapes) : base(shapes)
    {

    }
    protected override PixelColorPointShape CreateShape()
    {
        return new PixelColorPointShape();
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

    protected override IEnumerable<IPreviewShape> GetPreviewShapes()
    {
        yield return this._previewShape;
    }
    public override IShapeStyleSetting GetShapeStyleSetting()
    {
        return PickPixelShapeStyleSetting.Instance;
    }
}
