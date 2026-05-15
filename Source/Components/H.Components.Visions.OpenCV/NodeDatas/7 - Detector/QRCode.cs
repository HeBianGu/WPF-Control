// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using H.Components.Visions.OpenCV;
using H.Controls.ShapeBox.Drawings;

namespace H.VisionMaster.OpenCV.NodeDatas.Detector;

[Icon(FontIcons.QRCode)]
[Display(Name = "二维码识别", GroupName = "查找", Order = 3)]
public class QRCode : OpenCVDetectorNodeDataBase, IDetectorGroupableNodeData
{
    private string _qrCodeResult;
    [ReadOnly(true)]
    [Expressionable]
    [Display(Name = "二维码结果", GroupName = VisionPropertyGroupNames.ResultParameters, Description = "结果参数，此结果可应用再条件分支等作为判断参数")]
    public string QrCodeResult
    {
        get { return _qrCodeResult; }
        set
        {
            _qrCodeResult = value;
            RaisePropertyChanged();
        }
    }

    private DetectDisplayMode _DetectDisplayMode = DetectDisplayMode.Dimension;
    [DefaultValue(DetectDisplayMode.Dimension)]
    [Display(Name = "绘制结果方式", GroupName = VisionPropertyGroupNames.DisplayParameters)]
    public DetectDisplayMode DetectDisplayMode
    {
        get { return _DetectDisplayMode; }
        set
        {
            _DetectDisplayMode = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    protected override FlowableResult<IMatImage> Invoke(Mat fromImage)
    {
        var resultImage = this.GetExpressionResultImage(fromImage.ToMatImage()).ToMatImage();
        // 2. 创建二维码检测器
        QRCodeDetector qrDecoder = new QRCodeDetector();
        // 3. 检测二维码
        Point2f[] points;
        using (Mat straightQrCode = new Mat())
        {
            bool detected = qrDecoder.Detect(fromImage, out points);
            if (detected && points != null)
            {
                //    bool detected = qrDecoder.DetectAndDecode(image, out points, out data);
                // 4. 解码二维码
                string data = qrDecoder.Decode(fromImage, points, straightQrCode);
                this.QrCodeResult = data;
                this.MatchingCountResult = 1;
                if (!string.IsNullOrEmpty(data))
                {
                    if (this.CaliperShape is RectShape caliper)
                    {
                        if (!points.Any(x => caliper.Rect.Contains(x.ToPoint().ToPoint())))
                        {
                            return this.OK(resultImage, "在卡尺区域内未检测到二维码");
                        }
                    }
                    if (points.Length < 2)
                    {
                        return this.OK(resultImage, "解码结果: " + data);
                    }
                    PolygonShape polygonShape = new PolygonShape(points.Select(x => x.ToPoint().ToPoint())) { Title = "解码结果: " + data };
                    if (this.DetectDisplayMode == DetectDisplayMode.Dimension)
                    {
                        var shapes = points.Select(x => x.ToPoint().ToPoint()).ToDimensionShapes(x => x.Text = this.GetWorldDistance(x.Length));
                        this.ResultShapes = shapes.OfType<IShape>().ToObservable();
                    }
                    else if (this.DetectDisplayMode == DetectDisplayMode.Default)
                    {
                        this.ResultShapes = polygonShape.ToEnumerable().OfType<IShape>().ToObservable();
                    }
                    else
                    {
                        for (int i = 0; i < points.Length; i++)
                        {
                            OpenCvSharp.Point f = (OpenCvSharp.Point)points[i];
                            OpenCvSharp.Point t = (OpenCvSharp.Point)points[(i + 1) % points.Length];
                            Cv2.Line(resultImage.Mat, f, t,
                                    VisionSettings.Instance.OutputColor.ToScalar(), resultImage.Mat.ToThickness());
                        }
                    }
                    this.ResultImages = polygonShape.BoundingBox.ToCVRect().ToEnumerable().ToResultImages(fromImage).ToList();
                    this.FirstResultImage = this.ResultImages.FirstOrDefault()?.Image;
                    Controls.Diagram.Presenter.NodeDatas.Base.IResultPresenter resultPresenter = polygonShape.ToResultPresenter();
                    return this.OK(resultImage, resultPresenter, "解码结果: " + data);
                }
                else
                {
                    return this.OK(resultImage, "检测到二维码但解码失败");
                }
            }
            else
            {
                this.MatchingCountResult = 0;
                return this.OK(resultImage, "未检测到二维码");
            }
        }
    }
}

