// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using System.Windows;
using System.Windows.Controls;

namespace H.Controls.Adorner.Draggable.Bevhavior;

public class DroppableAdornerCanvasBehavior : DropableAdornerBehavior<Canvas>
{
    protected override void DropElement(UIElement element, Point location, Point offset)
    {
        this.AssociatedObject.Children.Add(element);

        Canvas.SetLeft(element, location.X - offset.X);

        Canvas.SetTop(element, location.Y - offset.Y);
    }
}



