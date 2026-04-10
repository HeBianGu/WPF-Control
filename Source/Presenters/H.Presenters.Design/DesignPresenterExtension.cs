// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Common.Interfaces;
using H.Presenters.Design.PrintPresenter;

namespace H.Presenters.Design;

public static class DesignPresenterExtension
{
    public static IEnumerable<IDesignPresenter> GetChildrenDesignPresenters(this IDesignPresenter designPresenter)
    {
        if (designPresenter is DefinitionGridPrintPagePresenter definition)
        {
            yield return definition;
            foreach (var item in GetChildrenDesignPresenters(definition.Content))
            {
                yield return item;
            }
        }
        if (designPresenter is IPanelDesignPresenter panelDesignPresenter)
        {
            yield return panelDesignPresenter;
            foreach (var item in panelDesignPresenter.Presenters)
            {
                yield return item;
                foreach (var child in GetChildrenDesignPresenters(item))
                {
                    yield return child;
                }
            }
        }
    }

    public static IEnumerable<IDesignPresenter> GetChildrenDesignPresenters(this IDesignPresenter designPresenters, Predicate<IDesignPresenter> predicate = null)
    {
        if (predicate?.Invoke(designPresenters) != false)
            yield return designPresenters;
        foreach (var child in GetChildrenDesignPresenters(designPresenters))
            if (predicate?.Invoke(child) != false)
                yield return child;
    }

    public static IEnumerable<IDesignPresenter> GetChildrenDesignPresenters(this IEnumerable<IDesignPresenter> designPresenters, Predicate<IDesignPresenter> predicate = null)
    {
        foreach (var item in designPresenters)
            foreach (var child in GetChildrenDesignPresenters(item, predicate))
                yield return child;
    }
}
