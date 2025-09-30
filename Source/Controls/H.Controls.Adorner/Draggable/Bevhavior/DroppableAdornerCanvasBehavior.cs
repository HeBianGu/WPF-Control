// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

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

