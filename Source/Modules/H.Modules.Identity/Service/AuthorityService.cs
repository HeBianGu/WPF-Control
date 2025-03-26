// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using H.Services.Common;
using H.Services.Identity.Author;

namespace H.Modules.Identity
{
    public interface IAuthorityOption
    {
        void Add(params IAuthority[] settings);
    }

    //public class AuthorityService : ServiceSettingInstance<AuthorityService, IAuthorityService>, IAuthorityService, IAuthorityOption
    //{
    //    private ObservableCollection<IAuthority> _authorities = new ObservableCollection<IAuthority>();
    //    /// <summary> 说明  </summary>
    //    public ObservableCollection<IAuthority> Authorities
    //    {
    //        get { return _authorities; }
    //        set
    //        {
    //            _authorities = value;
    //            RaisePropertyChanged();
    //        }
    //    }

    //    public virtual void Load()
    //    {

    //    }

    //    public virtual bool Save(out string message)
    //    {
    //        message = null;
    //        return true;
    //    }

    //    public void Add(params IAuthority[] settings)
    //    {
    //        foreach (var setting in settings)
    //        {
    //            if (this.Authorities.Contains(setting))
    //                continue;
    //            this.Authorities.Add(setting);
    //        }
    //    }

    //    public bool IsValid(string id)
    //    {
    //        return Loginner.Instance?.User?.Role?.Authors?.Any(x => x.ID == id) == true;
    //    }
    //}
}




