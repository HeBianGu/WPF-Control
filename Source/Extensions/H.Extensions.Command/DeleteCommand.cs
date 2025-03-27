// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

global using H.Services.Message;
global using System.Collections;
global using System.Windows;
global using H.Services.Message.Dialog;
global using H.Extensions.Common;
using H.Services.Message;
using H.Extensions.Common;

namespace H.Extensions.Command;

[Icon("\xE77F")]
[Display(Name = "删除", Description = "从列表中删除当前项目")]
public class DeleteCommand : DisplayMarkupCommandBase
{
    public override async Task ExecuteAsync(object parameter)
    {
        await IocMessage.Dialog.ShowDeleteDialog(x =>
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
          });
        await base.ExecuteAsync(parameter);
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
