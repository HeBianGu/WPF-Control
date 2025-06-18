// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.Diagram.Presenter.Extensions;
using H.Controls.Form;
using H.Presenters.Common;

namespace H.Controls.Diagram.Presenter;

public class CommandsPropertyPresenter : CommandsPresenter<TabFormPresenter>
{
    public IDiagramableNodeData NodeData { get; set; }
    public CommandsPropertyPresenter(IDiagramableNodeData nodeData) : base(new TabFormPresenter(nodeData))
    {
        this.NodeData = nodeData;
    }

    [Icon(FontIcons.Delete)]
    [Display(Name = "确定", GroupName = "操作")]
    public DisplayCommand SumitCommand => new DisplayCommand(x =>
    {
        var dialog = this.GetDialog(x);
        dialog.Sumit();
    });

    protected virtual IDialog GetDialog(object parameter)
    {
        if (parameter is FrameworkElement element)
        {
            if (element is IDialog)
                return element as IDialog;
            FrameworkElement parent = element.GetParent<FrameworkElement>(x => x?.DataContext is IDialog || x is IDialog);
            return parent is IDialog dialog ? dialog : parent.DataContext as IDialog;

        }
        return null;
    }
}
