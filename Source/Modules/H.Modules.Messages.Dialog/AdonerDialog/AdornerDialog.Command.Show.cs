using H.Controls.Adorner;
using H.Providers.Mvvm;
using System.Windows;
using System.Windows.Documents;

namespace H.Modules.Messages.Dialog
{
    public class ShowAdornerDialogCommand : MarkupCommandBase
    {
        public override void Execute(object parameter)
        {
            Window window = Application.Current.MainWindow;
            UIElement child = window.Content as UIElement;
            AdornerLayer layer = AdornerLayer.GetAdornerLayer(child);
            AdornerDialogPresenter contentDialog = new AdornerDialogPresenter(parameter);
            PresenterAdorner adorner = new PresenterAdorner(child, contentDialog);
            layer.Add(adorner);
        }
    }
}
