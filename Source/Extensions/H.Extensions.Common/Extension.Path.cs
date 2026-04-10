// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Extensions.Common;

public static class PathExtension
{
    public static string GetFileName(this string filePath)
    {
        return System.IO.Path.GetFileName(filePath);
    }

    public static string GetFileNameWithoutExtension(this string filePath)
    {
        return System.IO.Path.GetFileNameWithoutExtension(filePath);
    }
    public static string GetDirectoryName(this string filePath)
    {
        return System.IO.Path.GetDirectoryName(filePath);
    }
    public static string GetExtension(this string filePath)
    {
        return System.IO.Path.GetExtension(filePath);
    }
    public static string GetFullPath(this string filePath)
    {
        return System.IO.Path.GetFullPath(filePath);
    }
    public static string GetPathRoot(this string filePath)
    {
        return System.IO.Path.GetPathRoot(filePath);
    }
    public static string GetRelativePath(this string filePath, string relativeTo)
    {
        return System.IO.Path.GetRelativePath(relativeTo, filePath);
    }

    public static string GetRelativeStartsWithPath(this string filePath, string relativeTo)
    {
        if (filePath.StartsWith(relativeTo, StringComparison.OrdinalIgnoreCase))
            return filePath.GetRelativePath(relativeTo);
        return filePath;
    }

    public static string GetAppDomainRelativePath(this string filePath)
    {
        string currentDir = AppDomain.CurrentDomain.BaseDirectory;
        return filePath.GetRelativeStartsWithPath(AppDomain.CurrentDomain.BaseDirectory);
    }
   
}
