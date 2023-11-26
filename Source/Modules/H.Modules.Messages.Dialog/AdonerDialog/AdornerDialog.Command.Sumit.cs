using H.Providers.Mvvm;

namespace H.Modules.Messages.Dialog
{
    public class SumitAdornerDialogCommand : MarkupCommandBase
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
