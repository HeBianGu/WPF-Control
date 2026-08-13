// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Components.VisionDiagram.Presenters;

namespace H.Components.VisionDiagrams.OpenCV.Extensions;

public static class MatExtension
{
    public static IEnumerable<Point2f> GetForegroundPoints(this Mat fromImage)
    {
        using var nonZero = new Mat();
        Cv2.FindNonZero(fromImage, nonZero);
        if (nonZero.Empty())
            return null;
        // 收集前景点
        var points = new Point2f[nonZero.Rows];
        double sumX = 0, sumY = 0;
        for (int i = 0; i < nonZero.Rows; i++)
        {
            var p = nonZero.Get<OpenCvSharp.Point>(i);
            points[i] = new Point2f(p.X, p.Y);
            sumX += p.X;
            sumY += p.Y;
        }
        return points;
    }

    public static void DrawRectangle(this Mat image, OpenCvSharp.Rect box, Color boxColor, int boxThickness = 1)
    {
        Cv2.Rectangle(image, box, boxColor.ToScalar(), boxThickness, LineTypes.Link8);
    }

    public static void DrawLine(this Mat image, OpenCvSharp.Point from, OpenCvSharp.Point to, Color boxColor, int boxThickness = 1)
    {
        Cv2.Line(image, from, to, boxColor.ToScalar(), boxThickness, LineTypes.Link8);
    }

    public static void DrawCircle(this Mat image, OpenCvSharp.Point point, int radius, Color color, int thickness = 1)
    {
        Cv2.Circle(image, point, radius, color.ToScalar(), thickness, LineTypes.Link8);
    }

    public static Mat ToROIImage(this Mat image, OpenCvSharp.Rect roi)
    {
        OpenCvSharp.Rect rect = new OpenCvSharp.Rect(0, 0, image.Width, image.Height);
        var intersect = rect.Intersect(roi);
        return new Mat(image, intersect);
    }

    public static System.Windows.Rect ToRect(this Mat image)
    {
        return new System.Windows.Rect(0, 0, image.Width, image.Height);
    }

    public static int ToThickness(this Mat image)
    {
        if (image == null || image.IsDisposed || image.Empty())
            return 1;
        double s = Math.Sqrt(image.Height * image.Height + image.Width * image.Width);
        //int thickness = (image.Height * image.Width) / 100000;
        return (int)(s / 200 * VisionSettings.Instance.OutputThickness);
    }

    public static ImageSource ToImageSource(this Mat mat)
    {
        if (!mat.IsValid())
            return null;
        //return Application.Current?.Dispatcher.Invoke(() =>
        //{
        var r = mat.ToWriteableBitmap();
        if (r.CanFreeze)
            r.Freeze();
        return r;
        //});
        //return mat.ToBitmapSource();
    }

    public static void Validate(this Mat image)
    {
        ArgumentNullException.ThrowIfNull(image);
        if (image.IsDisposed || image.Empty())
            throw new ArgumentException("输入图像未初始化或无效", nameof(image));
        if (image.Channels() is not (1 or 3 or 4))
            throw new NotSupportedException($"不支持{image.Channels()}通道图像");
    }

    public static bool IsValid(this Mat mat)
    {
        return mat != null && !mat.IsDisposed && !mat.Empty();
    }

    public static bool IsValid(this Mat mat, OpenCvSharp.Rect roiRect)
    {
        if (!mat.IsValid())
            return false;
        var imageRect = new OpenCvSharp.Rect(0, 0, mat.Width, mat.Height);
        var intersect = imageRect.Intersect(roiRect);
        if (intersect.Width <= 0 || intersect.Height <= 0)
            return false;
        return true;
    }
    public static Mat ToRgb(this Mat image)
    {
        return image.Channels() switch
        {
            1 => image.CvtColor(ColorConversionCodes.GRAY2RGB),
            3 => image.CvtColor(ColorConversionCodes.BGR2RGB),
            4 => image.CvtColor(ColorConversionCodes.BGRA2RGB),
            _ => throw new NotSupportedException($"不支持{image.Channels()}通道图像")
        };
    }

    public static Mat ToBgr(this Mat image)
    {
        return image.Channels() switch
        {
            1 => image.CvtColor(ColorConversionCodes.GRAY2BGR),
            3 => image,
            4 => image.CvtColor(ColorConversionCodes.BGRA2BGR),
            _ => throw new NotSupportedException($"不支持{image.Channels()}通道图像")
        };
    }

    public static Mat ToGray(this Mat image)
    {
        return image.Channels() switch
        {
            1 => image,
            3 => image.CvtColor(ColorConversionCodes.BGR2GRAY),
            4 => image.CvtColor(ColorConversionCodes.BGRA2GRAY),
            _ => throw new NotSupportedException($"不支持{image.Channels()}通道图像")
        };
    }

    //public static Mat ToGrayMat(this Mat mat)
    //{
    //    if (mat.Channels() <= 1)
    //        return mat;
    //    var grayTemplate = new Mat();
    //    Cv2.CvtColor(mat, grayTemplate, ColorConversionCodes.BGR2GRAY);
    //    return grayTemplate;
    //}

    public static Mat ToByteGray(this Mat gray)
    {
        if (gray.Type() == MatType.CV_8UC1)
            return gray;

        using Mat normalized = new Mat();
        Cv2.Normalize(gray, normalized, 0, 255, NormTypes.MinMax);
        Mat result = new Mat();
        normalized.ConvertTo(result, MatType.CV_8UC1);
        return result;
    }

    public static bool IsBinaryThresholdMat(this Mat image, byte foregroundValue = 255)
    {
        ArgumentNullException.ThrowIfNull(image);
        if (image.Empty() || image.Channels() != 1)
            return false;
        if (image.Depth() != MatType.CV_8U)
            return false;
        using Mat intermediate = new Mat();
        // 检查是否存在 1 ～ foregroundValue - 1 的中间灰度。
        Cv2.InRange(
            image,
            new Scalar(1),
            new Scalar(foregroundValue - 1),
            intermediate);
        return Cv2.CountNonZero(intermediate) == 0;
    }

    public static Mat ToBinaryThresholdMat(this Mat mat, double thresh = 120, double maxval = 255, ThresholdTypes thresholdType = ThresholdTypes.Binary)
    {
        var gray = mat.ToGray();
        if (gray.IsBinaryThresholdMat())
            return gray;
        using Mat binary = new Mat();
        Cv2.Threshold(gray, binary, thresh, maxval, thresholdType);
        return binary;
    }

    public static Mat ToHSVMat(this Mat mat)
    {
        if (mat.Channels() <= 1)
            return mat;
        var grayTemplate = new Mat();
        Cv2.CvtColor(mat, grayTemplate, ColorConversionCodes.BGR2HSV);
        return grayTemplate;
    }


    public static IMatImage ToMatImage(this Mat mat)
    {
        return new MatImage(mat);
    }

    public static OpenCvSharp.Point[][] FindContours(this Mat mat, OpenCvSharp.Rect roiRect)
    {
        if (!mat.IsValid(roiRect))
            return null;

        var imageRect = new OpenCvSharp.Rect(0, 0, mat.Width, mat.Height);
        var intersect = imageRect.Intersect(roiRect);
        if (intersect.Width <= 0 || intersect.Height <= 0)
            return null;
        if (!mat.IsRoiInRange(roiRect))
            return null;

        using (Mat roiImage = new Mat(mat, roiRect))
        {
            // 在 ROI 区域内查找轮廓
            Cv2.FindContours(roiImage, out OpenCvSharp.Point[][] contours, out _, RetrievalModes.Tree, ContourApproximationModes.ApproxNone);

            List<OpenCvSharp.Point[]> filteredContours = new List<OpenCvSharp.Point[]>();
            int roiWidth = roiImage.Width;
            int roiHeight = roiImage.Height;

            foreach (var contour in contours)
            {
                // 将轮廓坐标从 ROI 内部转换到整个图像的坐标系
                for (int j = 0; j < contour.Length; j++)
                {
                    contour[j].X += roiRect.X;
                    contour[j].Y += roiRect.Y;
                }
                filteredContours.Add(contour);
            }
            return filteredContours.ToArray();
        }
    }

    // 将Mat转换为Base64字符串
    public static string ToBase64String(this Mat mat, string extension = ".jpg")
    {
        if (mat.Empty())
            return string.Empty;
        // 编码为图像格式
        Cv2.ImEncode(extension, mat, out byte[] buffer);
        // 转换为Base64
        return Convert.ToBase64String(buffer);
    }

    // 从Base64字符串恢复Mat
    public static Mat ToMat(this string base64String)
    {
        if (string.IsNullOrEmpty(base64String))
            return new Mat();
        // 解码Base64
        byte[] buffer = Convert.FromBase64String(base64String);
        // 解码为Mat
        return Cv2.ImDecode(buffer, ImreadModes.Unchanged);
    }

    public static IEnumerable<IVisionResultImage<IMatImage>> ToResultImages(this IEnumerable<ResultDetectBox> tuples, Mat image)
    {
        foreach (var item in tuples)
        {
            if (item.Box.Box.ToCVRect2f().IsValid() == false)
                continue;
            // Clamp ROI to image bounds (intersection with [0,0,image.Width,image.Height])
            var roi = item.Box.Box.ToCVRect();
            var imageRect = new OpenCvSharp.Rect(0, 0, image.Width, image.Height);
            roi = imageRect.Intersect(roi);

            if (roi.Width <= 0 || roi.Height <= 0)
                continue;

            Mat mat = new Mat(image, roi);
            item.Base64Image = mat.ToBase64String();
            yield return new VisionResultImage<IMatImage>() { Image = new MatImage(mat), Name = item.ToTitle() };
        }
    }
}

