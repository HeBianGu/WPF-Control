// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Modules.Guide;

public interface IGuideOptions
{
    int AnimationDuration { get; set; }
    Color CoverColor { get; set; }
    double CoverOpacity { get; set; }
    Brush Stroke { get; set; }
    DoubleCollection StrokeDashArray { get; set; }
    double StrokeThickness { get; set; }
    double TextMaxWidth { get; set; }
    bool UseOnLoad { get; set; }
}
