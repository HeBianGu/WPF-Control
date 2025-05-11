// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Services.Setting;

namespace H.Modules.About;

/// <summary>
/// 提供关于选项的接口。
/// </summary>
public interface IAboutOptions : ISettable
{
    /// <summary>
    /// 获取或设置协议。
    /// </summary>
    string Agreement { get; set; }

    /// <summary>
    /// 获取或设置程序集版本。
    /// </summary>
    string AssemblyVersion { get; set; }

    /// <summary>
    /// 获取或设置公司名称。
    /// </summary>
    string Company { get; set; }

    /// <summary>
    /// 获取或设置配置。
    /// </summary>
    string Configuration { get; set; }

    /// <summary>
    /// 获取或设置版权信息。
    /// </summary>
    string Copyright { get; set; }

    /// <summary>
    /// 获取或设置文化信息。
    /// </summary>
    string Culture { get; set; }

    /// <summary>
    /// 获取或设置文件版本。
    /// </summary>
    string FileVersion { get; set; }

    /// <summary>
    /// 获取或设置隐私政策。
    /// </summary>
    string Privacy { get; set; }

    /// <summary>
    /// 获取或设置产品描述。
    /// </summary>
    string ProductDescription { get; set; }

    /// <summary>
    /// 获取或设置产品名称。
    /// </summary>
    string ProductName { get; set; }

    /// <summary>
    /// 获取或设置商标信息。
    /// </summary>
    string Trademark { get; set; }

    /// <summary>
    /// 获取或设置版本信息。
    /// </summary>
    string Version { get; set; }
}