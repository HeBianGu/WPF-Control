// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using H.Mvvm;
using H.Mvvm.Attributes;
using H.Services.Common;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;

namespace H.Extensions.Command
{

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
}
