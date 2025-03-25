// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Windows.Input;

namespace H.Services.Message.Dialog.Commands;


public abstract class ShowMessageDialogCommandBase : ShowDialogCommandBase, ICommand
{
    public string Message { get; set; }
}
