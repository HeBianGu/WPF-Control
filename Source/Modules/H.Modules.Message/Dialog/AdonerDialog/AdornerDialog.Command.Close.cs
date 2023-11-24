using H.Controls.Adorner;
using H.Providers.Mvvm;
using System.Windows.Documents;
using System.Windows;
using System.Linq;

namespace H.Modules.Message
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
