// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Extensions.Mvvm.ViewModels.Base;
using H.Services.Logger;
using Microsoft.Extensions.Options;

namespace H.Modules.Logger;

public interface IAppLogPresenter
{
    ObservableCollection<IAppLogMessage> Messages { get; set; }
}

[Display(Name = "应用日志展示", Description = "应用日志展示的信息")]
public class AppLogPresenter : DisplayBindableBase, IAppLogPresenter
{
    private readonly IOptions<LoggerOptions> _options;
    public AppLogPresenter(IOptions<LoggerOptions> options)
    {
        _options = options;
    }

    private ObservableCollection<IAppLogMessage> _Messages = new ObservableCollection<IAppLogMessage>();
    public ObservableCollection<IAppLogMessage> Messages
    {
        get { return _Messages; }
        set
        {
            _Messages = value;
            RaisePropertyChanged();
        }
    }


    private IAppLogMessage _SelectedMessage;
    public IAppLogMessage SelectedMessage
    {
        get { return _SelectedMessage; }
        set
        {
            _SelectedMessage = value;
            RaisePropertyChanged();
        }
    }

}

public interface IAppLogMessage
{
    LogType Level { get; set; }
    string Message { get; set; }
    DateTime Time { get; set; }
}

public class AppLogMessage : IAppLogMessage
{
    public AppLogMessage(string message, LogType level = LogType.Info)
    {
        Message = message;
        Level = level;
    }
    public DateTime Time { get; set; } = DateTime.Now;
    public string Message { get; set; }
    public LogType Level { get; set; }
}
