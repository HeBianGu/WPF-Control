// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Extensions.Setting;
using H.Services.Setting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace H.Controls.ShapeBox;

public static class Extention
{
    public static void UseShapeStyleOptions(this IApplicationBuilder service)
    {
        service.AddSetting(ROIRectStateStyleSetting.Instance);
        service.AddSetting(RulerLineShapeStyleSetting.Instance);
        service.AddSetting(LineShapeStyleSetting.Instance);
        service.AddSetting(PointShapeStyleSetting.Instance);
        service.AddSetting(CircleShapeStyleSetting.Instance);
        service.AddSetting(CaliperShapeStyleSetting.Instance);
        service.AddSetting(PickPixelShapeStyleSetting.Instance);
    }
}
