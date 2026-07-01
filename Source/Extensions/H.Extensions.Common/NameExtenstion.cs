// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")
using System.Linq;

namespace H.Extensions.Common;

public static class NameExtenstion
{
    public static string GetIndexSafeName(this string formatName, IEnumerable<string> names)
    {
        for (int i = 1; ; i++)
        {
            string candidate = formatName + i.ToString();
            if (!names.Contains(candidate))
                return candidate;
        }
    }

    public static string GetIndexSafeName(this IEnumerable<string> names, string formatName)
    {
        for (int i = 1; ; i++)
        {
            string candidate = formatName + i.ToString();
            if (!names.Contains(candidate))
                return candidate;
        }
    }

    public static int GetSafeIndex(this IEnumerable<int> indexs)
    {
        for (int i = 1; ; i++)
        {
            if (!indexs.Contains(i))
                return i;
        }
    }
}
