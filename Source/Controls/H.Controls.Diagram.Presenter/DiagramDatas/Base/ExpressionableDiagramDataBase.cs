// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.Diagram.Presenter.Expressions;

namespace H.Controls.Diagram.Presenter.DiagramDatas.Base;

public abstract class ExpressionableDiagramDataBase : ZoomableDiagramDataBase, IExpressionable
{
    #region - 节点表达式添加方式示例 -

    private DemoExpressionClass _DemoExpressionClass = new DemoExpressionClass();
    [Browsable(false)]
    [JsonIgnore]
    //步骤3：定义一个属性，增加Expressionable特性，并设置ResultParameters
    [Expressionable]
    [Display(Name = "示例：自定义参数", Description = "用来演示如何增加节点表达式参数")]
    public DemoExpressionClass DemoExpressionClass
    {
        get { return _DemoExpressionClass; }
        set
        {
            _DemoExpressionClass = value;
            RaisePropertyChanged();
        }
    }

    [Expressionable]
    [Display(Name = "示例：自定义字符串", Description = "用来演示如何增加节点表达式参数")]
    public string StringValue { get; set; } = "Hello World";

    private ObservableCollection<IConstNodeDataExpression> _ConstNodeDataExpressions = new ObservableCollection<IConstNodeDataExpression>();
    public ObservableCollection<IConstNodeDataExpression> ConstNodeDataExpressions
    {
        get { return _ConstNodeDataExpressions; }
        set
        {
            _ConstNodeDataExpressions = value;
            RaisePropertyChanged();
        }
    }

    [Icon(FontIcons.Globe)]
    [Display(Name = "全局变量设置", GroupName = "操作", Order = 0)]
    public DisplayCommand ShowConstExpressionsCommand => new DisplayCommand(async x =>
    {
        ConstNodeDataExpressionsPresenter constNodeDataExpressionsPresenter = new ConstNodeDataExpressionsPresenter();
        constNodeDataExpressionsPresenter.ConstNodeDataExpressions = this.ConstNodeDataExpressions;
        var r = await IocMessage.Dialog.Show(constNodeDataExpressionsPresenter);
        if (r != true)
            return;
        this.ConstNodeDataExpressions = constNodeDataExpressionsPresenter.ConstNodeDataExpressions;
    });


    public virtual IEnumerable<NodeDataExpression> GetExpressions()
    {
        foreach (var item in Controls.Diagram.Presenter.Expressions.ConstNodeDataExpressions.GetDefaults().OfType<NodeDataExpression>())
        {
            yield return item;
        }
        yield return new ConstNodeDataExpression<string>("Hello World", this.Name);
        yield return new ConstNodeDataExpression<int>(10, this.Name);
        yield return new ConstNodeDataExpression<double>(3.14, this.Name);

        foreach (var item in this.ConstNodeDataExpressions.OfType<NodeDataExpression>())
        {
            yield return item;
        }

        foreach (var item in this.GetExpressions(this.Name))
        {
            yield return item;
        }
    }

    public virtual bool TryGetExpressionValue(NodeDataExpression expression, out object value)
    {
        return expression.TryGetExpressionValue(this, out value);
    }

    #endregion
}
