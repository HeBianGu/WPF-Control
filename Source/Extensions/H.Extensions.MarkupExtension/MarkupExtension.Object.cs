﻿// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;
using System.Windows.Markup;

namespace H.Extensions.MarkupExtension
{
    public class GetInstanceExtension : System.Windows.Markup.MarkupExtension
    {
        public Type Type { get; set; }
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Activator.CreateInstance(this.Type);
        }
    }
}
