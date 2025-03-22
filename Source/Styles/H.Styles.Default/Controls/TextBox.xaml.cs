using H.Mvvm;
using H.Mvvm.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace H.Styles.Default
{
    public class TextBoxKeys
    {
        public static ComponentResourceKey Default => new ComponentResourceKey(typeof(TextBoxKeys), "S.TextBox.Default");

        public static ComponentResourceKey Attach => new ComponentResourceKey(typeof(TextBoxKeys), "S.TextBox.Attach");

        public static ComponentResourceKey Delete => new ComponentResourceKey(typeof(TextBoxKeys), "S.TextBox.Delete");

        public static ComponentResourceKey Edit => new ComponentResourceKey(typeof(TextBoxKeys), "S.TextBox.Edit");

    }

    [Icon("\xE77F")]
    [Display(Name = "删除", Description = "清空文本框文本")]
    public class DeleteTextTextBoxCommand : DisplayMarkupCommandBase
    {
        public override void Execute(object parameter)
        {
            if (parameter is TextBox textBox)
            {
                textBox.Text = string.Empty;
            }
        }

        public override bool CanExecute(object parameter)
        {
            if (parameter is TextBox textBox)
            {
                return !string.IsNullOrEmpty(textBox.Text);
            }
            return false;
        }
    }
}
