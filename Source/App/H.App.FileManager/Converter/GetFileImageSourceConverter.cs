using H.Extensions.Common;
using H.Extensions.ValueConverter;
using H.Extensions.ValueConverter.Images;
using System;
using System.Globalization;
using System.IO;


namespace H.App.FileManager
{
    public class GetFileImageSourceConverter : MarkupValueConverterBase
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;
            if (value is fm_dd_video video && video.Images.Count > video.SelectedImageIndex)
            {
                if (video.Images.Count > 0 && video.SelectedImageIndex < 0)
                    return video.Images[0].Url.ToImageEx().GetImageSource();
                fm_dd_video_image vimage = video.Images[video.SelectedImageIndex];
                return vimage.Url.ToImageEx().GetImageSource();
            }
            if (value is fm_dd_image image)
            {
                return image.Url.ToImageEx().GetImageSource();
            }

            if (value is fm_dd_file file)
            {
                var icon = IconHelper.GetSystemInfoIcon(file.Url);
                return IconHelper.GetIconToImageSource(icon);
            }
            return null;
        }
    }
}
