// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Globalization;
using System.Windows.Controls;

namespace H.Extensions.Validation.ValidationRules;

public class RequiredValidationRule : ValidationRuleBase
{

    public RequiredValidationRule()
    {
        this.ErrorMessage = "数据不能为空";
    }

    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        if (value != null && value.ToString() != string.Empty)
            return new ValidationResult(true, null);
        else
        {
            return new ValidationResult(false, this.ErrorMessage);
        }
    }
}
