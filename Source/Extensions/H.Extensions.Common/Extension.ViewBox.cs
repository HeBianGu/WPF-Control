// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Windows;
using System.Windows.Controls;

namespace H.Extensions.Common;

public static class ViewBoxExtension
{
    // 获取 ViewBox 的缩放比例
    public static double GetViewBoxScale(this Viewbox viewBox)
    {
        if (viewBox.Child is FrameworkElement child)
        {
            double scaleX = viewBox.ActualWidth / child.ActualWidth;
            double scaleY = viewBox.ActualHeight / child.ActualHeight;

            // ViewBox 默认使用 Uniform 缩放模式，取较小的比例
            return Math.Min(scaleX, scaleY);
        }
        return 1.0;
    }
}
