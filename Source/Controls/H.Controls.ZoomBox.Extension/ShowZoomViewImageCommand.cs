// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Windows.Media;

namespace H.Controls.ZoomBox.Extension
{
    [Icon("\xEB9F")]
    [Display(Name = "图片")]
    public class ShowZoomViewImageCommand : DisplayMarkupCommandBase
    {
        public override async Task ExecuteAsync(object parameter)
        {
            if (parameter is ImageSource source)
                await IocMessage.Dialog.ShowZoomViewImage(source);
            if (parameter is string filePath)
                await IocMessage.Dialog.ShowZoomViewImage(filePath);
        }
    }
}
