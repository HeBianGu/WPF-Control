// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.ComponentModel.DataAnnotations;

namespace H.Extensions.Validation.Attributes;

/// <summary>
/// 应用TypeConverter去验证数据是否合法
/// </summary>
public class TypeConverterValidationAttribute : ValidationAttribute
{
    public Type Type { get; set; }

    public TypeConverterValidationAttribute(Type type)
    {
        this.Type = type;
    }
}
