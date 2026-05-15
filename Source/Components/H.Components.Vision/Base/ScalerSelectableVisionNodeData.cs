// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.Form.PropertyItem.ComboBoxPropertyItems;

namespace H.Components.Vision.Base;
public abstract class ScalerSelectableVisionNodeData<T> : FromImageVisionNodeDataBase<T>, IVisionNodeData<T> where T : class, IVisionImage
{
    private IScalerNodeData _ScalerNodeData;
    [GetMethodNameSource(nameof(GetScalerNodeDatas))]
    [PropertyItem(typeof(ComboBoxPropertyItem))]
    [Display(Name = "比例尺图像源", GroupName = VisionPropertyGroupNames.BaseParameters, Description = "选择比例尺节点源")]
    public IScalerNodeData ScalerNodeData
    {
        get { return _ScalerNodeData; }
        set
        {
            _ScalerNodeData = value;
            RaisePropertyChanged();
        }
    }

    public IEnumerable<IScalerNodeData> GetScalerNodeDatas()
    {
        return this.AllFromNodeDatas.OfType<IScalerNodeData>();
    }

    protected IScalerNodeData GetScalerNodeData()
    {
        if (this.ScalerNodeData != null)
            return this.ScalerNodeData;
        return this.GetScalerNodeDatas()?.FirstOrDefault();
    }

    protected string GetWorldDistance(double px)
    {
        if (this.GetScalerNodeData() is IScalerNodeData scaler)
            return scaler.GetWorldDistance(px);
        return px + "px";
    }
}

