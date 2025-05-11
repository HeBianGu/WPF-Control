// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.Adorner.Adorner.Base;

public abstract class VisualCollectionAdornerBase : AdornerBase
{
    protected VisualCollection _visualCollection;

    public VisualCollectionAdornerBase(UIElement adornedElement) : base(adornedElement)
    {
        _visualCollection = new VisualCollection(this);
    }

    protected override Visual GetVisualChild(int index)
    {
        return _visualCollection[index];
    }
    protected override int VisualChildrenCount
    {
        get
        {
            return _visualCollection.Count;
        }
    }
}
