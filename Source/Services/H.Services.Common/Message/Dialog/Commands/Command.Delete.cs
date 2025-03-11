// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using H.Mvvm.Attributes;
using System.ComponentModel.DataAnnotations;

namespace H.Services.Common
{
    [Icon("\xE77F")]
    [Display(Name = "删除", Description = "从列表中删除当前项目")]
    public class DeleteCommand : DisplayMarkupCommandBase
    {
        public override void Execute(object parameter)
        {
            if (parameter is FrameworkElement element)
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
            if (parameter is FrameworkElement element)
            {
                if (element.DataContext == null)
                    return false;
                ItemsControl items = element.GetParent<ItemsControl>();
                return items != null;
            }
            return false;
        }
    }
}
