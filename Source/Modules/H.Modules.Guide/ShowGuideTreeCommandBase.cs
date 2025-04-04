﻿namespace H.Modules.Guide;

public abstract class ShowGuideTreeCommandBase : DisplayMarkupCommandBase
{
    public override async Task ExecuteAsync(object parameter)
    {
        UIElement element = parameter is UIElement e ? e : GuideExtension.GetAdornerElement();
        var tree = element.GetGuideTree(this.IsMatch);
        var presenter = new GuideTreePresenter(tree);
        await IocMessage.Dialog.Show(presenter);
    }

    protected abstract bool IsMatch(UIElement element);
}