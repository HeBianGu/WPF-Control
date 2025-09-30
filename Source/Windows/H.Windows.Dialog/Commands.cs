// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using System;
global using System.ComponentModel.DataAnnotations;
global using System.Reflection;
global using System.Windows.Input;
global using System.Windows.Markup;

namespace H.Windows.Dialog;

public abstract class DialogCommandBase : MarkupExtension, ICommand
{
    public int Width { get; set; } = 500;
    public int Height { get; set; } = 300;

    public event EventHandler? CanExecuteChanged
    {
        add
        {
            CommandManager.RequerySuggested += value;
        }
        remove
        {
            CommandManager.RequerySuggested -= value;
        }
    }
    public virtual bool CanExecute(object? parameter)
    {
        return parameter is Type;
    }

    public abstract void Execute(object? parameter);

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return this;
    }
}
