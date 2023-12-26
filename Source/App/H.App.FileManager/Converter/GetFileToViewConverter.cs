using H.Extensions.ValueConverter;
using System;
using System.Globalization;


namespace H.App.FileManager
{
    public class GetFileToViewConverter : MarkupValueConverterBase
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;
            if (value is fm_dd_video video)
            {
                return new VideoView(video);
            }
            if (value is fm_dd_image image)
            {
                return new ImageView(image);
            }
            if (value is fm_dd_file file)
            {
                return new FileView(file);
            }
            return null;
        }
    }
}
