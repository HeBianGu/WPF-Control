// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Common.Interfaces;

namespace H.Presenters.Design;

public class DesignSelectedHitTestAdornerBehavior : SelectedHitTestAdornerBehavior
{
    public IDesignPresenter SelectedDesignPresenter
    {
        get { return (IDesignPresenter)GetValue(SelectedDesignPresenterProperty); }
        set { SetValue(SelectedDesignPresenterProperty, value); }
    }

    public static readonly DependencyProperty SelectedDesignPresenterProperty =
        DependencyProperty.Register("SelectedDesignPresenter", typeof(IDesignPresenter), typeof(DesignSelectedHitTestAdornerBehavior), new FrameworkPropertyMetadata(default(IDesignPresenter), (d, e) =>
        {
            DesignSelectedHitTestAdornerBehavior control = d as DesignSelectedHitTestAdornerBehavior;

            if (control == null) return;

            if (e.OldValue is IDesignPresenter o)
            {

            }

            if (e.NewValue is IDesignPresenter n)
            {

            }

        }));

    protected override UIElement HitElement(Point point)
    {
        var all = this.AssociatedObject.HitTestAllByFilter<UIElement>(point, x => GetIsHitTest(x));
        var children = all.Where(x => x.GetContent() is not IChildableDesignPresenter);
        var result = children.FirstOrDefault();
        if (result == null)
            result = all.Where(x => x.GetContent() is IChildableDesignPresenter)?.LastOrDefault();
        if (result.GetContent() is IDesignPresenter presenter)
            this.SelectedDesignPresenter = presenter;
        return result;
    }
}
