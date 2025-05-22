// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using H.Controls.Diagram.Presenter.PortDatas;
global using H.Mvvm.ViewModels.Base;
namespace H.Controls.Diagram.Presenter.Provider;
public class DiagramTheme : BindableBase
{
    private TextNodeData _note = new TextNodeData();
    public TextNodeData Note
    {
        get { return _note; }
        set
        {
            _note = value;
            RaisePropertyChanged();
        }
    }

    private TextLinkData _link = new TextLinkData();
    public TextLinkData Link
    {
        get { return _link; }
        set
        {
            _link = value;
            RaisePropertyChanged();
        }
    }

    private TextPortData _port = new TextPortData();
    public TextPortData Port
    {
        get { return _port; }
        set
        {
            _port = value;
            RaisePropertyChanged();
        }
    }
}
