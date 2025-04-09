// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using System.Windows;

namespace H.Extensions.Command;

[Icon("\xE77F")]
[Display(Name = "粘贴", Description = "从剪贴板粘贴数据")]
public class ClipBoardPasteTextCommand : DisplayMarkupCommandBase
{
    public override bool CanExecute(object parameter)
    {
        return Clipboard.GetText() != null;
    }
    public override void Execute(object parameter)
    {
        if (parameter == null)
            return;
        var text = Clipboard.GetText();
        if (parameter is TextBox tb)
            tb.Text = text;
        if (parameter is ContentControl cc)
            cc.Content = text;
        if (parameter is TextBlock textBlock)
            textBlock.Text = text;
    }
}
