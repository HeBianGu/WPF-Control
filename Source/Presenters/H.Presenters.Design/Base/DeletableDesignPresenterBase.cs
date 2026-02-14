// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Common.Interfaces;
using H.Extensions.FontIcon;
using H.Extensions.Mvvm.Commands;
using H.Mvvm.Commands;
using H.Services.Message;
using System.Collections;
using System.Windows.Documents;
using System.Windows.Input;

namespace H.Presenters.Design.Base;

public abstract class DeletableDesignPresenterBase : DesignPresenterBase, ILockableDesignPresenter
{
    [Icon(FontIcons.Delete)]
    [Display(Name = "删除")]
    public DisplayCommand DeleteCommand => new DisplayCommand(e =>
    {
        Delete(e);
    });

    private bool _Locked = false;
    [Display(Name = "锁定", GroupName = "常用,布局")]
    public bool Locked
    {
        get { return _Locked; }
        set
        {
            _Locked = value;
            RaisePropertyChanged();
        }
    }

    private bool _UseTool = true;
    [Browsable(false)]
    [ReadOnly(true)]
    [Display(Name = "启用工具栏", GroupName = "数据")]
    public bool UseTool
    {
        get { return _UseTool; }
        set
        {
            _UseTool = value;
            RaisePropertyChanged();
        }
    }

    protected virtual void Delete(object e)
    {
        UIElement helement = null;
        object content = null;
        // Adorner
        if (e is ContentControl c)
        {
            Adorner adorner = c.GetParent<Adorner>();
            helement = adorner.AdornedElement;
            content = c.Content;
        }
        // ContextMenu
        if (e is ContextMenu b)
        {
            helement = b.PlacementTarget;
            content = b.GetContent();
        }

        FrameworkElement element = helement.GetParent<FrameworkElement>(x =>
        {
            return x.GetContent() is IChildableDesignPresenter childable1 && childable1 != content;
        });

        if (element?.GetContent() is IChildableDesignPresenter childable && content is IDesignPresenter item)
            childable.Delete(item);
        else
        {
            helement.GetItemsSource<IList>()?.Remove(content);
            ItemsControl itemsControl = helement.GetParent<ItemsControl>();
            itemsControl.GetItemsSource<IList>()?.Remove(content);
        }
    }
}
