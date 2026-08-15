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

[Display(Name = "OpenCvSharp4", GroupName = "视觉与相机", Description = "OpenCV 的 .NET 封装，用于图像处理。")]
public class OpenCvSharp4DependencyItem : DependencyItemBase
{
    public OpenCvSharp4DependencyItem()
    {
        Name = "OpenCvSharp4";
        Author = "shimat";
        Uri = "https://github.com/shimat/opencvsharp";
        Version = "4.13.0.20260222";
        Licence = "Apache License 2.0";
        Description = "OpenCV 的 .NET 封装，用于图像处理。";
    }
}

