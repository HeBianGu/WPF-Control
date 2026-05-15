// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.Form.PropertyItem.TextPropertyItems;
using H.Controls.ShapeBox.State;
using H.Controls.ShapeBox.State.Base;

namespace H.Components.Vision.Base;
[Icon(FontIcons.Crop)]
[Display(Name = "绘制ROI")]
public class DrawROIVisionState : ROIRectState
{
    private readonly IROINodeData _rOINodeData;
    public DrawROIVisionState(IROINodeData rOINodeData)
    {
        this._rOINodeData = rOINodeData;
    }

    protected override void OnRectChanged(Rect old, Rect n)
    {
        base.OnRectChanged(old, n);
        this._rOINodeData.ROI = this.ROIRectShape.Rect;
    }


    public override void Enter()
    {
        this.ROIRectShape.Rect = this._rOINodeData.ROI.IsZoreOrEmpty() ? Rect.Empty : this._rOINodeData.ROI;
        base.Enter();
    }
}


public interface IROINodeData : IDiagramableNodeData
{
    Rect ROI { get; set; }
}

public abstract class ROINodeData<T> : WaitFromVisionNodeData<T>, IROINodeData where T : class, IVisionImage
{
    protected override IEnumerable<IViewState> CreateViewStates()
    {
        return base.CreateViewStates().Concat(new DrawROIVisionState(this).ToEnumerable());
    }

    public override void LoadDefault()
    {
        base.LoadDefault();
        this.ROI = Rect.Empty;
    }

    private Rect _ROI = Rect.Empty;
    [PropertyItem(typeof(DeleteTextPropertyItem))]
    [TypeConverter(typeof(Round2RectConverter))]
    [Display(Name = "ROI范围", GroupName = VisionPropertyGroupNames.BaseParameters, Order = 1000)]
    public Rect ROI
    {
        get { return _ROI; }
        set
        {
            _ROI = value;
            RaisePropertyChanged();
        }
    }

    [JsonIgnore]
    [Expressionable]
    [Display(Name = "ROI图像结果", GroupName = VisionPropertyGroupNames.ResultParameters, Description = "当前图像结果进行ROI运算后的图像结果")]
    public virtual T ROIImage { get; set; }

    protected override FlowableResult<T> InvokeAction(Func<FlowableResult<T>> invoke)
    {
        var r = base.InvokeAction(invoke);
        this.ROIImage = this.ROI.IsZoreOrEmpty() ? this.ResultImage : this.ResultImage.ToROIImage(this.ROI) as T;
        return r;
    }

    public override void Dispose()
    {
        base.Dispose();
        this.ROIImage?.Dispose();
    }

    public override void Clear()
    {
        base.Clear();
        this.ROIImage?.Dispose();
    }
}

