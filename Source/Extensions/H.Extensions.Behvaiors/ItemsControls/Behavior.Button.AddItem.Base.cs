// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Collections;
using System.Windows;

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