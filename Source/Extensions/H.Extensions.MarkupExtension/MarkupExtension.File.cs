// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;
using System.Windows.Markup;

namespace H.Extensions.MarkupExtension
{
    public class SpecialFolderExtension : System.Windows.Markup.MarkupExtension
    {
        public Environment.SpecialFolder SpecialFolder { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Environment.GetFolderPath(this.SpecialFolder);
        }
    }
}
