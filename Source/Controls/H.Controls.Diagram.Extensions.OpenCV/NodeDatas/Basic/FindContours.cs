namespace H.Controls.Diagram.Extensions.OpenCV.NodeDatas.Basic;
[Display(Name = "查找轮廓", GroupName = "基础函数", Description = "二指图片的效果反转既黑色变白色，白色变黑色", Order = 0)]
public class FindContours : BasicActionNodeDataBase
{
    private RetrievalModes _retrievalMode = RetrievalModes.Tree;
    [DefaultValue(RetrievalModes.Tree)]
    [Display(Name = "RetrievalMode", GroupName = "数据")]
    public RetrievalModes RetrievalMode
    {
        get { return _retrievalMode; }
        set
        {
            _retrievalMode = value;
            DispatcherRaisePropertyChanged();
        }
    }


    private ContourApproximationModes _contourApproximationModes = ContourApproximationModes.ApproxNone;
    [DefaultValue(ContourApproximationModes.ApproxNone)]
    [Display(Name = "ContourApproximationMode", GroupName = "数据")]
    public ContourApproximationModes ContourApproximationMode
    {
        get { return _contourApproximationModes; }
        set
        {
            _contourApproximationModes = value;
            DispatcherRaisePropertyChanged();
        }
    }



    private Point? _offset = null;
    [DefaultValue(null)]
    [Display(Name = "Offset", GroupName = "数据")]
    public Point? Offset
    {
        get { return _offset; }
        set
        {
            _offset = value;
            DispatcherRaisePropertyChanged();
        }
    }


    private int _contourIdx = -1;
    [DefaultValue(-1)]
    [Display(Name = "ContourIdx", GroupName = "数据")]
    public int ContourIdx
    {
        get { return _contourIdx; }
        set
        {
            _contourIdx = value;
            DispatcherRaisePropertyChanged();
        }
    }


    private int _thickness = 1;
    [DefaultValue(1)]
    [Display(Name = "Thickness", GroupName = "数据")]
    public int Thickness
    {
        get { return _thickness; }
        set
        {
            _thickness = value;
            DispatcherRaisePropertyChanged();
        }
    }


    private LineTypes _lineType = LineTypes.Link8;
    [DefaultValue(LineTypes.Link8)]
    [Display(Name = "LineType", GroupName = "数据")]
    public LineTypes LineType
    {
        get { return _lineType; }
        set
        {
            _lineType = value;
            DispatcherRaisePropertyChanged();
        }
    }


    private int _maxLevel = int.MaxValue;
    [DefaultValue(int.MaxValue)]
    [Display(Name = "MaxLevel", GroupName = "数据")]
    public int MaxLevel
    {
        get { return _maxLevel; }
        set
        {
            _maxLevel = value;
            DispatcherRaisePropertyChanged();
        }
    }

    private Point? _drawOffset;
    [DefaultValue(null)]
    [Display(Name = "DrawOffset", GroupName = "数据")]
    public Point? DrawOffset
    {
        get { return _drawOffset; }
        set
        {
            _drawOffset = value;
            DispatcherRaisePropertyChanged();
        }
    }

    //public override IFlowableResult Invoke(Part previors, Node current)
    //{
    //    Point[][] contours;
    //    HierarchyIndex[] hierarchly;
    //    Cv2.FindContours(this._srcMat, out contours, out hierarchly, this.RetrievalMode, this.ContourApproximationMode, this.Offset);
    //    var dst = new Mat(this._srcFiltPathPath, ImreadModes.Color);
    //    Cv2.DrawContours(dst, contours, this.ContourIdx, Scalar.Red, this.Thickness, this.LineType, hierarchly, this.MaxLevel, this.DrawOffset);
    //    this.Mat = dst;
    //    this.RefreshMatToView();
    //    return base.Invoke(previors, current);
    //}


    private DrawContourType _drawContourType = DrawContourType.DrawContours;
    [DefaultValue(DrawContourType.DrawContours)]
    [Display(Name = "DrawContourType", GroupName = "数据")]
    public DrawContourType DrawContourType
    {
        get { return _drawContourType; }
        set
        {
            _drawContourType = value;
            DispatcherRaisePropertyChanged();
        }
    }


    protected override IFlowableResult Refresh()
    {
        Point[][] contours;
        HierarchyIndex[] hierarchly;
        Cv2.FindContours(this._preMat, out contours, out hierarchly, this.RetrievalMode, this.ContourApproximationMode, this.Offset);
        //var dst = new Mat(this._srcFilePath, ImreadModes.Color);
        var dst = this.SrcMat.Clone();
        if (this.DrawContourType == DrawContourType.DrawContours)
            Cv2.DrawContours(dst, contours, this.ContourIdx, Scalar.Orange, this.Thickness, this.LineType, hierarchly, this.MaxLevel, this.DrawOffset);
        if (this.DrawContourType == DrawContourType.ConvexHull)
        {
            var rects = contours.Select(x => Cv2.BoundingRect(x));
            foreach (var contour in contours)
            {
                //Cv2.ConvexHull(contours[0], contour);
            }
        }
        if (this.DrawContourType == DrawContourType.BoundingRect)
        {
            var rects = contours.Select(x => Cv2.BoundingRect(x));
            foreach (var rect in rects)
            {
                Cv2.Rectangle(dst, rect.TopLeft, rect.BottomRight, Scalar.Red, this.Thickness, this.LineType);
            }
        }
        if (this.DrawContourType == DrawContourType.MinAreaRect)
        {
            var rects = contours.Select(x => Cv2.MinAreaRect(x));
            foreach (var rect in rects)
            {
                Cv2.Rectangle(dst, rect.BoundingRect().TopLeft, rect.BoundingRect().BottomRight, Scalar.Red, this.Thickness, this.LineType);
            }
        }
        if (this.DrawContourType == DrawContourType.ApproxPolyDP)
        {
            var points = contours.Select(x => Cv2.ApproxPolyDP(x, 20, true));
            Cv2.DrawContours(dst, points, this.ContourIdx, Scalar.Orange, this.Thickness, this.LineType, hierarchly, this.MaxLevel, this.DrawOffset);
        }

        this.Mat = dst;
        this.RefreshMatToView();
        return base.Refresh();
    }

}

public enum DrawContourType
{
    [Display(Name = "轮廓")]
    DrawContours = 0,
    [Display(Name = "外接矩形")]
    BoundingRect,
    [Display(Name = "最小外接矩形")]
    MinAreaRect,
    [Display(Name = "凸包")]
    ConvexHull,
    [Display(Name = "逼近")]
    ApproxPolyDP
}
