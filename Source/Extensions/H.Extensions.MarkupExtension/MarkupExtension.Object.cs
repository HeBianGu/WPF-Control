// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;

namespace H.Extensions.MarkupExtension
{
    public class GetInstanceExtension : System.Windows.Markup.MarkupExtension
    {
        public Type Type { get; set; }
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Activator.CreateInstance(Type);
        }
    }
}
