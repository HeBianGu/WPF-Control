// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Common.Commands;
using H.Components.Visions.OpenCV.Presenters;

namespace H.Components.Visions.OpenCV;

public class ShowOpenCVVisionDiagramPreseterCommand : AsyncMarkupCommandBase
{
    public override async Task ExecuteAsync(object parameter)
    {
        await IocMessage.Dialog.Show(new OpenCVVisionDiagramPreseter());
    }
}
