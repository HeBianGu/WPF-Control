// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

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
