// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Common.Interfaces;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace H.Extensions.IocPresenter;

public class IocTitlePresenterBase<T, Interface> : IocDisplayBindableBase<T, Interface>, ITitleable where T : class, Interface, new()
{
    public IocTitlePresenterBase()
    {
        TypeDescriptor.GetAttributes(this).OfType<DisplayAttribute>();
        this.Title = TypeDescriptor.GetClassName(this);
    }
    public string Title { get; set; }
}
