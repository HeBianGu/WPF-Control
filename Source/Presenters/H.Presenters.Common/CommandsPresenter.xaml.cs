// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Presenters.Common;



public class TestCommandsPresenter : CommandsPresenterBase
{
    [Display(Name = "确定", GroupName = "操作")]
    public DisplayCommand SumitCommand => new DisplayCommand(x =>
    {

    });

    [Display(Name = "取消", GroupName = "操作")]
    public DisplayCommand CancelCommand => new DisplayCommand(x =>
    {

    });
}

public class CommandsPresenter : CommandsPresenterBase
{
    public CommandsPresenter(object presenter)
    {
        this.Presenter = presenter;
    }
}

[Icon("\xEDE3")]
public class CommandsPresenterBase : CommandsBindableBase
{
    public object Presenter { get; set; }
}

public class CommandsPresenter<T> : CommandsPresenterBase
{
    public CommandsPresenter(T presenter)
    {
        this.Presenter = presenter;
    }

    public new T Presenter { get; set; }
}

public class DialogCommandsPresenter<T> : CommandsPresenter<T>
{
    public DialogCommandsPresenter(T presenter) : base(presenter)
    {

    }

    [Display(Name = "确定", GroupName = "操作")]
    public DisplayCommand SumitCommand => new DisplayCommand(x =>
    {
        var dialog = this.GetDialog(x);
        dialog.Sumit();
    });

    protected virtual IDialog GetDialog(object parameter)
    {
        if (parameter is FrameworkElement element)
        {
            if (element is IDialog)
                return element as IDialog;
            FrameworkElement parent = element.GetParent<FrameworkElement>(x => x?.DataContext is IDialog || x is IDialog);
            return parent is IDialog dialog ? dialog : parent.DataContext as IDialog;

        }
        return null;
    }
}
