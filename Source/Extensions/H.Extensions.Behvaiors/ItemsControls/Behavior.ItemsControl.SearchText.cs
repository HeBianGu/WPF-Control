// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Extensions.Behvaiors.ItemsControls;

public class ItemsControlSearchTextBehavior : Behavior<ItemsControl>
{

    private readonly Predicate<object> _predicate;
    public ItemsControlSearchTextBehavior()
    {
        this._predicate = (o) =>
        {
            if (string.IsNullOrEmpty(this.SearchText))
                return true;
            if (o == null)
                return false;
            return o.ToString().Contains(this.SearchText, StringComparison.OrdinalIgnoreCase);
        };
    }
    public string SearchText
    {
        get { return (string)GetValue(SearchTextProperty); }
        set { SetValue(SearchTextProperty, value); }
    }

    public static readonly DependencyProperty SearchTextProperty =
        DependencyProperty.Register("SearchText", typeof(string), typeof(ItemsControlSearchTextBehavior), new FrameworkPropertyMetadata(default(string), (d, e) =>
        {
            ItemsControlSearchTextBehavior control = d as ItemsControlSearchTextBehavior;

            if (control == null) return;

            if (e.OldValue is string o)
            {

            }

            if (e.NewValue is string n)
            {

            }
            if (control.AssociatedObject?.Items == null)
                return;
            Predicate<object> predicate = (o) =>
            {
                if (string.IsNullOrEmpty(control.SearchText))
                    return true;
                if (o == null)
                    return false;
                return o.ToString().Contains(control.SearchText, StringComparison.OrdinalIgnoreCase);
            };
            control.AssociatedObject.Items.Filter = predicate;
        }));
    //protected override void OnAttached()
    //{
    //    base.OnAttached();
    //    this.AssociatedObject.Items.Filter += _predicate;
    //}

    //protected override void OnDetaching()
    //{
    //    base.OnDetaching();
    //    this.AssociatedObject.Items.Filter += _predicate;
    //}
}