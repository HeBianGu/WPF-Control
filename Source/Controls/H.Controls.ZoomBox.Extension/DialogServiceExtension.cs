// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Extensions.Common;
using H.Services.Message.Dialog;
using System.Windows.Media;

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
