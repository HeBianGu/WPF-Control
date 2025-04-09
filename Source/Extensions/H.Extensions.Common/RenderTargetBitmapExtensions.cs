// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Windows.Media.Imaging;

namespace H.Extensions.Common;

public static class RenderTargetBitmapExtensions
{
    public static void SaveAsPng(this RenderTargetBitmap renderTargetBitmap, string filePath)
    {
        PngBitmapEncoder encoder = new PngBitmapEncoder();
        encoder.Frames.Add(BitmapFrame.Create(renderTargetBitmap));
        using (var stream = new System.IO.FileStream(filePath, System.IO.FileMode.Create))
        {
            encoder.Save(stream);
        }
    }

    public static void SaveAsJpeg(this RenderTargetBitmap renderTargetBitmap, string filePath)
    {
        JpegBitmapEncoder encoder = new JpegBitmapEncoder();
        encoder.Frames.Add(BitmapFrame.Create(renderTargetBitmap));
        using (var stream = new System.IO.FileStream(filePath, System.IO.FileMode.Create))
        {
            encoder.Save(stream);
        }
    }

    public static void SaveAsBmp(this RenderTargetBitmap renderTargetBitmap, string filePath)
    {
        BmpBitmapEncoder encoder = new BmpBitmapEncoder();
        encoder.Frames.Add(BitmapFrame.Create(renderTargetBitmap));
        using (var stream = new System.IO.FileStream(filePath, System.IO.FileMode.Create))
        {
            encoder.Save(stream);
        }
    }
}
