// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Controls.Form.Attributes;

[AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
public class BindingVisiblableMethodNameAttribute : Attribute
{
    public BindingVisiblableMethodNameAttribute(string methodName)
    {
        this.MethodName = methodName;
    }

    public string MethodName { get; private set; }
}
