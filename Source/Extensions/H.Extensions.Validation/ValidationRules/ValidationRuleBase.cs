// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Windows.Controls;

namespace H.Extensions.Validation.ValidationRules;

public abstract class ValidationRuleBase : ValidationRule
{
    public string ErrorMessage { get; set; } = "数据不匹配";
}
