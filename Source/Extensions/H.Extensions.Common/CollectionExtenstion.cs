// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")
using System.Windows.Media;

namespace H.Extensions.Common;

public static class CollectionExtenstion
{
    public static Int32Collection ToCollection(this IEnumerable<int> value, Action<Int32Collection> action = null)
    {
        var r = new Int32Collection(value);
        action?.Invoke(r);
        return r;
    }

    public static DoubleCollection ToCollection(this IEnumerable<double> value, Action<DoubleCollection> action = null)
    {
        var r = new DoubleCollection(value);
        action?.Invoke(r);
        return r;
    }
}
