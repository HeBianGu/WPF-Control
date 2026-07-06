// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.Form.PropertyItem.Attribute;
using H.Controls.ShapeBox.Shapes;

namespace H.Components.VisionDiagram.NodeDatas.Measures;
public abstract class PointToPointMeasureNodeDataBase<T> : FromToMeasureNodeDataBase<T, Point, Point> where T : class, IVisionImage
{
    private IExpressionKey _From;
    [GetMethodNameSource(nameof(GetFromNodeDataExpressions))]
    [PropertyItem(typeof(InputExpressionComboBoxTextPropertyItem))]
    [Tab(VisionTabNames.RunParameters)]
    [Display(Name = "选择输入点1", GroupName = VisionTabNames.RunParameters, Description = "用来选择测量输入数据")]
    public IExpressionKey From
    {
        get { return _From; }
        set
        {
            _From = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private IExpressionKey _To;
    [GetMethodNameSource(nameof(GetToNodeDataExpressions))]
    [PropertyItem(typeof(InputExpressionComboBoxTextPropertyItem))]
    [Tab(VisionTabNames.RunParameters)]
    [Display(Name = "选择输入点2", GroupName = VisionTabNames.RunParameters, Description = "用来选择测量输入数据")]
    public IExpressionKey To
    {
        get { return _To; }
        set
        {
            _To = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    protected override IExpressionKey GetFromExpression() => this.From;
    protected override IExpressionKey GetToExpression() => this.To;

    protected override FlowableResult<T> Invoke(T fromImage)
    {
        var fromPoint = this.FromValue;
        var toPoint = this.ToValue;
        this.ResultShapes.Add(new PointShape(fromPoint));
        this.ResultShapes.Add(new PointShape(toPoint));
        this.MeasureResult = (fromPoint - toPoint).Length;
        DimensionShape lineShape = new DimensionShape(fromPoint, toPoint);
        lineShape.Text = this.GetWorldDistance(lineShape.Length);
        if (this.DistanceStroke != null)
            lineShape.Stroke = this.DistanceStroke;
        this.ResultShapes.Add(lineShape);
        var resultImage = this.GetExpressionResultImage(fromImage).Clone();
        return this.OK(resultImage as T, lineShape.ToResultPresenter());
    }
}

