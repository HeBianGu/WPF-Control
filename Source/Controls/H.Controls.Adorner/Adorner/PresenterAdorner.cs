// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Common.Interfaces;
using H.Controls.Adorner.Adorner.Base;

namespace H.Controls.Adorner.Adorner;

public class PresenterAdorner : VisualCollectionAdornerBase
{
    private ContentPresenter _contentPresenter = new ContentPresenter();
    public PresenterAdorner(UIElement adornedElement, object presenter) : base(adornedElement)
    {
        _contentPresenter.Content = presenter;
        _visualCollection.Add(_contentPresenter);
    }

    public ContentPresenter ContentPresenter => this._contentPresenter;
    public object Presenter => this._contentPresenter.Content;
    protected override Size MeasureOverride(Size constraint)
    {
        this._contentPresenter.Measure(this.AdornedElement.RenderSize);
        return new Size(Math.Max(this._contentPresenter.DesiredSize.Width, this.AdornedElement.DesiredSize.Width), Math.Max(this._contentPresenter.DesiredSize.Height, this.AdornedElement.DesiredSize.Height));
    }

    protected override Size ArrangeOverride(Size finalSize)
    {
        this._contentPresenter.Arrange(new Rect(this.AdornedElement.RenderSize));
        return base.ArrangeOverride(finalSize);
    }

    public static UIElement GetAdonerElement(UIElement element = null)
    {
        if (element == null)
        {
            if (Application.Current.MainWindow is IAdornerDialogElement adorner)
                return adorner.GetElement();
            return Application.Current.MainWindow.Content as UIElement;
        }
        if (element is IAdornerDialogElement dialogElement)
            return dialogElement.GetElement();
        return element;
    }
}
