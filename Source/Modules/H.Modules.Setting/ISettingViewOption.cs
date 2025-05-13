// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Services.Message.Form;

namespace H.Modules.Setting;

public interface ISettingViewOptions
{
    double Height { get; set; }
    Thickness Margin { get; set; }
    double MinHeight { get; set; }
    double MinWidth { get; set; }
    double NavigationiTitleWidth { get; set; }
    double TitleWidth { get; set; }
    double Width { get; set; }
    IFormOption FormOption { get; }
}
