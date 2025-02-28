// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Input;

namespace H.Controls.Diagram.Bevaviors;

/// <summary> Diagram接收放下的操作 </summary>
public class NodeMouseOverBehavior : Behavior<Node>
{
    protected override void OnAttached()
    {
        this.AssociatedObject.MouseEnter += AssociatedObject_MouseEnter;
        this.AssociatedObject.MouseLeave += AssociatedObject_MouseLeave;
    }

    private void AssociatedObject_MouseLeave(object sender, MouseEventArgs e)
    {
        //this.AssociatedObject.AdornerVisible(false);

        //this.AssociatedObject.Ports.ForEach(l=>l.Visibility=Visibility.Collapsed);

        //this.AssociatedObject.ShowSystemPort(false);

        this.AssociatedObject.GetPorts().ForEach(l => l.Visibility = Visibility.Hidden);
    }

    private void AssociatedObject_MouseEnter(object sender, MouseEventArgs e)
    {
        //this.AssociatedObject.AdornerVisible(true);

        //this.AssociatedObject.Ports.ForEach(l => l.Visibility = Visibility.Visible);

        //this.AssociatedObject.ShowSystemPort(true);

        this.AssociatedObject.GetPorts(l => l.PortType == PortType.OutPut || l.PortType == PortType.Both).ForEach(l => l.Visibility = Visibility.Visible);

    }


    protected override void OnDetaching()
    {
        //this.AssociatedObject.MouseEnter -= AssociatedObject_MouseEnter;

        //this.AssociatedObject.Adorner.MouseLeave -= AssociatedObject_MouseLeave;
    }



}
