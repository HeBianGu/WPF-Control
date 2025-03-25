﻿// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Extensions.MarkupExtension;

public abstract class GetValueToTypeExtensionBase : System.Windows.Markup.MarkupExtension
{
    public string Value { get; set; }
    public Type Type { get; set; }
}

public abstract class GetValueExtensionBase : System.Windows.Markup.MarkupExtension
{
    public string Value { get; set; }
}

public abstract class GetTypeExtensionBase : System.Windows.Markup.MarkupExtension
{
    public Type Type { get; set; }
}


public abstract class InvokeMethodExtensionBase : System.Windows.Markup.MarkupExtension
{
    public string MethodName { get; set; }
    public object[] Paramenters { get; set; }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        System.Reflection.MethodInfo method = this.GetType().GetMethod(this.MethodName);
        return method?.Invoke(this, this.Paramenters);
    }
}
