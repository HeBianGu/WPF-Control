// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Extensions.Mvvm.ViewModels.Base;
using System.Windows.Input;

namespace H.Controls.ShapeBox.State.Base;
public abstract class StateBase : DisplayBindableBase, IViewState
{
    public IView View { get; set; }
    public virtual void MouseLeave(object sender, MouseEventArgs e)
    {

    }

    public virtual void MouseUp(object sender, MouseButtonEventArgs e)
    {

    }

    public virtual void MouseMove(object sender, MouseEventArgs e)
    {

    }


    public virtual void MouseDown(object sender, MouseButtonEventArgs e)
    {

    }

    protected virtual void Clear()
    {

    }

    protected virtual IStateShapeView GetStateShapeView() => this.View as IStateShapeView;

    protected virtual IShapeView GetShapeView() => this.View as IShapeView;

    protected void RefreshShapeView(params IShape[] shapes)
    {
        this.GetShapeView()?.DrawShapes();
    }


    protected FrameworkElement GetElementView() => this.View as FrameworkElement;


    protected void DrawStateShape(params IShape[] shapes)
    {
        this.GetStateShapeView()?.DrawStateShape(shapes);
    }

    public virtual void Enter()
    {

    }

    public virtual void Exit()
    {
        this.Clear();
        this.DrawStateShape();
        this.View = null;
    }

    public virtual void ScaleChanged()
    {
        this.RefreshStateShapes();
    }

    public void RefreshStateShapes()
    {
        var shapes = this.GetStateShapes().ToArray();
        this.DrawStateShape(shapes);
    }

    protected virtual IEnumerable<IShape> GetStateShapes()
    {
        yield break;
    }

    protected virtual IEnumerable<IShape> GetShapes()
    {
        if(this.View is IShapeView shapeView)
            foreach (var item in shapeView.GetShapes())
            {
                yield return item;
            }
    }

    private Cursor _cursor;
    public virtual Cursor Cursor
    {
        get { return _cursor ?? Cursors.Arrow; }
        set { _cursor = value; }
    }

    public virtual void QueryCursor(object sender, QueryCursorEventArgs e)
    {
        this.UpdateCursor(e);
        e.Handled = true;
    }

    protected virtual void UpdateCursor(QueryCursorEventArgs e)
    {
        e.Cursor = this.Cursor;
    }
}
