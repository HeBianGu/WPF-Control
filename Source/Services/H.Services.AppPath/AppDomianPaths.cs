// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Services.AppPath;

public static class AppDomianPaths
{
    public const string Assets = nameof(Assets);
    public static string DefaultTemplates => Path.Combine(Assets, "DefaultTemplates");
    public static string DefaultProjects => Path.Combine(DefaultTemplates, "Project");
    public static string DefaultSettings => Path.Combine(DefaultTemplates, "Setting");
    public static string Modules => "Modules";
    public static string Components => "Components";
    public static string Versions => "Versions";
}

public static class AppDomianPathExtensions
{
    public static string ToDefaultTemplatePath(this string path, string relativePath)
    {
        var rpath = Path.GetRelativePath(relativePath, path);
        var folderName = Path.GetFileNameWithoutExtension(relativePath);
        return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AppDomianPaths.DefaultTemplates, folderName, rpath);
    }
}
