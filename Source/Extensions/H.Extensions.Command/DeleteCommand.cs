// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using H.Services.Message;
global using System.Collections;
using System.Reflection.Metadata;

namespace H.Extensions.Command;

[Icon("\xE77F")]
[Display(Name = "删除", Description = "从列表中删除当前项目")]
public class DeleteCommand : DisplayMarkupCommandBase
{
    public bool UseDialog { get; set; } = true;
    public override async Task ExecuteAsync(object parameter)
    {
        if (this.UseDialog)
        {
            await IocMessage.Dialog.ShowDeleteDialog(x =>
            {
                this.Delete(parameter);
            });
        }
        else
        {
            this.Delete(parameter);
        }
     
        await base.ExecuteAsync(parameter);
    }


    private void Delete(object parameter)
    {
        if (this.GetTargetElement(parameter) is FrameworkElement element)
        {
            if (element.DataContext == null)
                return;
            ItemsControl items = element.GetParent<ItemsControl>();
            if (items.ItemsSource is IList list)
                list.Remove(element.DataContext);
            else
                items.Items.Remove(element.DataContext);
        }
    }


    public override bool CanExecute(object parameter)
    {
        if (this.GetTargetElement(parameter) is FrameworkElement element)
        {
            if (element.DataContext == null)
                return false;
            ItemsControl items = element.GetParent<ItemsControl>();
            return items != null;
        }
        return false;
    }
}
