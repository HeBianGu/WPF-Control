// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Extensions.Mvvm.ViewModels.Base;
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
