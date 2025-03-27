using H.Common.Commands;
using H.Extensions.Common;
using System.ComponentModel.DataAnnotations;
using System.Windows.Media;

namespace H.Presenters.Common;

public interface IImageViewPresenter
{
    ImageSource ImageSource { get; set; }
}

[Icon("\xEB9F")]
[Display(Name = "图片")]
public class ImageViewPresenter : DisplayBindableBase, IImageViewPresenter
{
    private ImageSource _imageSource;
    public ImageSource ImageSource
    {
        get { return _imageSource; }
        set
        {
            _imageSource = value;
            RaisePropertyChanged();
        }
    }
}

public static partial class DialogServiceExtension
{
    public static async Task<bool?> ShowImageSource(this IDialogMessageService service, Action<IImageViewPresenter> option, Action<IDialog> builder = null)
    {
        return await service.ShowDialog<ImageViewPresenter>(option, null, x =>
        {
            x.DialogButton = DialogButton.None;
            x.MinWidth = 200;
            builder?.Invoke(x);
        });
    }

    public static async Task<bool?> ShowImageSource(this IDialogMessageService service, ImageSource imageSource, Action<IDialog> builder = null)
    {
        return await service.ShowImageSource(x => x.ImageSource = imageSource, builder);
    }

    public static async Task<bool?> ShowImageSource(this IDialogMessageService service, string filePath, Action<IDialog> builder = null)
    {
        return await service.ShowImageSource(x => x.ImageSource = filePath.ToImageSource(), builder);
    }

}

[Icon("\xEB9F")]
[Display(Name = "图片")]
public class ShowImageFileCommand : DisplayMarkupCommandBase
{
    public string FilePath { get; set; }
    public bool UseCache { get; set; } = true;
    public override async Task ExecuteAsync(object parameter)
    {
        var path = parameter?.ToString() ?? this.FilePath;
        if (string.IsNullOrEmpty(path))
        {
            IocMessage.IOFileDialog.ShowOpenImageFile(x =>
            {
                path = x;
                if (this.UseCache)
                    this.FilePath = path;
            });
        }
        await IocMessage.Dialog.ShowImageSource(path);
    }
}

[Icon("\xEB9F")]
[Display(Name = "图片")]
public class ShowImageSourceCommand : DisplayMarkupCommandBase
{
    public override async Task ExecuteAsync(object parameter)
    {
        if (parameter is ImageSource source)
            await IocMessage.Dialog.ShowImageSource(source);
    }
}
