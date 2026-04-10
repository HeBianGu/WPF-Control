// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Extensions.FontIcon;
using H.Extensions.Mvvm.Commands;
using System.Text.Json.Serialization;

namespace H.Presenters.Design.Presenter;

public interface IDisplayCommandPresenter
{
    IDisplayCommand Command { get; set; }
}

[Icon(FontIcons.ButtonA)]
[Display(Name = "操作")]
public class DisplayCommandPresenter : CommandsDesignPresenterBase, IDisplayCommandPresenter
{
    public DisplayCommandPresenter()
    {
        this.Command = this.CreateCommand();
    }

    protected virtual IDisplayCommand CreateCommand()
    {
        return new DisplayCommand(Execute, CanExecute)
        {
            Name = this.Name,
            GroupName = this.GroupName,
            Description = this.Description,
            Icon = this.Icon,
            Order = this.Order
        };
    }

    private IDisplayCommand _Command;
    [JsonIgnore]
    public IDisplayCommand Command
    {
        get { return _Command; }
        set
        {
            _Command = value;
            RaisePropertyChanged();
        }
    }

    protected virtual void Execute(object x)
    {

    }

    protected virtual bool CanExecute(object x)
    {
        return true;
    }
}
