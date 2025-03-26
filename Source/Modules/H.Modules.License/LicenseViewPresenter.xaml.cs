// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control
using H.Services.Common;
using H.Mvvm;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Xml.Serialization;
using H.Mvvm.ViewModels.Base;
using H.Mvvm.Commands;
using H.Services.Setting;
using H.Common.Attributes;

namespace H.Modules.License
{
    public interface ILicenseViewPresenter
    {

    }

    [Icon("\xE72E")]
    [Display(Name = "许可证书", GroupName = SettingGroupNames.GroupAuthority, Description = "应用此功能进行产品许可注册")]
    public class LicenseViewPresenter : DisplayBindableBase, ILicenseViewPresenter, IDataErrorInfo
    {
        public LicenseViewPresenter()
        {
            this.HostID = LicenseProxy.Instance?.GetHostID();
            this.Module = Assembly.GetEntryAssembly().GetName().Name;
            LicenseOption option = LicenseProxy.Instance?.IsVail(this.Module, out string error);
            if (option != null)
            {
                this.Time = option.EndTime;
                this.IsTrail = option.Level == -1;
                this.Lic = option.License;
                this.Level = option.Level;
            }
        }

        private bool _isTrail;
        [System.Text.Json.Serialization.JsonIgnore]

        [System.Xml.Serialization.XmlIgnore]
        [Browsable(false)]
        public bool IsTrail
        {
            get { return _isTrail; }
            set
            {
                _isTrail = value;
                RaisePropertyChanged();
            }
        }

        private string _module;
        [Browsable(false)]
        [Display(Name = "模块名称")]
        public string Module
        {
            get { return _module; }
            set
            {
                _module = value;
                RaisePropertyChanged();
            }
        }

        private string _lic;
        [System.Text.Json.Serialization.JsonIgnore]

        [System.Xml.Serialization.XmlIgnore]
        [Browsable(false)]
        [Display(Name = "注册码")]
        public string Lic
        {
            get { return _lic; }
            set
            {
                _lic = value;
                RaisePropertyChanged();
            }
        }

        private string _hostID;
        [Browsable(false)]
        [Display(Name = "机器码")]
        public string HostID
        {
            get { return _hostID; }
            set
            {
                _hostID = value;
                RaisePropertyChanged();
            }
        }


        private DateTime _time;
        [Browsable(false)]
        [Display(Name = "截止日期")]
        public DateTime Time
        {
            get { return _time; }
            set
            {
                _time = value;
                RaisePropertyChanged();
            }
        }


        private string _message;
        [System.Text.Json.Serialization.JsonIgnore]

        [System.Xml.Serialization.XmlIgnore]
        [Browsable(false)]
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                RaisePropertyChanged();
            }
        }

        private int _level;
        public int Level
        {
            get { return _level; }
            set
            {
                _level = value;
                RaisePropertyChanged();
            }
        }

        private bool _useVip = false;
        public bool UseVip
        {
            get { return _useVip; }
            set
            {
                _useVip = value;
                RaisePropertyChanged();
            }
        }


        public RelayCommand RegisterCommand => new RelayCommand(e =>
        {
            ILicenseService _license = Ioc.GetService<ILicenseService>();
            if (_license == null)
            {
                this.Message = "未找到许可服务接口<ILicenseService>";
                return;
            }

            LicenseOption option = _license.TryActive(this.Module, this.Lic, out string error);

            if (option == null)
            {
                this.Message = error;
                return;
            }

            this.Time = option.EndTime;
            this.HostID = option.HostID;
            this.Message = "注册成功";
            this.Level = option.Level;
            Ioc<IVipFlagViewPresenter>.Instance?.Refresh();
            Ioc<ILicenseFlagViewPresenter>.Instance?.Refresh();
        }, x => string.IsNullOrEmpty(this.Error));

        [System.Text.Json.Serialization.JsonIgnore]

        [System.Xml.Serialization.XmlIgnore]
        [Browsable(false)]
        public string Error { get; private set; }

        public string this[string columnName]
        {
            get
            {
                this.Error = null;
                if (string.IsNullOrEmpty(this.HostID))
                {
                    this.Error = "机器码不能为空";
                    return this.Error;
                }
                if (string.IsNullOrEmpty(this.Lic))
                {
                    this.Error = "注册码不能为空";
                    return this.Error;
                }

                if (string.IsNullOrEmpty(this.Module))
                {
                    this.Error = "模块名称不能为空";
                    return this.Error;
                }

                ILicenseService _license = Ioc.GetService<ILicenseService>();
                if (_license == null)
                {
                    this.Error = "未找到许可服务接口<ILicenseService>";
                    return this.Error;
                }

                return this.Error;
            }
        }
    }
}
