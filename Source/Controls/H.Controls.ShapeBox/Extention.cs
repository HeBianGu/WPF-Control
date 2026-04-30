// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.ShapeBox.Settables.ViewSettables;
using H.Services.Setting;

namespace H.Controls.ShapeBox;
public static class Extention
{
    public static void UseShapeBoxSetting(this IApplicationBuilder service)
    {
        service.UseShapeViewSetting();
        service.UseStateShapeViewSetting();
        service.UseSelectShapeViewSetting();
        service.UsePreviewShapeViewSetting();
        service.UseMouseOverShapeViewSetting();
    }
    public static void UseShapeStyleOptions(this IApplicationBuilder service)
    {
        service.AddSetting(RulerLineShapeStyleSetting.Instance);
        service.AddSetting(LineShapeStyleSetting.Instance);
        service.AddSetting(PointShapeStyleSetting.Instance);
        service.AddSetting(CircleShapeStyleSetting.Instance);
        service.AddSetting(CaliperShapeStyleSetting.Instance);
        service.AddSetting(PickPixelShapeStyleSetting.Instance);
    }

    public static void UseROIRectStateStyleSetting(this IApplicationBuilder service, Action<IROIRectStateStyleOption> action = null)
    {
        action?.Invoke(ROIRectStateStyleSetting.Instance);
        service.AddSetting(ROIRectStateStyleSetting.Instance);
    }

    public static void UsePreviewShapeViewSetting(this IApplicationBuilder service, Action<PreviewShapeViewSetting> builder = null)
    {
        builder?.Invoke(PreviewShapeViewSetting.Instance);
        service.AddSetting(PreviewShapeViewSetting.Instance);
    }
    public static void UseSelectShapeViewSetting(this IApplicationBuilder service, Action<SelectShapeViewSetting> builder = null)
    {
        builder?.Invoke(SelectShapeViewSetting.Instance);
        service.AddSetting(SelectShapeViewSetting.Instance);
    }
    public static void UseShapeViewSetting(this IApplicationBuilder service, Action<ShapeViewSetting> builder = null)
    {
        builder?.Invoke(ShapeViewSetting.Instance);
        service.AddSetting(ShapeViewSetting.Instance);
    }

    public static void UseStateShapeViewSetting(this IApplicationBuilder service, Action<StateShapeViewSetting> builder = null)
    {
        builder?.Invoke(StateShapeViewSetting.Instance);
        service.AddSetting(StateShapeViewSetting.Instance);
    }

    public static void UseMouseOverShapeViewSetting(this IApplicationBuilder service, Action<MouseOverShapeViewSetting> builder = null)
    {
        builder?.Invoke(MouseOverShapeViewSetting.Instance);
        service.AddSetting(MouseOverShapeViewSetting.Instance);
    }
}
