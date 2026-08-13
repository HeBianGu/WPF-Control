// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.Form.PropertyItem.Attribute;

namespace H.Components.VisionDiagram.NodeDatas.Measures;
public abstract class FromToMeasureNodeDataBase<T, FromT, ToT> : MeasureNodeDataBase<T> where T : class, IVisionImage
{
    private double _MeasureResult;
    [Expressionable]
    [Tab(VisionTabNames.ResultParameters)]
    [Display(Name = "测量结果", GroupName = VisionTabNames.ResultParameters, Description = "测量结果值")]
    public double MeasureResult
    {
        get { return _MeasureResult; }
        set
        {
            _MeasureResult = value;
            RaisePropertyChanged();
        }
    }

    private IExpressionKey _ResultImageExpression;
    [GetMethodNameSource(nameof(GetImageFromExpressions))]
    [PropertyItem(typeof(InputExpressionComboBoxTextPropertyItem))]
    [Tab(VisionTabNames.DisplayParameters)]
    [Display(Name = "选择输出图像", GroupName = VisionTabNames.DisplayParameters, Description = "选择前序结果图像作为此流程图像源")]
    public IExpressionKey ResultImageExpression
    {
        get { return _ResultImageExpression; }
        set
        {
            _ResultImageExpression = value;
            RaisePropertyChanged();
        }
    }

    protected T GetExpressionResultImage(T def = default)
    {
        var r = this.GetExpressionValue<T>(this.ResultImageExpression);
        if (!r.success)
            return def;
        return r.value;
    }

    public IEnumerable<IExpressionKey> GetFromNodeDataExpressions() => this.GetFromExpressionKeys<FromT>();

    public IEnumerable<IExpressionKey> GetToNodeDataExpressions() => this.GetFromExpressionKeys<ToT>();


    protected override FlowableResult<T> Invoke(IStartVisionNodeData srcImageNodeData, IVisionNodeData from, T fromImage, IFlowableDiagramData diagram)
    {
        var fromExpression = this.GetFromExpression();
        if (fromExpression == null)
            return this.Error(fromImage, "输入不能为空");
        var toExpression = this.GetToExpression();
        if (toExpression == null)
            return this.Error(fromImage, "输入不能为空");
        if (fromExpression == toExpression)
            return this.Error(fromImage, "输入1和输入2不能是同一值");
        if (this.FromValue.Equals(default(FromT)))
            return this.Continue(fromImage, "输入1不能为默认值");
        if (this.ToValue.Equals(default(ToT)))
            return this.Continue(fromImage, "输入2不能为默认值");
        return base.Invoke(srcImageNodeData, from, fromImage, diagram);
    }

    protected abstract IExpressionKey GetFromExpression();

    protected abstract IExpressionKey GetToExpression();

    protected FromT FromValue
    {
        get
        {
            var r = this.GetExpressionValue<FromT>(this.GetFromExpression());
            return r.value;
        }
    }


    protected ToT ToValue
    {
        get
        {
            var r = this.GetExpressionValue<ToT>(this.GetToExpression());
            return r.value;
        }
    }
}

