// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Components.VisionDiagram.Base;
using H.Controls.ShapeBox.State.Base;

namespace H.Components.VisionDiagram.NodeDatas;
[Icon(FontIcons.Photo2)]
public abstract class StartVisionNodeData<T> : NinePointCalibrationNodeData<T>, IStartVisionNodeData<T> where T : class, IVisionImage
{
    protected StartVisionNodeData()
    {
        this.UseStart = true;
    }

    protected override bool IsFromImageValid(T fromImage)
    {
        return fromImage?.IsValid() != false;
    }

    protected override IEnumerable<IViewState> CreateViewStates()
    {
        foreach (var item in base.CreateViewStates())
        {
            yield return item;
        }
        yield return new DrawScalerLineShapeState(this);
    }

    [JsonIgnore]
    [Expressionable]
    [Tab(VisionTabNames.ResultParameters)]
    [Display(Name = "灰度图像结果", GroupName = VisionTabNames.ResultParameters, Description = "当前流程运行完返回的灰度图像结果")]
    public virtual T GrayImage { get; set; }

    private double _Scaler;
    [Tab(VisionTabNames.RunParameters)]
    [Display(Name = "实际长度/像素长度", GroupName = VisionTabNames.RunParameters)]
    public double Scaler
    {
        get { return _Scaler; }
        set
        {
            _Scaler = value;
            RaisePropertyChanged();
        }
    }

    private int _pixelWidth;
    [ReadOnly(true)]
    [Expressionable]
    [Tab(VisionTabNames.ResultParameters)]
    [Display(Name = "图像宽度", GroupName = VisionTabNames.ResultParameters)]
    public int PixelWidth
    {
        get { return _pixelWidth; }
        set
        {
            _pixelWidth = value;
            RaisePropertyChanged();
        }
    }

    private int _pixelHeight;
    [ReadOnly(true)]
    [Expressionable]
    [Tab(VisionTabNames.ResultParameters)]
    [Display(Name = "图像高度", GroupName = VisionTabNames.ResultParameters)]
    public int PixelHeight
    {
        get { return _pixelHeight; }
        set
        {
            _pixelHeight = value;
            RaisePropertyChanged();
        }
    }

    private int _imageColorType;
    [ReadOnly(true)]
    [Expressionable]
    [Tab(VisionTabNames.ResultParameters)]
    [Display(Name = "颜色类型", GroupName = VisionTabNames.ResultParameters)]
    public int ImageColorType
    {
        get { return _imageColorType; }
        set
        {
            _imageColorType = value;
            RaisePropertyChanged();
        }
    }

    string IScalerNodeData.GetWorldDistance(double px)
    {
        GetWorldDistance(px);
        return this.Scaler == 0 ? string.Format("{0:F2}px", px)
                  : string.Format("{0:F2}mm", px * this.Scaler);
    }
    public override void Dispose()
    {
        base.Dispose();
        this.GrayImage?.Dispose();
    }

    public override void Clear()
    {
        base.Clear();
        this.GrayImage?.Dispose();
    }
}


