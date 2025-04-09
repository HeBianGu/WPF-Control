using H.Iocable;
using H.Mvvm.ViewModels.Base;

namespace H.Extensions.IocPresenter;

public abstract class IocDisplayBindableBase<T, Interface> : DisplayBindableBase where T : class, Interface, new()
{
    public static T Instance => Ioc.GetService<Interface>() as T;
}
