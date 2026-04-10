// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Common.Interfaces;
using H.Common.Transitionable;
using H.Services.Message.Dialog;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace H.Windows.Dialog;

public partial class TransparencyDialogWindow : DialogWindow, IDialog
{
    public TransparencyDialogWindow()
    {


    }
    static TransparencyDialogWindow()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(TransparencyDialogWindow), new FrameworkPropertyMetadata(typeof(TransparencyDialogWindow)));
    }
}

public partial class TransparencyDialogWindow
{
    protected override ResourceKey GetResourceKey(DialogButton dialogButton)
    {
        switch (dialogButton)
        {
            case DialogButton.Sumit:
                return TransparencyDialogKeys.Sumit;
            case DialogButton.None:
                return TransparencyDialogKeys.None;
            case DialogButton.Cancel:
                return TransparencyDialogKeys.Cancel;
            case DialogButton.SumitAndCancel:
                return TransparencyDialogKeys.SumitAndCancel;
            default:
                return TransparencyDialogKeys.Sumit;
        }
    }
}
