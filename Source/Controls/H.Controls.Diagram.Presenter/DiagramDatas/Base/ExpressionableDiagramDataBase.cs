// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.Diagram.Presenter.Expressions;

namespace H.Controls.Diagram.Presenter.DiagramDatas.Base;

public abstract class ExpressionableDiagramDataBase : ZoomableDiagramDataBase, IGetExpressionsable
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

    private ObservableCollection<IVarExpression> _VarExpressions = new ObservableCollection<IVarExpression>();
    public ObservableCollection<IVarExpression> VarExpressions
    {
        get { return _VarExpressions; }
        set
        {
            _VarExpressions = value;
            RaisePropertyChanged();
        }
    }

    [Icon(FontIcons.Globe)]
    [Display(Name = "流程局部变量", GroupName = DiagramDataCommandGroupNames.DataConfiguration, Order = 0)]
    public DisplayCommand ShowVarExpressionKeysCommand => new DisplayCommand(async x =>
    {
        VarExpressionsPresenter varExpressionsPresenter = new VarExpressionsPresenter();
        varExpressionsPresenter.DefaultGroupName = "流程局部变量";
        varExpressionsPresenter.VarExpressions = this.VarExpressions;
        var r = await IocMessage.Dialog.Show(varExpressionsPresenter, x => x.Title = "流程局部变量");
        if (r != true)
            return;
        this.VarExpressions = varExpressionsPresenter.VarExpressions;
    });


    public virtual IEnumerable<IExpression> GetExpressions(Predicate<object> predicate = null)
    {
        //foreach (var item in Controls.Diagram.Presenter.Expressions.ConstNodeDataExpressions.GetDefaults().OfType<IExpressionKey>())
        //{
        //    yield return item;
        //}
        //yield return new ConstExpressionKey<string>("Hello World", this.Name);
        //yield return new ConstExpressionKey<int>(10, this.Name);
        //yield return new ConstExpressionKey<double>(3.14, this.Name);

        foreach (var item in this.VarExpressions)
        {
            yield return item;
        }

        foreach (var item in this.GetPropertyInfoExpressions(this.Name))
        {
            yield return item;
        }
    }

    #endregion
}
