// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

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

