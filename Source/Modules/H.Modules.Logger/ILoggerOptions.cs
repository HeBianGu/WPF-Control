// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Common.Attributes;
using H.Common.Commands;
using H.Extensions.FontIcon;
using H.Iocable;
using H.Services.Logger;
using H.Services.Message;
using H.Services.Message.Dialog.Commands;
using H.Themes.Backgrounds;
using H.Themes.FontSizes;
using H.Themes.Layouts;
using System.Windows.Media;

namespace H.Modules.Logger;

[Icon(FontIcons.Message)]
[Display(Name = "应用程序消息日志", Description = "显示应用程序消息日志")]
public class ShowAppLogerCommmand : ShowIocPresenterCommandBase<IAppLogPresenter>
{

}

public interface ILoggerOptions
{
    LogType LogType { get; set; }

    int Capacity { get; set; }
}
