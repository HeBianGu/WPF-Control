using H.Common.Commands;
using H.Mvvm;
using System.ComponentModel.DataAnnotations;

namespace H.Modules.Messages.Dialog
{
    [Icon("\xE77F")]
    [Display(Name = "取消", Description = "取消并关闭对话框页面")]
    public class CancelAdornerDialogCommand : DisplayMarkupCommandBase
    {
        public override void Execute(object parameter)
        {
            if (parameter is AdornerDialogPresenter presenter)
                presenter.Cancel();
        }

        public override bool CanExecute(object parameter)
        {
            return parameter is AdornerDialogPresenter;
        }
    }
}
