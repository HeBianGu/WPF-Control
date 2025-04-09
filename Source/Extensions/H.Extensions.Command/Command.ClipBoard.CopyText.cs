// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using System.Windows;

namespace H.Extensions.Command;

[Icon("\xE77F")]
[Display(Name = "复制", Description = "复制文本到剪贴板")]
public class ClipBoardCopyTextCommand : DisplayMarkupCommandBase
{
    public override bool CanExecute(object parameter)
    {
        return parameter != null;
    }
    public override void Execute(object parameter)
    {
        if (parameter == null)
            return;
        try
        {
            Clipboard.SetText(parameter.ToString());
        }
        catch
        {

        }
    }
}
