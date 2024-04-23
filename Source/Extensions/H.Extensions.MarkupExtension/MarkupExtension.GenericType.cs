// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;

namespace H.Extensions.MarkupExtension
{
    public class GenericTypeExtension : System.Windows.Markup.MarkupExtension
    {
        public Type GenericType { get; set; }

        public Type TypeArgument { get; set; }

        public Type[] TypeArguments { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (TypeArgument == null)
                return this.GenericType.MakeGenericType(this.TypeArguments);
            return this.GenericType.MakeGenericType(this.TypeArgument);
        }
    }
}
