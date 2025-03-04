global using H.Mvvm;
global using H.Mvvm.Attributes;
global using H.Mvvm.ViewModels.Base;

namespace H.Presenters.Common
{
    [Icon("\xE875")]
    public class PasswordBoxPresenter:DisplayBindableBase
    {
        public string Password { get; set; }
    }
}
