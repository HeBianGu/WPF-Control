// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Iocable;
using H.Extensions.Mvvm.ViewModels.Base;

namespace H.Extensions.IocPresenter;

public abstract class IocDisplayBindableBase<T, Interface> : DisplayBindableBase where T : class, Interface, new()
{
    public static T Instance => Ioc.GetService<Interface>() as T;
}
