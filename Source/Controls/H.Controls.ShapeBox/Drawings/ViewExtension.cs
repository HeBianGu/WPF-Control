// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using System.Collections.Generic;
global using System.Linq;
using H.Extensions.Common;

namespace H.Controls.ShapeBox.Drawings;
public static class ViewExtension
{
    public static double ToView(this double value, IView view)
    {
        return value * view.Scale;
    }
    public static double ToGeo(this double value, IView view)
    {
        return value / view.Scale;
    }

    public static int ToView(this int value, IView view)
    {
        return (int)(1.0 * value).ToView(view);
    }
    public static int ToGeo(this int value, IView view)
    {
        return (int)(1.0 * value).ToGeo(view);
    }
}
