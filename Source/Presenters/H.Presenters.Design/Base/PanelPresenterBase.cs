// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Common.Interfaces;
using H.Extensions.FontIcon;
using System.Windows.Markup;
using System.Xml.Serialization;

namespace H.Presenters.Design.Base;

[Icon(FontIcons.Tiles)]
[ContentProperty("Presenters")]
[DefaultProperty("Presenters")]
public abstract class PanelPresenterBase : DropAdornerDesignPresenterBase, IPanelDesignPresenter
{
    public PanelPresenterBase()
    {
        //this.MinHeight = 100;
        this.MinWidth = 50;
    }

    private ObservableCollection<IDesignPresenter> _presenters = new ObservableCollection<IDesignPresenter>();
    [Browsable(false)]
    public ObservableCollection<IDesignPresenter> Presenters
    {
        get { return _presenters; }
        set
        {
            _presenters = value;
            RaisePropertyChanged();
        }
    }

    private HorizontalAlignment _childrenHorizontalAlignment = HorizontalAlignment.Stretch;
    [Display(Name = "水平内部对齐", GroupName = "常用,样式")]
    public HorizontalAlignment ChildrenHorizontalAlignment
    {
        get { return _childrenHorizontalAlignment; }
        set
        {
            _childrenHorizontalAlignment = value;
            RaisePropertyChanged();
        }
    }

    private VerticalAlignment _childrenVerticalAlignment = VerticalAlignment.Stretch;
    [Display(Name = "垂直内部对齐", GroupName = "常用,样式")]
    public VerticalAlignment ChildrenVerticalAlignment
    {
        get { return _childrenVerticalAlignment; }
        set
        {
            _childrenVerticalAlignment = value;
            RaisePropertyChanged();
        }
    }

    private double _cacheOpacity = 1.0;
    public override void DragEnter(UIElement element, DragEventArgs e)
    {
        IDraggableAdorner adorner = e.Data.GetData("DragGroup") as IDraggableAdorner;
        if (adorner.GetData() is IDesignPresenter value)
        {
            this.Presenters.Add(value);
            this._cacheOpacity = value.Opacity;
            _droppingDesignPresenter = value;
            _droppingDesignPresenter.Opacity = 0.5;
            _droppingDesignPresenter.IsHitTestVisible = false;
        }
    }

    protected IDesignPresenter _droppingDesignPresenter;
    public override void Drop(UIElement element, DragEventArgs e)
    {
        if (_droppingDesignPresenter == null)
            return;
        this.Presenters.Remove(_droppingDesignPresenter);
        _droppingDesignPresenter.Opacity = this._cacheOpacity;
        if (_droppingDesignPresenter is ICloneable cloneable && cloneable.Clone() is IDesignPresenter clone)
        {
            clone.IsHitTestVisible = true;

            this.Presenters.Add(clone);
        }
        _droppingDesignPresenter = null;
        this._cacheOpacity = 1.0;

    }

    public override void DragLeave(UIElement element, DragEventArgs e)
    {
        this.Presenters.Remove(_droppingDesignPresenter);
        _droppingDesignPresenter = null;
    }

    public void Delete(IDesignPresenter designPresenter)
    {
        if (this.Presenters.Contains(designPresenter))
            this.Presenters.Remove(designPresenter);
    }

    public override ICloneableDesignPresenter Clone()
    {
        var r = base.Clone();
        if (r is PanelPresenterBase panel)
        {
            foreach (var item in this.Presenters)
            {
                if (item is ICloneable cloneable && cloneable.Clone() is IDesignPresenter clone)
                    panel.Presenters.Add(clone);
            }
        }
        return r;
    }
}
