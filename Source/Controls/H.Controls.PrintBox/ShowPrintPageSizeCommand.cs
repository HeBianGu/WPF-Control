// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Common.Attributes;
using H.Common.Commands;

namespace H.Controls.PrintBox
{
    [Icon("\xE8FF")]
    [Display(Name = "纸张设置")]
    public class ShowPrintPageSizeCommand : DisplayMarkupCommandBase
    {
        public override async Task ExecuteAsync(object parameter)
        {
            if (parameter is PrintBox pb)
                await pb.ShowPageSize();
            await base.ExecuteAsync(parameter);
        }

        public override bool CanExecute(object parameter)
        {
            return parameter is PrintBox;
        }
    }
}
