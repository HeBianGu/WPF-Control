using H.Mvvm;
using H.Mvvm.Attributes;
using H.Mvvm.ViewModels.Base;
using System.Threading.Tasks;
using System;
using System.ComponentModel.DataAnnotations;

namespace H.Presenters.Common
{
    public interface IFilePathTextBoxPresenter : IOpenFilePathable
    {

    }

    [Icon("\xE70F")]
    [Display(Name = "打开文件")]
    public class FilePathTextBoxPresenter : DisplayBindableBase, IFilePathTextBoxPresenter
    {
        private string _filePath;
        public string FilePath
        {
            get { return _filePath; }
            set
            {
                _filePath = value;
                RaisePropertyChanged();
            }
        }
        public string Filter { get; set; } = IIOFileDialogOption.defaultFilter;
    }

    public static partial class DialogServiceExtension
    {
        public static async Task<bool?> ShowFilePathTextBox(this IDialogMessageService service, Action<IFilePathTextBoxPresenter> option, Action<IFilePathTextBoxPresenter> sumitAction, Action<IDialog> builder = null, Func<bool> canSumit = null)
        {
            return await service.ShowDialog<FilePathTextBoxPresenter>(option, sumitAction, builder, canSumit);
        }
    }
}
