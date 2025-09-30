// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Services.Message.Dialog.Commands;

public class ShowEditCommand : ShowMessageDialogCommandBase
{
    public bool UseModelState { get; set; } = true;
    public object Value { get; set; }

    public override async Task ExecuteAsync(object parameter)
    {
        await IocMessage.Form.ShowEdit(this.Value ?? parameter, x => this.Invoke(x), this.UseModelState ? null : x => true);
    }
    public override bool CanExecute(object parameter)
    {
        return base.CanExecute(parameter) && (parameter != null || this.Value != null);
    }
}
[Icon("\xE70F")]
[Display(Name = "编辑", Description = "显示多标签表单编辑数据")]
public class ShowTabEditCommand : ShowMessageDialogCommandBase
{
    public bool UseModelState { get; set; } = true;
    public object Value { get; set; }
    public string TabNames { get; set; }

    public override async Task ExecuteAsync(object parameter)
    {
        await IocMessage.Form.ShowTabEdit(this.Value ?? parameter, x => this.Invoke(x), this.UseModelState ? null : x => true, x => x.UseGroupNames = TabNames);
    }
    public override bool CanExecute(object parameter)
    {
        return base.CanExecute(parameter) && (parameter != null || this.Value != null);
    }
}
