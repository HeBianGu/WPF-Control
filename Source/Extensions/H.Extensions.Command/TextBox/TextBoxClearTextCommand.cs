// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using H.Mvvm;
using H.Mvvm.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Windows.Controls;

namespace H.Extensions.Command
{
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
            {
                tb.Text = null;
            }
        }
    }
}
