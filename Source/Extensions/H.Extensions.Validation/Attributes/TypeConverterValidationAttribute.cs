// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

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
