
using H.Extensions.Setting;
using H.Providers.Ioc;
using H.Providers.Mvvm;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace H.Modules.Identity
{
    [Displayer(Name = "权限设置", GroupName = SettingGroupNames.GroupSystem)]
    public class AuthoritySetting : Setting<AuthoritySetting>
    {
        private bool _useAuthority = true;
        [Display(Name = "启用权限控制")]
        public bool UseAuthority
        {
            get { return _useAuthority; }
            set
            {
                _useAuthority = value;
                RaisePropertyChanged();
            }
        }

        private bool _useAllIfNoLogin = true;
        [Display(Name = "启用如果没有登录用户加载所有权限")]
        public bool UseAllIfNoLggin
        {
            get { return _useAllIfNoLogin; }
            set
            {
                _useAllIfNoLogin = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<IAuthority> _authoritys = new ObservableCollection<IAuthority>();
        public ObservableCollection<IAuthority> Authoritys
        {
            get { return _authoritys; }
            set
            {
                _authoritys = value;
                RaisePropertyChanged();
            }
        }
    }
}
