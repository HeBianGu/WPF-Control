// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.Diagram.Presenter.Expressions;

public interface IExpressionKey
{
    /// <summary>
    /// 唯一名称
    /// </summary>
    string Name { get; set; }
    /// <summary>
    /// 分组名称
    /// </summary>
    string GroupName { get; set; }
    /// <summary>
    /// 数据类型
    /// </summary>
    string DataType { get; set; }

    object Value { get; set; }
}

public class ExpressionKey : BindableBase, IExpressionKey
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

    [Display(Name = "注释")]
    public string Description { get; set; }
    public string DataType { get; set; }
    public virtual string DisplayName => $"{this.GroupName}.{this.Name}";
    [JsonIgnore]
    public object Value { get; set; }

    public override bool Equals(object obj)
    {
        if (obj is IExpressionKey expression)
            return this.Name == expression.Name && this.GroupName == expression.GroupName && this.DataType == expression.DataType;
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(this.Name, this.GroupName, this.DataType);
    }
}

//public interface INodeDataExpressionKey : IExpressionKey
//{
//    string Path { get; set; }
//}

//public class ExpressionKey : INodeDataExpressionKey
//{
//    public string Path { get; set; }
//    public string Type { get; set; }
//    public string GroupName { get; set; }
//    [Required]
//    [Display(Name = "属性名称")]
//    public string Name { get; set; }
//    public override bool Equals(object obj)
//    {
//        if (obj is ExpressionKey expression)
//            return this.Path == expression.Path && this.GroupName == expression.GroupName && this.Name == expression.Name;
//        return false;
//    }

//    public override int GetHashCode()
//    {
//        return HashCode.Combine(this.Path, this.GroupName, this.Name);
//    }
//}
