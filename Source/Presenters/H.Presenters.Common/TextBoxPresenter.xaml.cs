using H.Mvvm;
using H.Mvvm.Attributes;
using H.Mvvm.ViewModels.Base;
using System.Threading.Tasks;
using System;
using System.Reflection;
using System.ComponentModel.DataAnnotations;

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
        public static async Task<bool?> ShowTextBox(this IDialogMessageService service, Action<ITextBoxPresenter> option, Action<ITextBoxPresenter> sumitAction = null, Action<IDialog> builder = null, Func<bool> canSumit = null)
        {
            return await service.ShowDialog<TextBoxPresenter>(option, sumitAction, x =>
            {
                x.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Stretch;
                x.MinWidth = 200;
                builder?.Invoke(x);
            }, canSumit);
        }
        public static async Task<bool?> ShowTextBox(this IDialogMessageService service, string text, Action<string> sumitAction = null, Action<IDialog> builder = null, Func<bool> canSumit = null)
        {
            return await service.ShowTextBox(x => x.Text = text, x => sumitAction?.Invoke(x.Text), builder, canSumit);
        }
    }

    public class ShowPropertyTextBoxCommand : DisplayMarkupCommandBase
    {
        public string PropertyName { get; set; }

        public override async Task ExecuteAsync(object parameter)
        {
            if (parameter != null)
            {
                var p = parameter.GetType().GetProperty(this.PropertyName);
                if (p != null)
                {
                    var v = p.GetValue(parameter);
                    var s = v.TryConvertToString();
                    var d = p.GetCustomAttribute<DisplayAttribute>();
                    await IocMessage.Dialog.ShowTextBox(s, x =>
                   {
                       if (x != null)
                       {
                           bool c = x.TryChangeType(p.PropertyType, out var value);
                           if (c)
                           {
                               p.SetValue(parameter, value);
                           }
                       }
                   }, x => x.Title = d?.Name);

                }
            }
            await base.ExecuteAsync(parameter);
        }
    }
}
