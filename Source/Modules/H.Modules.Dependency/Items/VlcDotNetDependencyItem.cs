// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")


// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Modules.Dependency.Base;

namespace H.Modules.Dependency.Items;

[Display(Name = "Vlc.DotNet", GroupName = "媒体与音频", Description = "用于 VLC 音视频播放。")]
public class VlcDotNetDependencyItem : DependencyItemBase
{
    public VlcDotNetDependencyItem()
    {
        Name = "Vlc.DotNet";
        Uri = "https://github.com/ZeBobo5/Vlc.DotNet";
        Version = "3.1.0";
        Licence = "LGPL License";
        Description = "用于 VLC 音视频播放。";
    }
}
