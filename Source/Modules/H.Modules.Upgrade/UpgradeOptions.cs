// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.Form.Attributes;
using H.Controls.Form.PropertyItem.TextPropertyItems;
using H.Extensions.Setting;
using H.Services.AppPath;
using System.Text.Json.Serialization;

namespace H.Modules.Upgrade;

[Display(Name = "软件更新", GroupName = SettingGroupNames.GroupSystem, Description = "配置软件更新相关参数")]
public class UpgradeOptions : IocOptionInstance<UpgradeOptions>, IUpgradeOptions
{
    private bool _checkUpgradeOnStart;
    [DefaultValue(true)]
    [Display(Name = "启动时检查更新")]
    public bool CheckUpgradeOnStart
    {
        get { return _checkUpgradeOnStart; }
        set
        {
            _checkUpgradeOnStart = value;
            RaisePropertyChanged();
        }
    }

    private string _UpgradeInfoUri;
    [ReadOnly(true)]
    [Display(Name = "更新文件地址")]
    public string UpgradeInfoUri
    {
        get { return _UpgradeInfoUri; }
        set
        {
            _UpgradeInfoUri = value;
            RaisePropertyChanged();
        }
    }

    private int _ReadInfoTimeOutMilliseconds;
    [Unit("ms")]
    [PropertyItem(typeof(UnitTextPropertyItem))]
    [ReadOnly(true)]
    [DefaultValue(5000)]
    [Range(1000, 30 * 1000)]
    [Display(Name = "读取信息超时时间")]
    public int ReadInfoTimeOutMilliseconds
    {
        get { return _ReadInfoTimeOutMilliseconds; }
        set
        {
            _ReadInfoTimeOutMilliseconds = value;
            RaisePropertyChanged();
        }
    }

    private bool _useIEOpenUri;
    [Browsable(false)]
    [DefaultValue(true)]
    [Display(Name = "使用浏览器打开")]
    public bool UseIEOpenUri
    {
        get { return _useIEOpenUri; }
        set
        {
            _useIEOpenUri = value;
            RaisePropertyChanged();
        }
    }

    private string _SavePath = AppPaths.Instance.Cache;
    [BindingVisiblableMethodName(nameof(GetNotUseIEOpenUri))]
    [ReadOnly(true)]
    [Display(Name = "下载文件保存位置")]
    public string SavePath
    {
        get { return _SavePath; }
        set
        {
            _SavePath = value;
            RaisePropertyChanged();
        }
    }

    public bool GetNotUseIEOpenUri() => !UseIEOpenUri;

    private string _loadFormat;
    [BindingVisiblableMethodName(nameof(GetNotUseIEOpenUri))]
    [ReadOnly(true)]
    [Display(Name = "下载安装包时格式")]
    [DefaultValue("正在下载 {0}/{1}")]
    public string LoadFormat
    {
        get { return _loadFormat; }
        set
        {
            _loadFormat = value;
            RaisePropertyChanged();
        }
    }

    private bool _automaticUpgrade;
    [ReadOnly(true)]
    [DefaultValue(false)]
    [Display(Name = "自动安装", Description = "有更新时自动为我安装(推荐)")]
    public bool AutomaticUpgrade
    {
        get { return _automaticUpgrade; }
        set
        {
            _automaticUpgrade = value;
            RaisePropertyChanged();
        }
    }

    private bool _notifyUpgrade;
    [ReadOnly(true)]
    [DefaultValue(false)]
    [Display(Name = "提醒安装", Description = "有更新时不要安装，但提醒我")]
    public bool NotifyUpgrade
    {
        get { return _notifyUpgrade; }
        set
        {
            _notifyUpgrade = value;
            RaisePropertyChanged();
        }
    }

    private string _UpgradeVersion;
    [JsonIgnore]
    [ReadOnly(true)]
    [Display(Name = "发布的最新版本", Description = "当前发布的最新版本")]
    public string UpgradeVersion
    {
        get { return _UpgradeVersion; }
        set
        {
            _UpgradeVersion = value;
            RaisePropertyChanged();
        }
    }

    private bool _HasNewVersion;
    [JsonIgnore]
    [ReadOnly(true)]
    [Display(Name = "是否有新版本", Description = "有新版本需要更新")]
    public bool HasNewVersion
    {
        get { return _HasNewVersion; }
        set
        {
            _HasNewVersion = value;
            RaisePropertyChanged();
        }
    }
}
