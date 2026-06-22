// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Common.Interfaces;

namespace H.Modules.Messages.Dialog
{
    public static class LayoutableExtension
    {
        public static void CopyFrom(this ILayoutable layoutable, IDesignPresenter from)
        {
            layoutable.HorizontalAlignment = from.HorizontalAlignment;
            layoutable.VerticalAlignment = from.VerticalAlignment;
            layoutable.HorizontalContentAlignment = from.HorizontalContentAlignment;
            layoutable.VerticalContentAlignment = from.VerticalContentAlignment;
            layoutable.Height = from.Height;
            layoutable.Width = from.Width;
            layoutable.Padding = from.Padding;
            layoutable.Margin = from.Margin;
            layoutable.MinWidth = from.MinWidth;
            layoutable.MinHeight = from.MinHeight;
            layoutable.BorderBrush = from.BorderBrush;
            layoutable.BorderThickness = from.BorderThickness;
            layoutable.Background = from.Background;
            layoutable.IsEnabled = from.IsEnabled;
            layoutable.Opacity = from.Opacity;
        }
    }
}
