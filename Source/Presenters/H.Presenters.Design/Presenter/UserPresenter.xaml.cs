using H.Providers.Ioc;
using H.Providers.Mvvm;
using System;

namespace H.Presenters.Design
{
    [Displayer(Name = "当前用户", Icon = "\xe84b")]
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
