// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase


using H.Extensions.Setting;
using H.Providers.Ioc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace H.Modules.License
{
    [Display(Name = "许可管理", GroupName = SettingGroupNames.GroupSystem, Description = "许可管理设置的信息")]
    public class LicenseOptions : IocOptionInstance<LicenseOptions>
    {
        private bool _useTipTrialOnLoad = false;
        [JsonIgnore]
        [XmlIgnore]
        [Browsable(false)]
        [DefaultValue(true)]
        [Display(Name = "不提示试用许可")]
        public bool UseTipTrialOnLoad
        {
            get { return _useTipTrialOnLoad; }
            set
            {
                _useTipTrialOnLoad = value;
                RaisePropertyChanged();
            }
        }

        private bool _useVailLicenceOnLoad = true;
        [Browsable(false)]
        [JsonIgnore]
        [XmlIgnore]
        [Display(Name = "启动时是否启用许可验证")]
        public bool UseVailLicenceOnLoad
        {
            get { return _useVailLicenceOnLoad; }
            set
            {
                _useVailLicenceOnLoad = value;
                RaisePropertyChanged();
            }
        }


        private bool _useTrial = true;
        [JsonIgnore]
        [XmlIgnore]
        [Browsable(false)]
        [Display(Name = "启用试用")]
        public bool UseTrial
        {
            get { return _useTrial; }
            set
            {
                _useTrial = value;
                RaisePropertyChanged();
            }
        }

        private string _contract = "QQ:908293466";
        [ReadOnly(true)]
        [Display(Name = "联系客服")]
        public string Contract
        {
            get { return _contract; }
            set
            {
                _contract = value;
                RaisePropertyChanged();
            }
        }

        private string _filePath = AppPaths.Instance.License;
        [Browsable(false)]
        [XmlIgnore]
        [ReadOnly(true)]
        [Display(Name = "许可路径")]
        public string FilePath
        {
            get { return _filePath; }
            set
            {
                _filePath = value;
                RaisePropertyChanged();
            }
        }

        private int _trialDays = 30;
        [ReadOnly(true)]
        [Display(Name = "试用天数")]
        public int TrialDays
        {
            get { return _trialDays; }
            set
            {
                _trialDays = value;
                RaisePropertyChanged();
            }
        }
    }
}
