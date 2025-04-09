using System.Threading.Tasks;
using System;
using System.Windows.Media;
using H.Extensions.Common;
using H.Services.Common;
using H.Services.Message.Dialog;

namespace H.Controls.ZoomBox.Extension
{
    public static partial class DialogServiceExtension
    {
        public static async Task<bool?> ShowZoomViewImage(this IDialogMessageService service, Action<IImageZoomViewPresenter> option, Action<IDialog> builder = null)
        {
            return await service.ShowDialog<ImageZoomViewPresenter>(option, null, x =>
            {
                x.DialogButton = DialogButton.None;
                x.MinWidth = 200;
                builder?.Invoke(x);
            });
        }

        public static async Task<bool?> ShowZoomViewImage(this IDialogMessageService service, ImageSource imageSource, Action<IDialog> builder = null)
        {
            return await service.ShowZoomViewImage(x => x.ImageSource = imageSource, builder);
        }

        public static async Task<bool?> ShowZoomViewImage(this IDialogMessageService service, string filePath, Action<IDialog> builder = null)
        {
            return await service.ShowZoomViewImage(x => x.ImageSource = filePath.ToImageSource(), builder);
        }

    }
}
