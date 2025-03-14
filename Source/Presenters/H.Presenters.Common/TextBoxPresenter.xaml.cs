using H.Mvvm;
using H.Mvvm.Attributes;
using H.Mvvm.ViewModels.Base;
using System.Threading.Tasks;
using System;

namespace H.Presenters.Common
{
    public interface ITextBoxPresenter
    {
        string Text { get; set; }
    }

    [Icon("\xE70F")]
    public class TextBoxPresenter : DisplayBindableBase, ITextBoxPresenter
    {
        public string Text { get; set; }
    }

    public static partial class DialogServiceExtension
    {
        public static async Task<bool?> ShowTextBox(this IDialogMessageService service, Action<ITextBoxPresenter> option, Action<ITextBoxPresenter> sumitAction, Action<IDialog> builder = null, Func<bool> canSumit = null)
        {
            return await service.ShowDialog<TextBoxPresenter>(option, sumitAction, builder, canSumit);
        }
        public static async Task<bool?> ShowTextBox(this IDialogMessageService service, string text, Action<string> sumitAction, Action<IDialog> builder = null, Func<bool> canSumit = null)
        {
            return await service.ShowTextBox(x => x.Text = text, x => sumitAction?.Invoke(x.Text), builder, canSumit);
        }
    }
}
