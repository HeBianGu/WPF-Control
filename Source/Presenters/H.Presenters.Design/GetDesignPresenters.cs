// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Common.Interfaces;
using H.Presenters.Design.Presenter;
using System.Windows.Markup;
namespace H.Presenters.Design;
public class GetDesignPresenters : MarkupExtension
{
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return this.GetType().GetInstances<IDesignPresenter>().ToList();
    }
}

public class DesignPresenterSelectedBehavior : SelectedHitTestAdornerBehavior
{
    [Obsolete]
    public IDesignPresenter SelectedDesignPresenter
    {
        get { return (IDesignPresenter)GetValue(SelectedDesignPresenterProperty); }
        set { SetValue(SelectedDesignPresenterProperty, value); }
    }

    [Obsolete]
    public static readonly DependencyProperty SelectedDesignPresenterProperty =
        DependencyProperty.Register("SelectedDesignPresenter", typeof(IDesignPresenter), typeof(DesignPresenterSelectedBehavior), new FrameworkPropertyMetadata(default(IDesignPresenter), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (d, e) =>
        {
            DesignPresenterSelectedBehavior control = d as DesignPresenterSelectedBehavior;

            if (control == null) return;

            if (e.OldValue is IDesignPresenter o)
            {

            }

            if (e.NewValue is IDesignPresenter n)
            {
                if (control.SelectedElement.GetDataContext() == n)
                    return;
                control.SelectedElement = control.GetElement(n);
            }
            else
            {
                control.SelectedElement = null;
            }
        }));

    private UIElement GetElement(IDesignPresenter designPresenter)
    {
        return this.AssociatedObject.GetChild<DesignBorder>(x => x.GetDataContext() == designPresenter);
    }

    [Obsolete]
    protected override void OnSelectdElementChanged()
    {
        base.OnSelectdElementChanged();
        this.SelectedDesignPresenter = this.SelectedElement?.GetDataContext() as IDesignPresenter;
    }

}
