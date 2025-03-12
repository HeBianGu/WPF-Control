using H.Mvvm.Attributes;
using H.Services.Common;
namespace H.Modules.Guide;
[Icon("\xEC92")]
[Display(Name = "功能列表", Description = "显示版本功能列表")]
public class ShowGuideTreeCommand : DisplayMarkupCommandBase
{
    public override async Task ExecuteAsync(object parameter)
    {
        UIElement element = parameter is UIElement e ? e : GuideExtension.GetAdornerElement();
        var tree = element.GetGuideTree();
        var presenter = new GuideTreePresenter(tree);
        await IocMessage.Dialog.Show(presenter);
    }
}