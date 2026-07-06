// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.Form.PropertyItem.Attribute;
using H.Controls.Form.PropertyItem.ComboBoxPropertyItems;

namespace H.Components.VisionDiagram.NodeDatas.Measures;
//  ToDo：增加线圆测量，圆圆测量（最远距离，最近距离，圆心距离，圆心最远距离等）

public abstract class MeasureNodeDataBase<T> : ROINodeData<T> where T : class, IVisionImage
{
    private Brush _distanceStroke = Brushes.Cyan;
    [GetHightlightBrushesSource]
    [PropertyItem(typeof(BrushComboBoxPropertyItem))]
    [Tab(VisionTabNames.DisplayParameters)]
    [Display(Name = "测量结果绘制颜色", GroupName = VisionTabNames.DisplayParameters, Description = "选择比例尺节点源")]
    public Brush DistanceStroke
    {
        get { return _distanceStroke; }
        set
        {
            _distanceStroke = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }
}

