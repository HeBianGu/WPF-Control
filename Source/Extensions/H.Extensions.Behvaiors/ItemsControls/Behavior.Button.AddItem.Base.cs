// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Collections;

namespace H.Extensions.Behvaiors.ItemsControls;

public abstract class AddItemButtonBehaviorBase : ButtonBehaviorBase
{
    public object DefaultValue
    {
        get { return GetValue(DefaultValueProperty); }
        set { SetValue(DefaultValueProperty, value); }
    }

    public static readonly DependencyProperty DefaultValueProperty =
        DependencyProperty.Register("DefaultValue", typeof(object), typeof(AddItemButtonBehaviorBase), new FrameworkPropertyMetadata(default, (d, e) =>
        {
            AddItemButtonBehaviorBase control = d as AddItemButtonBehaviorBase;

            if (control == null) return;

            if (e.OldValue is object o)
            {

            }

            if (e.NewValue is object n)
            {

            }

        }));

    protected object CreateNewItem()
    {
        if (this.DefaultValue == null && this.ItemsSource == null)
            return null;
        if (this.ItemsSource == null)
            this.ItemsControl.ItemsSource = this.DefaultValue.GetType().CreateObservableCollection<IList>();
        if (this.DefaultValue is ICloneable cloneable)
            return cloneable.Clone();
        if (this.ItemsSource.TryCreateItem(out object instance))
            return instance;
        return null;
    }
}