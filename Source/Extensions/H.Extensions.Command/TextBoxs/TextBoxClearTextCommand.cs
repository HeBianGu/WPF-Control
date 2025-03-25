// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Extensions.Command.TextBoxs;

[Icon("\xE77F")]
[Display(Name = "删除", Description = "清空当前文本框文本")]
public class TextBoxClearTextCommand : DisplayMarkupCommandBase
{
    public override bool CanExecute(object parameter)
    {
        return parameter is TextBox tb && !string.IsNullOrEmpty(tb.Text);
    }
    public override void Execute(object parameter)
    {
        if (parameter is TextBox tb)
            tb.Text = null;
    }
}
