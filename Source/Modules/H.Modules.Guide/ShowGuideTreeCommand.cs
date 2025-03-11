using H.Mvvm.Attributes;
using H.Services.Common;
namespace H.Modules.Guide;
[Icon("\xE713")]
[Display(Name = "我的操作", Description = "默认操作")]
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