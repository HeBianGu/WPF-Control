using System.Windows.Markup;

namespace H.Controls.Diagram.Presenters.OpenCV;

/// <summary>
/// Image file Paths
/// </summary>
public static class ImagePath
{
    public const string Lenna = "Data/Image/lenna.png";
    public const string Lenna511 = "Data/Image/lenna511.png";
    public const string Girl = "Data/Image/Girl.bmp";
    public const string Mandrill = "Data/Image/Mandrill.bmp";
    public const string Goryokaku = "Data/Image/goryokaku.jpg";
    public const string Maltese = "Data/Image/maltese.jpg";
    public const string Cake = "Data/Image/cake.bmp";
    public const string Fruits = "Data/Image/fruits.jpg";
    public const string Penguin1 = "Data/Image/penguin1.png";
    public const string Penguin1b = "Data/Image/penguin1b.png";
    public const string Penguin2 = "Data/Image/penguin2.png";
    public const string Distortion = "Data/Image/Calibration/01.jpg";
    public const string Calibration = "Data/Image/Calibration/00.jpg";
    public const string SurfBox = "Data/Image/box.png";
    public const string SurfBoxinscene = "Data/Image/box_in_scene.png";
    public const string TsukubaLeft = "Data/Image/tsukuba_left.png";
    public const string TsukubaRight = "Data/Image/tsukuba_right.png";
    public const string Square1 = "Data/Image/Squares/pic1.png";
    public const string Square2 = "Data/Image/Squares/pic2.png";
    public const string Square3 = "Data/Image/Squares/pic3.png";
    public const string Square4 = "Data/Image/Squares/pic4.png";
    public const string Square5 = "Data/Image/Squares/pic5.png";
    public const string Square6 = "Data/Image/Squares/pic6.png";
    public const string Shapes = "Data/Image/shapes.png";
    public const string Yalta = "Data/Image/yalta.jpg";
    public const string Depth16Bit = "Data/Image/16bit.png";
    public const string Hand = "Data/Image/hand_p.jpg";
    public const string Asahiyama = "Data/Image/asahiyama.jpg";
    public const string Balloon = "Data/Image/Balloon.png";
    public const string Newspaper = "Data/Image/very_old_newspaper.png";
    public const string Binarization = "Data/Image/binarization_sample.bmp";
    public const string Walkman = "Data/Image/walkman.jpg";
    public const string Cat = "Data/Image/cat.jpg";
    public const string Match1 = "Data/Image/match1.png";
    public const string Match2 = "Data/Image/match2.png";
    public const string Aruco = "Data/Image/aruco_markers_photo.jpg";

    public const string Cvmorph = "Data/Image/cvmorph.png";

    public const string Circle = "Data/Image/circle.png";
    public const string Pentagon = "Data/Image/pentagon.png";
}

public class GetOpenCVImagesExtension : MarkupExtension
{
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return OpenCVImages.GetImageSources();
    }
}

public class OpenCVImages
{
    public static IEnumerable<ImageSource> GetImageSources()
    {
        System.Reflection.FieldInfo[] ms = typeof(ImagePath).GetFields();
        TypeConverter c = TypeDescriptor.GetConverter(typeof(ImageSource));
        //string format = "pack://application:,,,/{0}";
        string format = "pack://application:,,,/H.Controls.Diagram.Presenters.OpenCV;component/{0}";
        foreach (System.Reflection.FieldInfo item in ms)
        {
            object v = item.GetValue(null);
            string s = string.Format(format, v);
            System.Diagnostics.Debug.WriteLine(s);
            yield return (ImageSource)c.ConvertFrom(s);
        }
    }
    public static IEnumerable<ImageSource> GetFileImageSources()
    {
        TypeConverter c = TypeDescriptor.GetConverter(typeof(ImageSource));
        foreach (var item in GetImageFiles())
        {
            yield return (ImageSource)c.ConvertFrom(item);
        }
    }

    public static IEnumerable<string> GetImageFiles()
    {
        System.Reflection.FieldInfo[] ms = typeof(ImagePath).GetFields();
        foreach (System.Reflection.FieldInfo item in ms)
        {
            object v = item.GetValue(null);
            yield return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, v.ToString());
        }
    }

    public static IEnumerable<string> GetResourceImageFiles()
    {
        System.Reflection.FieldInfo[] ms = typeof(ImagePath).GetFields();
        foreach (System.Reflection.FieldInfo item in ms)
        {
            object v = item.GetValue(null);
            yield return GetResourceFilePath(v.ToString());
        }
    }

    public static string GetResourceFilePath(string dataPath)
    {
         return $"pack://application:,,,/H.Controls.Diagram.Presenters.OpenCV;component/{dataPath}";
    }
}
