// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Iocable;
using H.Services.AppPath;
using H.Services.Identity;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Reflection;

namespace H.Extensions.AppPath;
/// <summary>
/// 系统路径
/// </summary>
[Display(Name = "系统路径")]
public class AppPathServce : Lazy<AppPathServce>, IAppPathServce
{
    /// <summary>
    /// 公司名称
    /// </summary>
    public virtual string Company { get; set; } = "HeBianGu";

    /// <summary>
    /// 构造函数
    /// </summary>
    public AppPathServce()
    {
        this.CheckFolder(this.AppPath);
        this.CheckFolder(this.Default);
        this.CheckFolder(this.Config);
        this.CheckFolder(this.Data);
        this.CheckFolder(this.Setting);
        this.CheckFolder(this.License);
        this.CheckFolder(this.Log);
        this.CheckFolder(this.Project);
        this.CheckFolder(this.Template);
        this.CheckFolder(this.Setting);
        this.CheckFolder(this.Cache);
    }

    #region - 基础目录 -

    /// <summary>
    /// 文档目录
    /// </summary>
    public virtual string Document { get; set; } = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

    /// <summary>
    /// 应用程序名称
    /// </summary>
    public virtual string AppName => Assembly.GetEntryAssembly()?.GetName()?.Name;

    /// <summary>
    /// 应用程序路径
    /// </summary>
    public virtual string AppPath => Path.Combine(this.Document, this.Company, this.AppName);

    /// <summary>
    /// 默认目录
    /// </summary>
    public virtual string Default
    {
        get
        {
            if (string.IsNullOrEmpty(this.Version))
                return Path.Combine(this.Document, this.Company, this.AppName, nameof(this.Default));
            return Path.Combine(this.Document, this.Company, this.AppName, nameof(this.Default), this.Version);
        }
    }


    /// <summary>
    /// 配置目录
    /// </summary>
    public virtual string Config => Path.Combine(this.Default, nameof(this.Config));

    /// <summary>
    /// 数据目录
    /// </summary>
    public virtual string Data => Path.Combine(this.Default, nameof(this.Data));

    /// <summary>
    /// 注册表路径
    /// </summary>
    public virtual string RegistryPath => Path.Combine("SOFTWARE", this.AppName);

    /// <summary>
    /// 设置目录
    /// </summary>
    public virtual string Setting => Path.Combine(this.Default, nameof(this.Setting));

    /// <summary>
    /// 许可证目录
    /// </summary>
    public virtual string License => Path.Combine(this.Default, nameof(this.License));

    /// <summary>
    /// 日志目录
    /// </summary>
    public virtual string Log => Path.Combine(this.Default, nameof(this.Log));

    /// <summary>
    /// 项目目录
    /// </summary>
    public virtual string Project => Path.Combine(this.Default, nameof(this.Project));

    /// <summary>
    /// 模板目录
    /// </summary>
    public virtual string Template => Path.Combine(this.Default, nameof(this.Template));

    /// <summary>
    /// 缓存目录
    /// </summary>
    public virtual string Cache => Path.Combine(this.Default, nameof(this.Cache));

    public virtual string Version { get; }
    #endregion

    #region - 登录用户目录 -

    /// <summary>
    /// 用户路径
    /// </summary>
    public virtual string UserPath
    {
        get
        {
            var userName = this.GetUserName();
            if (userName == null)
                return this.Default;
            if (string.IsNullOrEmpty(this.Version))
                return Path.Combine(this.AppPath, userName);
            return Path.Combine(this.AppPath, userName, this.Version);
        }
    }

    private string GetUserName()
    {
        return Ioc<ILoginService>.Instance?.User?.Account;
    }

    /// <summary>
    /// 用户数据目录
    /// </summary>
    public virtual string UserData => Path.Combine(this.UserPath, nameof(this.Data));

    /// <summary>
    /// 用户设置目录
    /// </summary>
    public virtual string UserSetting => Path.Combine(this.UserPath, nameof(this.Setting));

    /// <summary>
    /// 用户项目目录
    /// </summary>
    public virtual string UserProject => Path.Combine(this.UserPath, nameof(this.Project));

    /// <summary>
    /// 用户模板目录
    /// </summary>
    public virtual string UserTemplate => Path.Combine(this.UserPath, nameof(this.Template));

    /// <summary>
    /// 用户缓存目录
    /// </summary>
    public virtual string UserCache => Path.Combine(this.UserPath, nameof(this.Cache));

    /// <summary>
    /// 用户许可证目录
    /// </summary>
    public virtual string UserLicense => Path.Combine(this.Default, nameof(this.License));

    /// <summary>
    /// 用户日志目录
    /// </summary>
    public virtual string UserLog => Path.Combine(this.Default, nameof(this.Log));

    #endregion

    /// <summary>
    /// 检查文件夹，如果不存在则创建
    /// </summary>
    /// <param name="folder">文件夹路径</param>
    public void CheckFolder(string folder)
    {
        if (!Directory.Exists(folder))
            Directory.CreateDirectory(folder);
    }
}
