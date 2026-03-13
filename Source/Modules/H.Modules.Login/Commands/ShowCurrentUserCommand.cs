// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Modules.Login.Presenters;
using H.Services.Message.Dialog.Commands;

namespace H.Modules.Login.Commands;

[Icon(FontIcons.View)]
[Display(Name = "用户信息", GroupName = SettingGroupNames.GroupSystem, Description = "显示当前用户信息")]
public class ShowCurrentUserCommand : ShowIocPresenterCommandBase<ICurrentUserViewPresenter>
{

}