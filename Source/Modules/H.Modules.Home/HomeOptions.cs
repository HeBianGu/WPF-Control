// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Extensions.Setting;
using H.Services.Setting;

namespace H.Modules.Home;
[Display(Name = "主页设置", GroupName = SettingGroupNames.GroupStyle, Description = "主页设置的信息")]
public class HomeOptions : IocOptionInstance<HomeOptions>, IHomeOptions
{


}
