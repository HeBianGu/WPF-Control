// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Common.Attributes;
using H.Services.Message.Dialog.Commands;
using System.ComponentModel.DataAnnotations;

namespace H.Modules.Theme;

[Icon("\xE790")]
[Display(Name = "颜色主题", Description = "显示设置颜色主题")]
public class ShowColorThemeViewCommand : ShowIocPresenterCommandBase<IColorThemeViewPresenter>
{

}
