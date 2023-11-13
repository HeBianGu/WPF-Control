// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

namespace H.Providers.Ioc
{
    public interface IRoleViewPresenter
    {

    }

    //public class AuthorityAttribute : System.Attribute
    //{
    //    public string ID { get; set; }
    //    public string Name { get; set; }
    //    public string GroupName { get; set; }
    //}

    //[Displayer(Name = "权限设置", GroupName = SystemSetting.GroupSystem)]
    //public class AuthoritySetting : LazyNotifyInstance<AuthoritySetting>
    //{
    //    private bool _useAuthority = true;
    //    [Display(Name = "启用权限控制")]
    //    public bool UseAuthority
    //    {
    //        get { return _useAuthority; }
    //        set
    //        {
    //            _useAuthority = value;
    //            RaisePropertyChanged();
    //        }
    //    }

    //    private bool _useAllIfNoLogin = true;
    //    [Display(Name = "启用如果没有登录用户加载所有权限")]
    //    public bool UseAllIfNoLggin
    //    {
    //        get { return _useAllIfNoLogin; }
    //        set
    //        {
    //            _useAllIfNoLogin = value;
    //            RaisePropertyChanged();
    //        }
    //    }

    //    private ObservableCollection<IAuthority> _authoritys = new ObservableCollection<IAuthority>();
    //    public ObservableCollection<IAuthority> Authoritys
    //    {
    //        get { return _authoritys; }
    //        set
    //        {
    //            _authoritys = value;
    //            RaisePropertyChanged();
    //        }
    //    }

    //}

    //public class AuthorityProxy : ServiceInstance<IAuthorityService>
    //{

    //}

    //public interface IAuthorityService
    //{
    //    void Add(params IAuthority[] settings);

    //    bool IsValid(string id);
    //}
}