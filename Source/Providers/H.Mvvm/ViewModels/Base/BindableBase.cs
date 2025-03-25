namespace H.Mvvm.ViewModels.Base;

public abstract class BindableBase : INotifyPropertyChanged
{
    public BindableBase()
    {
        Init();
    }

    protected virtual void Init()
    {

    }

    #region - MVVM -
    public event PropertyChangedEventHandler PropertyChanged;

    public virtual void RaisePropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
    {
        if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));

    }
    #endregion
}
