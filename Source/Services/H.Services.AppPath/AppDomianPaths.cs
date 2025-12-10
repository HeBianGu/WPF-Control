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
    public static string Modules => "Modules";
    public static string Components => "Components";
    public static string Versions => "Versions";
}
