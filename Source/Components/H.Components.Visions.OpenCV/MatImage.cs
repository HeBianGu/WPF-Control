// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Common.Interfaces;

namespace H.Components.Visions.OpenCV;
public interface IMatImage : IVisionImage
{
    Mat Mat { get; set; }
    IMatImage ToMatImage();
}

public class MatImage : IVisionImage, IMatImage
{
    private ImageSource _imageSource;
    public MatImage()
    {
        this.Mat = new Mat();
    }
    public MatImage(Mat image)
    {
        this.Mat = image;
        this._imageSource = image.ToImageSource();
    }

    public Mat Mat { get; set; }

    public ImageSource ImageSource
    {
        get { return _imageSource; }
        set { _imageSource = value; }
    }

    public ImageSource ToImageSource()
    {
        if (this._imageSource == null)
            this._imageSource = this.Mat?.ToImageSource();
        return this._imageSource;
    }

    public void InvalidateImageSource()
    {
        this._imageSource = null;
    }

    public bool IsValid()
    {
        return this.Mat.IsValid();
    }

    public void Dispose()
    {
        this.Mat?.Dispose();
        this.ImageSource = null;
    }

    public IVisionImage ToROIImage(System.Windows.Rect rect)
    {
        return new MatImage(this.Mat.ToROIImage(rect.ToCVRect()));
    }

    public IMatImage Clone()
    {
        return new MatImage(this.Mat.Clone());
    }

    IVisionImage ICloneable<IVisionImage>.Clone()
    {
        return this.Clone();
    }

    public IMatImage ToMatImage()
    {
        return this.Clone();
    }
}
