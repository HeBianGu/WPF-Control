using H.Mvvm.ViewModels.Base;
using System.Runtime.CompilerServices;

namespace H.Extensions.Revertible;

public abstract class RevertiblePropertyChangedBase : BindableBase
{
    protected bool SetRevertiableProperty<T>(Action<T> setValue, T oldValue, T newValue, [CallerMemberName] string propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(oldValue, newValue))
            return false;
        PropertyChangedRevertiblePrensenter<T> persenter = new PropertyChangedRevertiblePrensenter<T>(propertyName, oldValue, newValue);
        IocRevertible.Commit(() =>
        {
            setValue?.Invoke(newValue);
            RaisePropertyChanged(propertyName);

        },
        () =>
        {
            setValue?.Invoke(oldValue);
            RaisePropertyChanged(propertyName);
        }, propertyName, persenter);
        return true;
    }
}
