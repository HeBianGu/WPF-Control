// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using Microsoft.Xaml.Behaviors;

namespace H.Controls.ZoomBox.Extension
{
    public class ZoomBoxFitOnSizeChangedBehavior : Behavior<Zoombox>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.SizeChanged += this.AssociatedObject_SizeChanged;
        }
        private void AssociatedObject_SizeChanged(object sender, System.Windows.SizeChangedEventArgs e)
        {
            this.AssociatedObject.FitToBounds();
        }
        protected override void OnDetaching()
        {
            base.OnDetaching();
            this.AssociatedObject.SizeChanged -= this.AssociatedObject_SizeChanged;
        }
    }
}