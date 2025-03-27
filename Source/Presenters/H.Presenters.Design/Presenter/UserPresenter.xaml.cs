using H.Services.Identity;

namespace H.Presenters.Design.Presenter;

[Display(Name = "当前用户")]
public class UserPresenter : TitlePresenter
{
    public UserPresenter()
    {
        this.Title = "当前用户：";
        this.Text = Ioc<ILoginService>.Instance?.User?.Name;
    }
    public override void LoadDefault()
    {
        base.LoadDefault();
    }
}
