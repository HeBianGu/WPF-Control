using System.ComponentModel;

namespace H.Iocable;

public abstract class IocBindable<T, Interface> : Ioc<T, Interface>, INotifyPropertyChanged where T : class, Interface, new()
{
    public event PropertyChangedEventHandler PropertyChanged;

    public virtual void RaisePropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
    {
        if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));

    }
}
