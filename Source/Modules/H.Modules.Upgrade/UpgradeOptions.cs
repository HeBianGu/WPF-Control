﻿// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase


using H.Extensions.Setting;
using H.Providers.Ioc;

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace H.Modules.Upgrade
{
    [Display(Name = "软件更新", GroupName = SystemSetting.GroupSystem)]
    public class UpgradeOptions : IocOptionInstance<UpgradeOptions>
    {
        protected override string GetDefaultFolder()
        {
            //base.GetDefaultFolder();
            return SystemPathSetting.Instance.Config;
        }

        private string _uri;
        [Display(Name = "检查更新地址")]
        public string Uri
        {
            get { return _uri; }
            set
            {
                _uri = value;
                RaisePropertyChanged();
            }
        }

        private string _SavePath = SystemPathSetting.Instance.Version;
        [Display(Name = "更新文件保存位置")]
        public string SavePath
        {
            get { return _SavePath; }
            set
            {
                _SavePath = value;
                RaisePropertyChanged();
            }
        }


        private string _loadFormat;
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

        private bool _useIEDownload;
        [DefaultValue(false)]
        [Display(Name = "使用浏览器下载")]
        public bool UseIEDownload
        {
            get { return _useIEDownload; }
            set
            {
                _useIEDownload = value;
                RaisePropertyChanged();
            }
        }

        private bool _checkUpdateOnStart;
        [DefaultValue(true)]
        [Display(Name = "启动时检查更新")]
        public bool CheckUpdateOnStart
        {
            get { return _checkUpdateOnStart; }
            set
            {
                _checkUpdateOnStart = value;
                RaisePropertyChanged();
            }
        }

        private bool _automaticUpgrade;
        [DefaultValue(true)]
        [Display(Name = "有更新时自动为我安装(推荐)", Description = "当有更新时，智能选择最佳的软件更新方案，自动完成更新的下载与安装，省心省力")]
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
        [DefaultValue(false)]
        [Display(Name = "有更新时不要安装，但提醒我", Description = "此方式不影响安全补丁等重要更新的自动安装")]
        public bool NotifyUpgrade
        {
            get { return _notifyUpgrade; }
            set
            {
                _notifyUpgrade = value;
                RaisePropertyChanged();
            }
        }

    }
}