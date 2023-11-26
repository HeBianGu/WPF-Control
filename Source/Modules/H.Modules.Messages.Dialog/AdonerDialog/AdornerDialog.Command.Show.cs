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
            var window = Application.Current.MainWindow;
            var child = window.Content as UIElement;
            var layer = AdornerLayer.GetAdornerLayer(child);
            var contentDialog = new AdornerDialogPresenter(parameter);
            var adorner = new PresenterAdorner(child, contentDialog);
            layer.Add(adorner);
        }
    }
}
