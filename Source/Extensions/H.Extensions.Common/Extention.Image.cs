// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using H.Extensions.Common;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace System.IO
{
    public static class ImageExtention
    {
        private static List<Tuple<string, int, int, BitmapImage>> _fileCache = new List<Tuple<string, int, int, BitmapImage>>();
        public static ImageSource GetImageSource(this string filePath, int decodePixelWidth = 0, int decodePixelHeight = 0, bool useCache = true)
        {
            if (!File.Exists(filePath))
                return null;

            if (filePath.IsImage() == false)
                return null;

            if (useCache)
            {
                Tuple<string, int, int, BitmapImage> find = _fileCache.FirstOrDefault(k => k.Item1 == filePath && k.Item2 == decodePixelWidth && k.Item3 == decodePixelHeight);
                if (find != null && find.Item4 != null)
                    return find.Item4;
            }

            try
            {
                using (FileStream filestream = File.Open(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    using (BinaryReader reader = new BinaryReader(filestream))

                    //using (BinaryReader reader = new BinaryReader(File.Open(filePath, FileMode.Open,FileAccess.ReadWrite, FileShare.ReadWrite)))
                    {
                        FileInfo fi = new FileInfo(filePath);
                        byte[] bytes = reader.ReadBytes((int)fi.Length);
                        reader.Close();
                        BitmapImage bitmapImage = new BitmapImage();
                        bitmapImage.BeginInit();
                        if (decodePixelWidth > 0)
                            bitmapImage.DecodePixelWidth = decodePixelWidth;
                        if (decodePixelHeight > 0)
                            bitmapImage.DecodePixelHeight = decodePixelHeight;
                        bitmapImage.StreamSource = new MemoryStream(bytes);
                        bitmapImage.EndInit();
                        bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                        if (useCache)
                            _fileCache.Add(Tuple.Create(filePath, decodePixelWidth, decodePixelHeight, bitmapImage));
                        return bitmapImage;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
                //var result = new BitmapImage(new Uri(filePath, UriKind.Absolute));

                //if (decodePixelWidth > 0)
                //    result.DecodePixelWidth = decodePixelWidth;
                //if (decodePixelHeight > 0)
                //    result.DecodePixelHeight = decodePixelHeight;
                //return new BitmapImage(new Uri(filePath, UriKind.Absolute));
            }
        }

        public static Tuple<int, int> GetImagePixel(this string filePath)
        {
            if (!File.Exists(filePath))
                return null;
            BitmapImage bmp = new BitmapImage(new Uri(filePath, UriKind.Absolute));
            if (bmp == null)
                return null;
            return Tuple.Create(bmp.PixelWidth, bmp.PixelHeight);
        }
    }
}
