// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using H.Controls.Adorner.Draggable;
global using System.Windows.Controls;
using H.Common.Interfaces;
using H.Themes;

namespace H.Presenters.Design.Base;

public class BorderDefinitionPresenter : DesignPresenterBase
{

}

public abstract class GridPresenterBase : PanelPresenterBase
{
    public GridPresenterBase()
    {
        this.Background = Brushes.Transparent;
    }

    public override void LoadDefault()
    {
        base.LoadDefault();
        this.MinRowHeight = (double)Application.Current.FindResource(LayoutKeys.RowHeight);
    }
    private Brush _lineBrush;
    [DefaultValue(null)]
    [Display(Name = "网线颜色", GroupName = "常用,样式")]
    public Brush LineBrush
    {
        get { return _lineBrush; }
        set
        {
            _lineBrush = value;
            RaisePropertyChanged();
        }
    }

    private double _lineThickness = 1.0;
    [DefaultValue(1.0)]
    [Display(Name = "网线粗细", GroupName = "常用,样式")]
    public double LineThickness
    {
        get { return _lineThickness; }
        set
        {
            _lineThickness = value;
            RaisePropertyChanged();
        }
    }

    private double _minRowHeight;
    [Display(Name = "最小行高", GroupName = "常用,样式")]
    public double MinRowHeight
    {
        get { return _minRowHeight; }
        set
        {
            _minRowHeight = value;
            RaisePropertyChanged();
        }
    }

    public override void DragEnter(UIElement element, DragEventArgs e)
    {
        //IDraggableAdorner adorner = e.Data.GetData("DragGroup") as IDraggableAdorner;
        //if (adorner.GetData() is ICloneable cloneable)
        //{
        //    object value = cloneable.Clone();
        //    if (value is DesignPresenterBase design)
        //    {
        //        System.Diagnostics.Debug.WriteLine("DragEnter");
        //        var grid = element.GetChild<Grid>();
        //        if (grid.HitTestRow(out int r) && grid.HitTestColumn(out int c))
        //        {
        //            design.Row = r;
        //            design.Column = c;
        //            System.Diagnostics.Debug.WriteLine($"{r}_{c}");
        //        }
        //    }
        //    this.Presenters.Add(value);
        //    _dropBackup = value;
        //}
    }

    public override bool IsHitTest(UIElement element, DragEventArgs e)
    {
        if (this.IsHitTestVisible == false)
            return false;
        var current = element.GetContent();
        if (current == _droppingDesignPresenter)
            return false;
        if (current is not PanelPresenterBase)
            return false;
        return !_droppingDesignPresenter.GetChildrenDesignPresenters().Contains(current);
    }

    private BorderDefinitionPresenter _activeBorder = new BorderDefinitionPresenter();
    public override void DragOver(UIElement element, DragEventArgs e)
    {
        var p = e.GetPosition(element);
        var grid = element is Grid ? element as Grid : element.GetChild<Grid>();
        if (_droppingDesignPresenter == null)
        {
            IDraggableAdorner adorner = e.Data.GetData("DragGroup") as IDraggableAdorner;
            if (adorner.GetData() is IDesignPresenter value)
            {
                if (grid.HitTestRow(p, out int r) && grid.HitTestColumn(p, out int c))
                {
                    value.Row = r;
                    value.Column = c;
                    value.Opacity = 0.5;
                    value.IsHitTestVisible = false;
                    _activeBorder.Row = r;
                    _activeBorder.Column = c;
                }
                this.Presenters.Add(value);
                this.Presenters.Add(this._activeBorder);
                _droppingDesignPresenter = value;
            }
        }
        else
        {
            if (grid.HitTestRow(p, out int r) && grid.HitTestColumn(p, out int c))
            {
                if (_droppingDesignPresenter.Row == r && _droppingDesignPresenter.Column == c)
                    return;
                _droppingDesignPresenter.Row = r;
                _droppingDesignPresenter.Column = c;
                _activeBorder.Row = r;
                _activeBorder.Column = c;
                //this.Presenters.Remove(_dropBackup);
                //this.Presenters.Add(_dropBackup);
                element.InvalidateVisual();
            }
        }
    }

    public override void Drop(UIElement element, DragEventArgs e)
    {
        this.Presenters.Remove(this._activeBorder);
        base.Drop(element, e);
    }

    public override void DragLeave(UIElement element, DragEventArgs e)
    {
        this.Presenters.Remove(this._activeBorder);
        base.DragLeave(element, e);
    }
}
