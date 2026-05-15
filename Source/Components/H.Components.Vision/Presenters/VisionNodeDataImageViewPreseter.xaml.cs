// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Components.Vision.Base;
using H.Extensions.Mvvm.ViewModels.Base;
using H.ValueConverter;
using System.Globalization;

namespace H.Components.Vision.Presenters;


[Icon(FontIcons.Photo)]
[Display(Name = "节点结果视图", GroupName = "机器视觉", Order = 0)]
public class VisionNodeDataImageViewPreseter : DisplayBindableBase
{
    public VisionNodeDataImageViewPreseter(IVisionNodeData visionNodeData)
    {
        this.VisionNodeData = visionNodeData;
    }

    private IVisionNodeData _VisionNodeData;
    public IVisionNodeData VisionNodeData
    {
        get { return _VisionNodeData; }
        set
        {
            _VisionNodeData = value;
            RaisePropertyChanged();
        }
    }


}


public class GetVisionNodeDataImageViewPreseterValueConverter : MarkupValueConverterBase
{
    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is IVisionNodeData visionNodeData)
            return new VisionNodeDataImageViewPreseter(visionNodeData);
        return DependencyProperty.UnsetValue;
    }
}