using H.Extensions.ValueConverter;
using System;
using System.Globalization;


namespace H.App.FileManager
{
    public class GetFileToViewConverter : MarkupValueConverterBase
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is fm_dd_file file)
            {
                return Ioc.GetService<IFileToMoreViewService>().ToView(file);
            }
            return null;
        }
    }
}
