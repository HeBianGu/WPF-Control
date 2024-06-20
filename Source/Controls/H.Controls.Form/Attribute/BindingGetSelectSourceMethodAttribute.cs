// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Controls.Form
{
    [System.AttributeUsage(System.AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class BindingGetSelectSourceMethodAttribute : System.Attribute
    {
        public BindingGetSelectSourceMethodAttribute(string methodName)
        {
            this.MethodName = methodName;
        }

        public string MethodName { get; }
    }
}
