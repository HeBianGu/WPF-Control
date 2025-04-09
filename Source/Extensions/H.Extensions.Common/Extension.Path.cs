// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

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
    public static string GetFileName(this string filePath, string relativeTo)
    {
        return System.IO.Path.GetRelativePath(relativeTo,filePath);
    }
}
