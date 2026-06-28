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

public class ExpressionKey : IExpressionKey
{
    /// <summary>
    /// 显示名称ID
    /// </summary>
    [Required]
    public string NameID { get; set; }
    /// <summary>
    /// 显示名称
    /// </summary>
    [JsonIgnore]
    public string Name { get; set; }
    [Required]
    public string GroupNameID { get; set; }
    [JsonIgnore]
    public string GroupName { get; set; }
    [Required]
    public string DataType { get; set; }
    [JsonIgnore]
    public virtual string DisplayName => $"{this.GroupName}.{this.Name}";
    [JsonIgnore]
    public object Value { get; set; }

    public override bool Equals(object obj)
    {
        if (obj is ExpressionKey expression)
            return this.NameID == expression.NameID && this.GroupNameID == expression.GroupNameID && this.DataType == expression.DataType;
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(this.NameID, this.GroupNameID, this.DataType);
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
