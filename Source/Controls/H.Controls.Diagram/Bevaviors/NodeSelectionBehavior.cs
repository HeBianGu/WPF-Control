// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using Microsoft.Xaml.Behaviors;
using System.Windows;

namespace H.Controls.Diagram.Bevaviors;

///// <summary> Diagram接收放下的操作 </summary>
//public class NodeSelectionAdornerBehavior : Behavior<PortNode>
//{
//    protected override void OnAttached()
//    {
//        this.AssociatedObject.SelectionChanged += AssociatedObject_SelectionChanged;
//    }

//    private void AssociatedObject_SelectionChanged(object sender, RoutedEventArgs e)
//    {
//        this.AssociatedObject.GetPorts(l => l.IsSystem).ForEach(l => l.Visibility = this.AssociatedObject.IsSelected ? Visibility.Visible : Visibility.Hidden);

//        //this.AssociatedObject.GetPorts(l => l.IsSystem).ForEach(l => l.Visibility = Visibility.Visible);
//    }
//    protected override void OnDetaching()
//    {
//        this.AssociatedObject.SelectionChanged -= AssociatedObject_SelectionChanged;
//    } 
//}

/// <summary> Diagram接收放下的操作 </summary>
public class NodeSelectionBehavior : Behavior<Node>
{
    protected override void OnAttached()
    {
        this.AssociatedObject.SelectionChanged += AssociatedObject_SelectionChanged;
    }

    private void AssociatedObject_SelectionChanged(object sender, RoutedEventArgs e)
    {
        if (this.AssociatedObject.IsSelected)
            this.AssociatedObject.GetPorts(l => l.PortType == PortType.OutPut || l.PortType == PortType.Both).ForEach(l => l.Visibility = Visibility.Visible);
        else
        {
            this.AssociatedObject.GetPorts(l => l.PortType == PortType.OutPut || l.PortType == PortType.Both).ForEach(l => l.Visibility = Visibility.Hidden);
        }

    }
    protected override void OnDetaching()
    {
        this.AssociatedObject.SelectionChanged -= AssociatedObject_SelectionChanged;
    }
}
