// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Windows.Input;

namespace H.Controls.ShapeBox.State.Base;
public abstract class PreviewShapeStateBase : ShowEditStateBase
{
    private CrossShape _crossPreviewShape = new CrossShape();

    public bool UseCrossPreviewShape { get; set; } = true;

    protected virtual IEnumerable<IPreviewShape> GetPreviewShapes()
    {
        if (this.UseCrossPreviewShape)
            yield return this._crossPreviewShape;
    }

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
        var p = e.GetPosition(sender as FrameworkElement);
        this._crossPreviewShape.Point = p;
        base.MouseMove(sender, e);
        this.DrawPreviewShape();
    }

    protected void DrawPreviewShape()
    {
        var shapes = this.GetPreviewShapes();
        if (shapes == null)
            return;
        this.GetPreviewShapeView()?.DrawPreviewShape(shapes.ToArray());
    }

    public override void Exit()
    {
        this.GetPreviewShapeView()?.DrawPreviewShape();
        base.Exit();
    }

    public override void ScaleChanged()
    {
        base.ScaleChanged();
        this.DrawPreviewShape();
    }

    public override IShapeStyleSetting GetShapeStyleSetting()
    {
        return PickPixelShapeStyleSetting.Instance;
    }
}
