using H.Mvvm;
using System.ComponentModel.DataAnnotations;

namespace H.Modules.Messages.Dialog
{
    [Icon("\xE77F")]
    [Display(Name = "确定", Description = "确定提交并关闭对话框页面")]
    public class SumitAdornerDialogCommand : DisplayMarkupCommandBase
    {
        public override void Execute(object parameter)
        {
            if (parameter is AdornerDialogPresenter presenter)
                presenter.Sumit();
        }

        public override bool CanExecute(object parameter)
        {
            return parameter is AdornerDialogPresenter;
        }
    }
}
