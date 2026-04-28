// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")
using System.Windows;
using System.Windows.Media;

namespace H.Extensions.Common;

public static class PenExtenstion
{
    public static Brush ToBrush(this Pen pen, Action<Brush> action = null)
    {
        var clone = pen.Brush.Clone();
        action?.Invoke(clone);
        if (clone.CanFreeze)
            clone.Freeze();
        return clone;
    }
}
