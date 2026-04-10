// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Extensions.Mvvm.ViewModels.Base;

namespace H.Modules.Login.Presenters;

[Icon(FontIcons.Contact)]
[Display(Name = "当前用户", GroupName = SettingGroupNames.GroupSystem, Description = "当前登录的用户信息")]
public class CurrentUserViewPresenter : DisplayBindableBase, ICurrentUserViewPresenter
{

}

public interface ICurrentUserViewPresenter
{

}
