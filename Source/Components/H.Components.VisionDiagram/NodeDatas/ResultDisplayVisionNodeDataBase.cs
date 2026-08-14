// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.Form.PropertyItem.Attribute;
using H.Controls.Form.PropertyItem.ComboBoxPropertyItems;
using H.Controls.Form.PropertyItem.TextPropertyItems;
using H.Controls.Form.PropertyItems;
using H.Mvvm.ViewModels.Base;

namespace H.Components.VisionDiagram.NodeDatas;
public class VisionResultDisplay : BindableBase
{

}

public class TextVisionResultDisplay : VisionResultDisplay
{
    //private string _TextFormat;
    //[Display(Name = "内容", GroupName = VisionTabNames.ResultParameters, Description = "文本格式")]
    //public string TextFormat
    //{
    //    get { return _TextFormat; }
    //    set
    //    {
    //        _TextFormat = value;
    //        RaisePropertyChanged();
    //    }
    //}

    private Color _OKColor = Colors.Chartreuse;
    [GetHightlightColorsSource]
    [PropertyItem(typeof(ColorComboBoxPropertyItem))]
    [Display(Name = "OK颜色", GroupName = VisionTabNames.ResultParameters, Description = "设置检测结果为 OK 时的文本显示颜色")]
    public Color OKColor
    {
        get { return _OKColor; }
        set
        {
            _OKColor = value;
            RaisePropertyChanged();
        }
    }

    private Color _NGColor = Colors.Red;
    [GetHightlightColorsSource]
    [PropertyItem(typeof(ColorComboBoxPropertyItem))]
    [Display(Name = "NG颜色", GroupName = VisionTabNames.ResultParameters, Description = "设置检测结果为 NG 时的文本显示颜色")]
    public Color NGColor
    {
        get { return _NGColor; }
        set
        {
            _NGColor = value;
            RaisePropertyChanged();
        }
    }

    private double _FontSize = 10.0d;
    [PropertyItem(typeof(DoubleSliderTextPropertyItem))]
    [Range(6.0d, 72.0d)]
    [DefaultValue(10.0d)]
    [Display(Name = "字号", GroupName = VisionTabNames.ResultParameters, Description = "设置结果文本在图像上的显示字号")]
    public double FontSize
    {
        get { return _FontSize; }
        set
        {
            _FontSize = value;
            RaisePropertyChanged();
        }
    }

    private double _Opacity = 1.0d;
    [PropertyItem(typeof(DoubleSliderTextPropertyItem))]
    [Range(0.0d, 1.0d)]
    [DefaultValue(1.0d)]
    [Display(Name = "透明度", GroupName = VisionTabNames.ResultParameters, Description = "设置结果文本在图像上的显示透明度")]
    public double Opacity
    {
        get { return _Opacity; }
        set
        {
            _Opacity = value;
            RaisePropertyChanged();
        }
    }

    private double _X;
    [Display(Name = "位置X", GroupName = VisionTabNames.ResultParameters, Description = "设置结果文本左上角的水平坐标")]
    public double X
    {
        get { return _X; }
        set
        {
            _X = value;
            RaisePropertyChanged();
        }
    }

    private double _Y;
    [Display(Name = "位置Y", GroupName = VisionTabNames.ResultParameters, Description = "设置结果文本左上角的垂直坐标")]
    public double Y
    {
        get { return _Y; }
        set
        {
            _Y = value;
            RaisePropertyChanged();
        }
    }
}

public abstract class ResultDisplayVisionNodeDataBase<T> : VisionNodeData<T> where T : class, IVisionImage
{
    private IExpressionKey _TextFormatExpression;
    [BindingVisibleablePropertyName(nameof(UseTestVisionResultDisplay))]
    [GetMethodNameSource(nameof(GetPrimitiveFromExpressionKeys))]
    [PropertyItem(typeof(InputExpressionComboBoxTextPropertyItem<string>))]
    [Tab(VisionTabNames.ResultDisplayParameters)]
    [Display(Name = "内容", GroupName = VisionGroupNames.TextDisplay, Description = "文本格式", Order = 1)]
    public IExpressionKey TextFormatExpression
    {
        get { return _TextFormatExpression; }
        set
        {
            _TextFormatExpression = value;
            RaisePropertyChanged();
        }
    }

    private TextVisionResultDisplay _TestVisionResultDisplay = new TextVisionResultDisplay();
    [BindingVisibleablePropertyName(nameof(UseTestVisionResultDisplay))]
    [PropertyStyle(UseTitle = false)]
    [PropertyItem(typeof(FormPropertyItem))]
    [Tab(VisionTabNames.ResultDisplayParameters)]
    [Display(Name = "文本显示", GroupName = VisionGroupNames.TextDisplay, Description = "配置检测结果文本在图像上的显示样式", Order = 16)]
    public TextVisionResultDisplay TestVisionResultDisplay
    {
        get { return _TestVisionResultDisplay; }
        set
        {
            _TestVisionResultDisplay = value;
            RaisePropertyChanged();
        }
    }
    public bool UseTestVisionResultDisplay { get; set; } = true;

    private string _TextDisplayResult;
    [Tab(VisionTabNames.ResultParameters)]
    [Display(Name = "内容结果", GroupName = VisionGroupNames.TextDisplay, Description = "设置需要叠加到结果图像上的文本内容", Order = 16)]
    public string TextDisplayResult
    {
        get { return _TextDisplayResult; }
        set
        {
            _TextDisplayResult = value;
            RaisePropertyChanged();
        }
    }

    public override IFlowableResult Invoke(IFlowableLinkData previors, IFlowableDiagramData diagram)
    {
        var r = base.Invoke(previors, diagram);
        var t = this.GetExpressionValue(this.TextFormatExpression);
        if (t.success)
        {
            this.TextDisplayResult = t.value?.ToString();
            var textShape = this.TestVisionResultDisplay.ToTextShape(this.TextDisplayResult, x =>
            {
                x.TextForeground = r.State == FlowableResultState.OK ? this.TestVisionResultDisplay.OKColor.ToFreezeSolid()
                : this.TestVisionResultDisplay.NGColor.ToFreezeSolid();
            });
            this.ResultShapes.Add(textShape);
        }
        return r;
    }
}

