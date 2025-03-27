// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Windows;

namespace H.Extensions.Behvaiors.ItemsControls;

public class ButtonInsertItemBehavior : AddItemButtonBehaviorBase
{
    public int Index
    {
        get { return (int)GetValue(IndexProperty); }
        set { SetValue(IndexProperty, value); }
    }

    public static readonly DependencyProperty IndexProperty =
        DependencyProperty.Register("Index", typeof(int), typeof(ButtonInsertItemBehavior), new FrameworkPropertyMetadata(0, (d, e) =>
         {
             ButtonInsertItemBehavior control = d as ButtonInsertItemBehavior;

             if (control == null) return;

             if (e.OldValue is int o)
             {

             }

             if (e.NewValue is int n)
             {

             }

         }));

    protected override void OnClick()
    {
        object addItem = CreateNewItem();
        if (addItem == null)
            return;
        this.ItemsSource.Insert(this.Index, addItem);
    }
}