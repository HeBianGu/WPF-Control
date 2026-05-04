// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Extensions.Common;
using H.Extensions.Mvvm.Commands;
using H.Extensions.Mvvm.ViewModels.Base;
using System.Collections.Specialized;

namespace H.Controls.ShapeBox;
public interface ISelectShapeBox
{
    IReadOnlyCollection<ISelectableShape> SelectedShapes { get; }
    void SelectShapes(params ISelectableShape[] selectableShapes);
}

public class SelectShapeBox : MouseOverShapeBox, ISelectShapeBox
{
    private DrawingVisual _SelectableShapeDrawingVisual = new DrawingVisual();

    protected override IEnumerable<Visual> CreateVisuals()
    {
        return base.CreateVisuals().Concat(this._SelectableShapeDrawingVisual.ToEnumerable());
    }

    public Brush SelectStroke
    {
        get { return (Brush)GetValue(SelectStrokeProperty); }
        set { SetValue(SelectStrokeProperty, value); }
    }

    public static readonly DependencyProperty SelectStrokeProperty =
        DependencyProperty.Register("SelectStroke", typeof(Brush), typeof(SelectShapeBox), new FrameworkPropertyMetadata(Brushes.Red, (d, e) =>
        {
            SelectShapeBox control = d as SelectShapeBox;

            if (control == null) return;

            if (e.OldValue is Brush o)
            {

            }

            if (e.NewValue is Brush n)
            {

            }

        }));


    public double SelectStrokeThickness
    {
        get { return (double)GetValue(SelectStrokeThicknessProperty); }
        set { SetValue(SelectStrokeThicknessProperty, value); }
    }

    public static readonly DependencyProperty SelectStrokeThicknessProperty =
        DependencyProperty.Register("SelectStrokeThickness", typeof(double), typeof(SelectShapeBox), new FrameworkPropertyMetadata(1.0, (d, e) =>
        {
            SelectShapeBox control = d as SelectShapeBox;

            if (control == null) return;

            if (e.OldValue is double o)
            {

            }

            if (e.NewValue is double n)
            {

            }

        }));



    public Brush SelectFill
    {
        get { return (Brush)GetValue(SelectFillProperty); }
        set { SetValue(SelectFillProperty, value); }
    }

    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty SelectFillProperty =
        DependencyProperty.Register("SelectFill", typeof(Brush), typeof(SelectShapeBox), new FrameworkPropertyMetadata(default(Brush), (d, e) =>
        {
            SelectShapeBox control = d as SelectShapeBox;

            if (control == null) return;

            if (e.OldValue is Brush o)
            {

            }

            if (e.NewValue is Brush n)
            {

            }

        }));


    protected override void OnScaleChanged()
    {
        base.OnScaleChanged();
        if (this.SelectStrokeThickness > 0)
            this.DrawSelectableShapes();
    }

    protected override void OnShapesChanged()
    {
        base.OnShapesChanged();
        this.SelectShapes();
    }

    public override void UpdateAll()
    {
        base.UpdateAll();
        this.DrawSelectableShapes();
    }

    public ObservableCollection<ISelectableShape> SelectedShapes
    {
        get { return (ObservableCollection<ISelectableShape>)GetValue(SelectedShapesProperty); }
        set { SetValue(SelectedShapesProperty, value); }
    }

    public static readonly DependencyProperty SelectedShapesProperty =
        DependencyProperty.Register("SelectedShapes", typeof(ObservableCollection<ISelectableShape>), typeof(SelectShapeBox), new FrameworkPropertyMetadata(default(ObservableCollection<ISelectableShape>), (d, e) =>
        {
            SelectShapeBox control = d as SelectShapeBox;

            if (control == null) return;

            if (e.OldValue is ObservableCollection<ISelectableShape> o)
            {
                o.CollectionChanged -= control.SelectedShapes_CollectionChanged;
            }

            if (e.NewValue is ObservableCollection<ISelectableShape> n)
            {
                n.CollectionChanged -= control.SelectedShapes_CollectionChanged;
                n.CollectionChanged += control.SelectedShapes_CollectionChanged;
            }
            control.DrawSelectableShapes();
        }));


    public ISelectableShape SelectedShape
    {
        get { return (ISelectableShape)GetValue(SelectedShapeProperty); }
        set { SetValue(SelectedShapeProperty, value); }
    }

    IReadOnlyCollection<ISelectableShape> ISelectShapeBox.SelectedShapes => this.SelectedShapes;

    public static readonly DependencyProperty SelectedShapeProperty =
        DependencyProperty.Register("SelectedShape", typeof(ISelectableShape), typeof(SelectShapeBox), new FrameworkPropertyMetadata(default(ISelectableShape), (d, e) =>
        {
            SelectShapeBox control = d as SelectShapeBox;

            if (control == null) return;

            if (e.OldValue is ISelectableShape o)
            {

            }

            if (e.NewValue is ISelectableShape n)
            {

            }
            control.DrawSelectableShapes();
        }));


    private IEnumerable<ISelectableShape> GetSelectableShapes()
    {
        if (this.SelectedShape != null)
            yield return this.SelectedShape;
        if (this.SelectedShapes == null)
            yield break;
        foreach (var item in this.SelectedShapes)
            yield return item;
    }

    private void SelectedShapes_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        this.DrawSelectableShapes();
    }

    protected override void ShapesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        base.ShapesCollectionChanged(sender, e);
        this.Dispatcher.Invoke(() => this.DrawSelectableShapes());
    }
    public void SelectShapes(params ISelectableShape[] selectableShapes)
    {
        this.SelectedShapes = selectableShapes.ToObservable();
        this.DrawSelectableShapes();
    }

    private void DrawSelectableShapes()
    {
        using var drawingContext = this._SelectableShapeDrawingVisual.RenderOpen();

        var selectedShapes = this.GetSelectableShapes().ToList();
        if (selectedShapes == null || selectedShapes.Count() == 0)
            return;
        var strokeThickness = this.ToViewThickness(this.SelectStrokeThickness);
        foreach (var item in selectedShapes.Where(x => this.Shapes?.Contains(x) == true))
        {
            item.DrawSelect(this, drawingContext, this.SelectStroke, strokeThickness, this.SelectFill);
        }
    }

    protected override IEnumerable<IDisplayCommand> CreateContextMenuommands()
    {
        foreach (var item in base.CreateContextMenuommands())
        {
            yield return item;
        }
        if (this.SelectedShape is ICommandsBindable bindable)
        {
            foreach (var item in bindable.Commands.OfType<IDisplayCommand>())
            {
                yield return item;
            }
        }
    }
}
