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

[Display(Name = "MvCameraControl.Net", GroupName = "视觉与相机", Description = "用于接入海康工业相机。")]
public class MvCameraControlDependencyItem : DependencyItemBase
{
    public MvCameraControlDependencyItem()
    {
        Name = "MvCameraControl.Net";
        Uri = "https://www.hikrobotics.com/cn/machinevision/service/download";
        Version = "由海康机器视觉 SDK 提供";
        Licence = "海康机器视觉 SDK 许可";
        Description = "用于接入海康工业相机。";
    }
}
