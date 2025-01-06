// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;

namespace H.Controls.Form
{
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public class PropertyItemAttribute : Attribute
    {
        public PropertyItemAttribute(Type type)
        {
            this.Type = type;
        }
        public Type Type { get; private set; }
    }
}
