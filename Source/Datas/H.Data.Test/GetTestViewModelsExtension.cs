// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Markup;

namespace H.Data.Test
{
    public class GetTestViewModelsExtension : MarkupExtension
    {
        public int Count { get; set; } = 10;

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Enumerable.Range(0, this.Count).Select(x => new TestBindable());
        }
    }
}
