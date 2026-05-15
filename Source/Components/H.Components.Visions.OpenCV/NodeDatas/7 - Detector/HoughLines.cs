// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Components.Visions.OpenCV.NodeDatas.Detector;
//需要检测理论上的无限长直线
//进行学术研究或特定算法开发
//需要更精细控制霍夫变换参数
[Icon(FontIcons.LargeErase)]
[Display(Name = "直线查找", GroupName = "查找", Order = 1, Description = "标准直线识别，检测无限长的直线(ρ,θ参数)")]
public class HoughLines : HoughLinesBase, IDetectorGroupableNodeData
{
    private double _rho = 1.0;
    [Range(1.0, 10000.0)]
    [PropertyItem(typeof(DoubleSliderTextPropertyItem))]
    [DefaultValue(1.0)]
    [Display(Name = "距离分辨率（像素）", GroupName = VisionPropertyGroupNames.RunParameters, Description = "距离分辨率（像素），增大可减少计算量但降低精度")]
    public double Rho
    {
        get { return _rho; }
        set
        {
            _rho = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private double _theta = 180;
    [Range(1.0, 360)]
    [PropertyItem(typeof(DoubleSliderTextPropertyItem))]
    [Display(Name = "角度分辨率（角度）", GroupName = VisionPropertyGroupNames.RunParameters, Description = "减小可提高角度精度但增加计算量")]
    public double Theta
    {
        get { return _theta; }
        set
        {
            _theta = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private int _threshold = 100;
    [PropertyItem(typeof(Int32SliderTextPropertyItem))]
    [Range(100, 200)]
    [Display(Name = "投票阈值", GroupName = VisionPropertyGroupNames.RunParameters, Description = "决定被认定为直线的最小票数,值越大检测到的直线越少但更显著")]
    public int Threshold
    {
        get { return _threshold; }
        set
        {
            _threshold = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private double _srn = 0;
    [Display(Name = "细化距离分辨率", GroupName = VisionPropertyGroupNames.RunParameters, Description = "控制距离分辨率(rho)的细化程度")]
    public double Srn
    {
        get { return _srn; }
        set
        {
            _srn = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private double _stn = 0;
    [Display(Name = "细化角度分辨率", GroupName = VisionPropertyGroupNames.RunParameters, Description = "控制角度分辨率(theta)的细化程度")]
    public double Stn
    {
        get { return _stn; }
        set
        {
            _stn = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    protected override FlowableResult<IMatImage> Invoke(Mat fromImage)
    {
        var resultImage = this.GetExpressionResultImage(fromImage.ToMatImage()).ToMatImage();
        LineSegmentPolar[] segStd = Cv2.HoughLines(fromImage, Rho, Math.PI / Theta, Threshold, Srn, Stn);
        int limit = Math.Min(segStd.Length, 10);
        List<Tuple<OpenCvSharp.Point, OpenCvSharp.Point>> lines = new List<Tuple<OpenCvSharp.Point, OpenCvSharp.Point>>();
        var color = VisionSettings.Instance.OutputColor.ToScalar();
        for (int i = 0; i < limit; i++)
        {
            float rho = segStd[i].Rho;
            float theta = segStd[i].Theta;
            double a = Math.Cos(theta);
            double b = Math.Sin(theta);
            double x0 = a * rho;
            double y0 = b * rho;
            OpenCvSharp.Point pt1 = new OpenCvSharp.Point { X = (int)Math.Round(x0 + 1000 * -b), Y = (int)Math.Round(y0 + 1000 * a) };
            OpenCvSharp.Point pt2 = new OpenCvSharp.Point { X = (int)Math.Round(x0 - 1000 * -b), Y = (int)Math.Round(y0 - 1000 * a) };

            //if (roi.HasValue)
            //{
            //    pt1.X += roi.Value.X;
            //    pt1.Y += roi.Value.Y;
            //    pt2.X += roi.Value.X;
            //    pt2.Y += roi.Value.Y;
            //}
            //imgStd.Line(pt1, pt2, color, imgStd.ToThickness(), LineTypes.AntiAlias, 0);
            lines.Add(Tuple.Create(pt1, pt2));
        }
        var shapes = lines.Select(x => new DimensionShape(x.Item1.ToPoint(), x.Item2.ToPoint())).ToList();
        foreach (var item in shapes)
        {
            item.Text = item.Angle.ToString("F1") + "°";
        }
        if (lines.Count() > 0)
            this.ResultVisionLine = new VisionLine(lines[0].Item1.ToPoint(), lines[0].Item2.ToPoint());
        this.ResultShapes = shapes.OfType<IShape>().ToObservable();
        this.MatchingCountResult = lines.Count();
        Controls.Diagram.Presenter.NodeDatas.Base.IResultPresenter resultPresenter = shapes.ToResultPresenter();
        return this.OK(resultImage, resultPresenter, this.MatchingCountResult.ToDetectSuccessMessage());
    }
}
