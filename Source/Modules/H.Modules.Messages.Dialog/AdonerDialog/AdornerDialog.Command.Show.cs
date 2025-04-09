global using H.Common.Commands;
global using System.ComponentModel.DataAnnotations;

namespace H.Modules.Messages.Dialog
{
    [Icon("\xE77F")]
    [Display(Name = "显示", Description = "显示对话框页面")]
    public class ShowAdornerDialogCommand : DisplayMarkupCommandBase
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
