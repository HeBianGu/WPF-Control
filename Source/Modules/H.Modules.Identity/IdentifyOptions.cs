using H.Extensions.Setting;
using H.Services.Common;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace H.Modules.Identity
{
    [Display(Name = "身份认证设置", GroupName = SettingGroupNames.GroupSystem, Description = "身份认证设置的信息")]
    public class IdentifyOptions : IocOptionInstance<IdentifyOptions>, IIdentifyOptions
    {
        private bool _useAdiminCheckOnRegister;
        [ReadOnly(true)]
        [DefaultValue(false)]
        [Display(Name = "启用注册审核", Description = "注册用户时默认不可用，需管理员审核后可用")]
        public bool UseAdiminCheckOnRegister
        {
            get { return _useAdiminCheckOnRegister; }
            set
            {
                _useAdiminCheckOnRegister = value;
                RaisePropertyChanged();
            }
        }

        private bool _useUserLicenseDeadTime;
        [ReadOnly(true)]
        [DefaultValue(false)]
        [Display(Name = "启用用户许可截止日期", Description = "启用后登录时会检验用户许可日期")]
        public bool UseUserLicenseDeadTime
        {
            get { return _useUserLicenseDeadTime; }
            set
            {
                _useUserLicenseDeadTime = value;
                RaisePropertyChanged();
            }
        }

        private TimeSpan _userLicenseDefaultTryTime = TimeSpan.FromDays(30);
        [ReadOnly(true)]
        [Display(Name = "用户许可默认试用时间", Description = "启用后首次注册的用户会有一个默认试用时间")]
        public TimeSpan UserLicenseDefaultTryTime
        {
            get { return _userLicenseDefaultTryTime; }
            set
            {
                _userLicenseDefaultTryTime = value;
                RaisePropertyChanged();
            }
        }

    }
}
