// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace H.Extensions.Validation.Attributes;

public class IPAddressAttribute : RegularExpressionAttribute
{
    public IPAddressAttribute() : base(@"^((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$")
    {
    }
}
