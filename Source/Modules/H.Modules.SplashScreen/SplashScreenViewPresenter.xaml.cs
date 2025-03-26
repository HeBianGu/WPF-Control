using H.Mvvm.ViewModels.Base;
using H.Services.Common.SplashScreen;

namespace H.Modules.SplashScreen;

public class SplashScreenViewPresenter : BindableBase, ISplashScreenViewPresenter
{
    private string _message;
    public string Message
    {
        get { return _message; }
        set
        {
            _message = value;
            RaisePropertyChanged();
        }
    }
}
