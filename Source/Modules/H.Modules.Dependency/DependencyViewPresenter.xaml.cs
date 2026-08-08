// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Common.Attributes;
using H.Extensions.FontIcon;
using H.Extensions.Mvvm.ViewModels.Base;

namespace H.Modules.Dependency
{
    public interface IDependencyViewPresenter
    {
    }
    [Icon(FontIcons.Info)]
    [Display(Name = "第三方依赖项", Description = "这是一个关于页面的信息")]
    public class DependencyViewPresenter : DisplayBindableBase, IDependencyViewPresenter
    {

    }
}
