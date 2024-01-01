using H.Providers.Ioc;
using H.Providers.Mvvm;
using System;
using System.ComponentModel.DataAnnotations;

namespace H.Presenters.Design
{
    [Display(Name = "当前用户")]
    public class UserPresenter : TitlePresenter
    {
        public UserPresenter()
        {
            Title = "当前用户：";
            Text = Ioc<ILoginService>.Instance?.User?.Name;
        }
        public override void LoadDefault()
        {
            base.LoadDefault();
        }
    }
}
