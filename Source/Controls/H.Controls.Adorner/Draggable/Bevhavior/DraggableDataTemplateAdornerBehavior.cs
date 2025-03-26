// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control



using H.Controls.Adorner.Adorner.DataTemplateAdorners;
using Microsoft.Xaml.Behaviors;
using System.Windows;

namespace H.Controls.Adorner.Draggable.Bevhavior;

public class DraggableDataTemplateAdornerBehavior : DraggableAdornerBehaviorBase
{
    protected override IDraggableAdorner CreateDragAdorner(UIElement adornerElement, Point offset)
    {
        //从AssociatedObject取DataTemplate数据
        object data = DataTemplateAdorner.GetData(this.AssociatedObject);
        if (data == null)
            data = this.AssociatedObject.GetContent();
        return new DragableDataTemplateAdorner(adornerElement, data, offset)
        {
            IsHitTestVisible = false,
            Opacity = this.Opacity,
            DropAdornerMode = this.DropAdornerMode
        };

    }

    public static bool GetIsUse(DependencyObject obj)
    {
        return (bool)obj.GetValue(IsUseProperty);
    }

    public static void SetIsUse(DependencyObject obj, bool value)
    {
        obj.SetValue(IsUseProperty, value);
    }

    public static readonly DependencyProperty IsUseProperty =
        DependencyProperty.RegisterAttached("IsUse", typeof(bool), typeof(DraggableDataTemplateAdornerBehavior), new PropertyMetadata(default(bool), OnIsUseChanged));

    public static void OnIsUseChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        DependencyObject control = d;
        bool n = (bool)e.NewValue;
        bool o = (bool)e.OldValue;
        RefreshUse(d, n);
    }

    private static void RefreshUse(DependencyObject d, bool use)
    {
        BehaviorCollection bevaviors = Interaction.GetBehaviors(d);
        IEnumerable<Behavior> finds = bevaviors.Where(x => x.GetType() == typeof(DraggableDataTemplateAdornerBehavior));
        foreach (Behavior item in finds.ToList())
        {
            bevaviors.Remove(item);
        }
        if (use)
            bevaviors.Add(new DraggableDataTemplateAdornerBehavior());
    }
}



