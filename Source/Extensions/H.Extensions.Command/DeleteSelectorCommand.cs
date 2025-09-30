// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Extensions.Command;

[Icon("\xE77F")]
[Display(Name = "删除选中项", Description = "从列表中删除当前项目")]
public class DeleteSelectorCommand : DisplayMarkupCommandBase
{
    public override async Task ExecuteAsync(object parameter)
    {
        await IocMessage.Dialog.ShowDeleteDialog(x =>
        {
            if (this.GetTargetElement(parameter) is Selector selector)
            {
                selector.Items.RemoveAt(selector.SelectedIndex);
            }
        });
        await base.ExecuteAsync(parameter);
    }

    public override bool CanExecute(object parameter)
    {
        return this.GetTargetElement(parameter) is Selector selector && selector.SelectedIndex >= 0;
    }
}
