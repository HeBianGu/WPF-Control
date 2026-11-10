// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Extensions.Expressions.Base;
using H.Mvvm.ViewModels.Base;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Text.Json.Serialization;

namespace H.Extensions.Expressions.Keys;

[DebuggerDisplay("InputExpressionKey:{DisplayName}")]
public class InputExpressionKey : BindableBase, IInputExpressionKey
{
    private string _GroupName;
    [ReadOnly(true)]
    [Display(Name = "分组")]
    public string GroupName
    {
        get { return _GroupName; }
        set
        {
            _GroupName = value;
            RaisePropertyChanged();
        }
    }
    private string _Name;
    [Display(Name = "名称")]
    public string Name
    {
        get { return _Name; }
        set
        {
            _Name = value;
            RaisePropertyChanged();
        }
    }

    private object _DisplayValue;
    /// <summary>
    /// 上一次运行结果值，主要用于表达式的显示参考，不参与表达式的计算，不需要持久化保存
    /// </summary>
    [JsonIgnore]
    [Display(Name = "结果值")]
    public object DisplayValue
    {
        get { return _DisplayValue; }
        set
        {
            _DisplayValue = value;
            RaisePropertyChanged();
        }
    }

    public virtual string DisplayName => $"{this.GroupName}.{this.Name}";


    public override bool Equals(object obj)
    {
        if (obj is IInputExpressionKey expression)
            return this.Name == expression.Name && this.GroupName == expression.GroupName;
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(this.Name, this.GroupName);
    }

    public virtual (bool success, object value) GetInputValue(IEnumerable<IInputExpression> exps)
    {
        var r = this.GetInputExpressions(exps).FirstOrDefault();
        if (r == null)
            return (false, default);
        return (true, r.Value);
    }

    public virtual IEnumerable<IInputExpression> GetInputExpressions(IEnumerable<IInputExpression> exps)
    {
        return exps.Where(x => x.ToKey().Equals(this));
    }
}

public class CollectionInputExpressionKey : InputExpressionKey, IInputExpressionKey
{
    public IInputExpressionKey IndexKey { get; set; }

    public override string DisplayName => $"{this.GroupName}.{this.Name}[{(this.IndexKey is IInputStringExpressionKey primitiveKey ? primitiveKey.Value : this.IndexKey?.DisplayName)}]";

    public override (bool success, object value) GetInputValue(IEnumerable<IInputExpression> exps)
    {
        //在[]内的文本解析成int类型的参数表达式，如果没有解析成功，则返回false，[]空值代表索引0
        var r = this.GetInputExpressions(exps).FirstOrDefault();
        if (r == null)
            return (false, default);
        (bool success, int value) indexresult = this.IndexKey == null ? (true, 0) : this.IndexKey.GetInputValue<int>(exps);
        if (!indexresult.success)
            return (false, default);
        r.Value = r.Value switch
        {
            IEnumerable<object> datas => datas.ElementAt(indexresult.value),
            _ => throw new InvalidOperationException($"无法获取集合类型的值：{r.Name}，类型：{r.Value?.GetType().FullName}")
        };
        return (true, r.Value);
    }
}

