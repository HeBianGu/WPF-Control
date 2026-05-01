// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.ShapeBox.Shapes;
using H.Services.Message;
using System.Windows.Input;
using System.Windows.Shapes;

namespace H.Controls.ShapeBox.State.Base;
public interface IAddableShapes
{
    void AddShapes(params IShape[] shapes);
}

public interface IDeletableShapes
{
    void DeleteShapes(params IShape[] shapes);
}

public interface IShapes : IAddableShapes, IDeletableShapes
{
    IReadOnlyCollection<IShape> Shapes { get; }
}
public abstract class ShapeState<T> : HandleShapeStateBase where T : IShape
{
    protected ShapeState()
    {
        this.Shape = this.CreateShape();
    }
    public T Shape { get; set; }

    protected virtual T CreateShape()
    {
        return default(T);
    }

    protected override IEnumerable<IPreviewShape> GetPreviewShapes()
    {
        if (this.Shape is IPreviewShape preview)
            yield return preview;
        foreach (var item in base.GetPreviewShapes())
            yield return item;
    }
    public override void ScaleChanged()
    {
        base.ScaleChanged();
        this.RefreshStateShapes();
    }

    public override void Enter()
    {
        base.Enter();
        this.GetShapeStyleSetting()?.SaveTo(this.Shape);
        this.RefreshStateShapes();
    }

    public override IShapeStyleSetting GetShapeStyleSetting()
    {
        return base.GetShapeStyleSetting();
    }
    public override async void ShowEdit()
    {
        await IocMessage.Form?.ShowTabEdit(this.Shape, x => x.Title = this.Name);
        this.RefreshStateShapes();
        this.GetShapeStyleSetting()?.LoadBy(this.Shape);
    }


    private CrossShape _crossPreviewShape = new CrossShape();
    public override void MouseMove(object sender, MouseEventArgs e)
    {
        base.MouseMove(sender, e);
        this.RefreshStateShapes();
    }

    protected override IPreviewShapeView GetPreviewShapeView()
    {
        return base.GetPreviewShapeView();
    }

    protected override IEnumerable<IShape> GetStateShapes()
    {
        yield return this.Shape;
    }

    protected override void OnHitHandleMoved()
    {
        base.OnHitHandleMoved();
        this.RefreshStateShapes();
        this.DrawPreviewShape();
        this.RefreshShapeView();
    }

    protected virtual void ClearStateShape()
    {
        this.DrawStateShape();
    }

    protected override void ClearHitHande()
    {
        base.ClearHitHande();
        //this.ClearStateShape();
    }
}
