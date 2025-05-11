﻿// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Image;
[Display(Name = "行人", GroupName = "数据源", Order = 20)]
public class Asahiyama : OpenCVImageNodeDataBase
{
    protected override string GetImagePath()
    {
        return ImagePath.Asahiyama;
    }
}
