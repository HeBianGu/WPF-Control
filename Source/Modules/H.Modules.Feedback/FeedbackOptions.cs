// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Common.Attributes;
using H.Extensions.FontIcon;
using H.Extensions.Mail;
using H.Services.Setting;
using System.ComponentModel.DataAnnotations;

namespace H.Modules.Feedback;

[Icon(FontIcons.Feedback)]
[Display(Name = "用户反馈设置", GroupName = SettingGroupNames.GroupSystem, Description = "用户反馈设置的信息")]
public class FeedbackOptions : SmtpSendOptions<FeedbackOptions>, IFeedbackOptions
{

}
