// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

#if NET
#endif 
namespace H.Extensions.Behvaiors.Adorners;

public class LoadedAdornerBehavior : AdornerBehaviorBase
{
    protected override void OnAttached()
    {
        this.AssociatedObject.Loaded += AssociatedObject_Loaded;
    }

    private void AssociatedObject_Loaded(object sender, RoutedEventArgs e)
    {
        Refresh();
    }

    private void Refresh()
    {
        if (this.AdornerType == null) return;

        if (this.AdornerVisual == null)
            this.AdornerVisual = this.AssociatedObject;

        AdornerLayer layer = AdornerLayer.GetAdornerLayer(this.AdornerVisual);
        if (layer == null) return;

        IEnumerable<Adorner> adorners = layer.GetAdorners(this.AdornerVisual as UIElement)?.Where(l => l.GetType() == this.AdornerType);

        if (adorners != null)
        {
            foreach (Adorner item in adorners)
            {
                layer.Remove(item);
            }
        }

        if (this.IsUse)
        {
            Adorner adorner = Activator.CreateInstance(this.AdornerType, this.AssociatedObject) as Adorner;
            if (adorner == null)
                return;
            layer.Add(adorner);
        }
    }

    protected override void OnDetaching()
    {
        this.AssociatedObject.Loaded -= AssociatedObject_Loaded;
    }

}

