// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Common.Commands;
using H.Modules.Guide.Base;
using H.Services.Common.Guide;
using H.Services.Message;

namespace H.Modules.Guide.Commands;

public abstract class ShowGuideTreeCommandBase : DisplayMarkupCommandBase
{
    public override async Task ExecuteAsync(object parameter)
    {
        UIElement element = parameter is UIElement e ? e : GuideExtension.GetAdornerElement();
        var tree = element.GetGuideTree(this.IsMatch);
        var presenter = new GuideTreePresenter(tree);
        await IocMessage.ShowDialog(presenter, x =>
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