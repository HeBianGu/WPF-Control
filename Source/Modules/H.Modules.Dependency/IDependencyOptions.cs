// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Services.Setting;

namespace H.Modules.Dependency;

/// <summary>
/// 提供关于选项的接口。
/// </summary>
public interface IDependencyOptions : ISettable
{
    ObservableCollection<IDependencyItem> DependencyItems { get; set; }
}