// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Components.Vision.Presenters;

namespace H.Components.Vision.Base;
public abstract class DemoNodeDataBase : HelpNodeDataBase
{
    #region - 基本参数添加方式示例 -

    private string _demoBaseParamter1;
    [Display(Name = "示例：基本参数", GroupName = VisionPropertyGroupNames.BaseParameters, Description = "用来演示如何增加基本参数", Order = int.MaxValue)]
    public string DemoBaseParamter1
    {
        get { return _demoBaseParamter1; }
        set
        {
            _demoBaseParamter1 = value;
            RaisePropertyChanged();
        }
    }
    #endregion

    #region - 运行参数添加方式示例 -

    private string _demoRunParamter1;
    [Display(Name = "示例：运行参数", GroupName = VisionPropertyGroupNames.RunParameters, Description = "用来演示如何增加结果参数", Order = int.MaxValue)]
    public string DemoRunParamter1
    {
        get { return _demoRunParamter1; }
        set
        {
            _demoRunParamter1 = value;
            RaisePropertyChanged();
        }
    }
    #endregion

    #region - 结果参数添加方式示例 -

    private string _demoResult1;
    [ReadOnly(true)]
    [Expressionable]
    [Display(Name = "示例：结果参数", GroupName = VisionPropertyGroupNames.ResultParameters, Description = "用来演示如何增加结果参数，此结果可应用再条件分支等作为判断参数", Order = int.MaxValue)]
    public string DemoResult1
    {
        get { return _demoResult1; }
        set
        {
            _demoResult1 = value;
            RaisePropertyChanged();
        }
    }
    #endregion

    #region -  帮助页面添加方式示例 -
    public override IHelpPresenter CreateHelpPresenter()
    {
        return base.CreateHelpPresenter();
    }
    #endregion

    //#region - 当前结果添加方式示例 -
    //public override IResultPresenter CreateResultPresenter()
    //{
    //    return Enumerable.Range(1, 3).ToDataGridValueResultPresenter(x => Random.NextDouble().ToString(), x => "示例：输出结果" + x);
    //}
    //#endregion

    #region - 节点表达式添加方式示例 -
    private NodeDataExpression _DemoExpression = new ConstNodeDataExpression<string>(string.Empty, "默认");
    [GetMethodNameSource(nameof(GetStringFromExpressions))]
    [PropertyItem(typeof(ExpressionComboBoxPropertyItem))]
    [Display(Name = "示例：表达式", GroupName = VisionPropertyGroupNames.OtherParameters, Description = "用来演示如何增加节点表达式参数")]
    public NodeDataExpression DemoExpression
    {
        get { return _DemoExpression; }
        set
        {
            _DemoExpression = value;
            RaisePropertyChanged();
        }
    }

    private NodeDataExpression _DemoIntExpression = new ConstNodeDataExpression<int>(0, "默认");
    [GetMethodNameSource(nameof(GetIntFromExpressions))]
    [PropertyItem(typeof(ExpressionComboBoxPropertyItem))]
    [Display(Name = "示例：整型表达式", GroupName = VisionPropertyGroupNames.OtherParameters, Description = "用来演示如何增加节点表达式参数")]
    public NodeDataExpression DemoIntExpression
    {
        get { return _DemoIntExpression; }
        set
        {
            _DemoIntExpression = value;
            RaisePropertyChanged();
        }
    }

    private NodeDataExpression _DemoDoubleExpression = new ConstNodeDataExpression<double>(0.0, "默认");
    [GetMethodNameSource(nameof(GetDoubleFromExpressions))]
    [PropertyItem(typeof(ExpressionComboBoxPropertyItem))]
    [Display(Name = "示例：浮点表达式", GroupName = VisionPropertyGroupNames.OtherParameters, Description = "用来演示如何增加节点表达式参数")]
    public NodeDataExpression DemoDoubleExpression
    {
        get { return _DemoDoubleExpression; }
        set
        {
            _DemoDoubleExpression = value;
            RaisePropertyChanged();
        }
    }

    private DemoExpressionClass _DemoExpressionClass = new DemoExpressionClass();
    [Browsable(false)]
    [JsonIgnore]
    //步骤3：定义一个属性，增加Expressionable特性，并设置ResultParameters
    [Expressionable]
    [Display(Name = "示例：自定义参数", GroupName = VisionPropertyGroupNames.OtherParameters, Description = "用来演示如何增加节点表达式参数")]
    public DemoExpressionClass DemoExpressionClass
    {
        get { return _DemoExpressionClass; }
        set
        {
            _DemoExpressionClass = value;
            RaisePropertyChanged();
        }
    }

    //public override Task<IFlowableResult> TryInvokeAsync(IFlowableLinkData previors, IFlowableDiagramData diagram)
    //{
    //    if (this.TryGetExpressionValue(this.DemoExpression, out object value))
    //    {
    //        System.Diagnostics.Debug.WriteLine(value);
    //    }
    //    return base.TryInvokeAsync(previors, diagram);
    //}

    #endregion

}

