// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

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
