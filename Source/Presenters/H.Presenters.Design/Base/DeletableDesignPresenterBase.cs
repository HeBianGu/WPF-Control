// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Common.Interfaces;
using H.Mvvm.Commands;
using System.Collections;
using System.Windows.Input;

namespace H.Presenters.Design.Base;

public abstract class DeletableDesignPresenterBase : DesignPresenterBase
{
    [Display(Name = "删除")]
    public ICommand DeleteCommand => new RelayCommand(e =>
    {
        Delete(e);
    });

    protected virtual void Delete(object e)
    {
        if (e is ContentControl c)
        {
            Adorner adorner = c.GetParent<Adorner>();
            FrameworkElement element = adorner.AdornedElement.GetParent<FrameworkElement>(x =>
            {
                return x.GetContent() is IChildableDesignPresenter childable1&&childable1!= c.Content;
            });

            if (element?.GetContent() is IChildableDesignPresenter childable && c.Content is IDesignPresenter item)
                childable.Delete(item);
            else
            {
                adorner.AdornedElement.GetItemsSource<IList>()?.Remove(c.Content);
                ItemsControl itemsControl = adorner.AdornedElement.GetParent<ItemsControl>();
                itemsControl.GetItemsSource<IList>()?.Remove(c.Content);
            }
        }
    }
}
