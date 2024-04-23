// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control



using H.Extensions.ValueConverter;
using H.Providers.Ioc;
using System;
using System.Globalization;

namespace H.Modules.Project
{
    public class GetIsCurrentProjectConverter : MarkupMultiValueConverterBase
    {
        public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return IocProject.Instance.Current == values[0];
        }
    }
}
