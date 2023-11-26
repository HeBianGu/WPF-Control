using H.Controls.Adorner;
using H.Providers.Mvvm;
using System.Linq;
using System.Windows;
using System.Windows.Documents;

namespace H.Modules.Messages.Dialog
{
    public class CloseAdornerDialogCommand : MarkupCommandBase
    {
        public override void Execute(object parameter)
        {
            if (parameter is AdornerDialogPresenter presenter)
                presenter.Close();
        }

        public override bool CanExecute(object parameter)
        {
            return parameter is AdornerDialogPresenter;
        }
    }
}
