using H.Common.Commands;
using H.Services.Common.Guide;
using H.Services.Message;

namespace H.Modules.Guide;

public abstract class ShowGuideTreeCommandBase : DisplayMarkupCommandBase
{
    public override async Task ExecuteAsync(object parameter)
    {
        UIElement element = parameter is UIElement e ? e : GuideExtension.GetAdornerElement();
        var tree = element.GetGuideTree(this.IsMatch);
        var presenter = new GuideTreePresenter(tree);
        await IocMessage.Dialog.Show(presenter, x =>
        {
            x.MinWidth = 500;
            x.HorizontalContentAlignment = HorizontalAlignment.Stretch;
        });
    }

    protected abstract bool IsMatch(UIElement element);

    public override bool CanExecute(object parameter)
    {
        return base.CanExecute(parameter) && Ioc<IGuideService>.Instance != null;
    }
}