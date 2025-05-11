// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using H.Common.Attributes;
global using H.Common.Commands;
global using H.Services.Message;
global using H.Services.Message.IODialog;
global using System.ComponentModel.DataAnnotations;

namespace H.Controls.ZoomBox.Extension
{
    [Icon("\xEB9F")]
    [Display(Name = "图片")]
    public class ShowZoomViewImageFileCommand : DisplayMarkupCommandBase
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
            await IocMessage.Dialog.ShowZoomViewImage(path);
        }
    }
}
