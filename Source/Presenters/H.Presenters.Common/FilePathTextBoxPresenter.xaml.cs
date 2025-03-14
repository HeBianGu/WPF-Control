using H.Mvvm;
using H.Mvvm.Attributes;
using H.Mvvm.ViewModels.Base;
using System.Threading.Tasks;
using System;

namespace H.Presenters.Common
{
    public interface IFilePathTextBoxPresenter
    {
        string FilePath { get; set; }
    }

    [Icon("\xE70F")]
    public class FilePathTextBoxPresenter : DisplayBindableBase, IFilePathTextBoxPresenter
    {
        public string FilePath { get; set; }
    }

    public static partial class DialogServiceExtension
    {
        public static async Task<bool?> ShowFilePathTextBox(this IDialogMessageService service, Action<IFilePathTextBoxPresenter> option, Action<IFilePathTextBoxPresenter> sumitAction, Action<IDialog> builder = null, Func<bool> canSumit = null)
        {
            return await service.ShowDialog<FilePathTextBoxPresenter>(option, sumitAction, builder, canSumit);
        }
    }
}
