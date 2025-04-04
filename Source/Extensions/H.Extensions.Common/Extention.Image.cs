// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace H.Extensions.Common;

public static class ImageExtention
{
    public static ImageEx ToImageEx(this string filePath) => new ImageEx(filePath);
}

public class ImageEx
{
    private static List<Tuple<string, int, int, BitmapImage>> _fileCache = new List<Tuple<string, int, int, BitmapImage>>();

    public ImageEx(string fullPath)
    {
        this.FullPath = fullPath;
    }

    public void ClearCache()
    {
        _fileCache.Clear();
    }

    public string FullPath { get; }

    public Tuple<int, int> GetImagePixel()
    {
        if (!File.Exists(this.FullPath)|| this.FullPath.IsImage()==false)
            return null;
        BitmapImage bmp = new BitmapImage(new Uri(this.FullPath, UriKind.RelativeOrAbsolute));
        if (bmp == null)
            return null;
        return Tuple.Create(bmp.PixelWidth, bmp.PixelHeight);
    }

    public ImageSource GetImageSource(int decodePixelWidth = 0, int decodePixelHeight = 0, bool useCache = true)
    {
        if (!File.Exists(this.FullPath))
            return null;

        if (this.FullPath.IsImage() == false)
            return null;

        if (useCache)
        {
            Tuple<string, int, int, BitmapImage> find = _fileCache.FirstOrDefault(k => k.Item1 == this.FullPath && k.Item2 == decodePixelWidth && k.Item3 == decodePixelHeight);
            if (find != null && find.Item4 != null)
                return find.Item4;
        }

        try
        {
            using (FileStream filestream = File.Open(this.FullPath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                using (BinaryReader reader = new BinaryReader(filestream))

                //using (BinaryReader reader = new BinaryReader(File.Open(filePath, FileMode.Open,FileAccess.ReadWrite, FileShare.ReadWrite)))
                {
                    FileInfo fi = new FileInfo(this.FullPath);
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
                        _fileCache.Add(Tuple.Create(this.FullPath, decodePixelWidth, decodePixelHeight, bitmapImage));
                    return bitmapImage;
                }
            }
        }
        catch (Exception)
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

}
