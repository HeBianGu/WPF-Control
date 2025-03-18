

namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Basic;
[Display(Name = "平面细分", GroupName = "基础函数", Description = "将平面区域划分为更小、更简单的子区域（如三角形、四边形等）的技术", Order = 72)]
public class Subdiv2D : BasicOpenCVNodeDataBase
{
    private Subdiv2DOutType _outType = Subdiv2DOutType.Vonoroi;
    /// <summary>
    /// 三角形（Triangles）：将平面区域划分为三角形。三角形是最简单的多边形，适合大多数计算任务。
    /// 四边形（Quads）：将平面区域划分为四边形。四边形在某些应用中（如纹理映射）可能更高效。
    /// 多边形（Polygons）：将平面区域划分为任意多边形。这种类型更灵活，但计算复杂度较高。
    /// </summary>
    [DefaultValue(Subdiv2DOutType.Vonoroi)]
    [Display(Name = "单元类型", GroupName = "数据", Description = "定义了平面细分的输出单元类型")]
    public Subdiv2DOutType OutType
    {
        get { return _outType; }
        set
        {
            _outType = value;
            RaisePropertyChanged();
        }
    }

    private int _size = 600;
    /// <summary>
    /// 绝对大小：指定每个单元的具体尺寸（如边长或面积）。
    /// 相对大小：根据输入区域的尺寸动态调整单元大小。
    /// </summary>
    [DefaultValue(600)]
    [Display(Name = "单元大小", GroupName = "数据", Description = "定义了细分后单元的目标大小")]
    public int Size
    {
        get { return _size; }
        set
        {
            _size = value;
            RaisePropertyChanged();
        }
    }

    protected override FlowableResult<Mat> Invoke(ISrcImageNodeData srcImageNodeData, IOpenCVNodeData from, IFlowableDiagramData diagram)
    {
        // Creates random point list
        Random rand = new Random();
        Point2f[] points = Enumerable.Range(0, 100).Select(_ =>
            new Point2f(rand.Next(0, Size), rand.Next(0, Size))).ToArray();

        using MatExpr imgExpr = Mat.Zeros(Size, Size, MatType.CV_8UC3);
        using Mat img = imgExpr.ToMat();
        foreach (Point2f p in points)
        {
            img.Circle((Point)p, 4, Scalar.Red, -1);
        }

        // Initializes Subdiv2D
        using OpenCvSharp.Subdiv2D subdiv = new OpenCvSharp.Subdiv2D();
        subdiv.InitDelaunay(new Rect(0, 0, Size, Size));
        subdiv.Insert(points);

        if (this.OutType == Subdiv2DOutType.Vonoroi)
        {  // Draws voronoi diagram
            subdiv.GetVoronoiFacetList(null, out Point2f[][] facetList, out Point2f[] facetCenters);
            Mat vonoroi = img.Clone();
            foreach (Point2f[] list in facetList)
            {
                Point2f before = list.Last();
                foreach (Point2f p in list)
                {
                    vonoroi.Line((Point)before, (Point)p, new Scalar(64, 255, 128), 1);
                    before = p;
                }
            }
            return this.OK(vonoroi);
        }

        {
            Vec4f[] edgeList = subdiv.GetEdgeList();
            Mat delaunay = img.Clone();
            foreach (Vec4f edge in edgeList)
            {
                Point p1 = new Point(edge.Item0, edge.Item1);
                Point p2 = new Point(edge.Item2, edge.Item3);
                delaunay.Line(p1, p2, new Scalar(64, 255, 128), 1);
            }
            return this.OK(delaunay);
        }
    }

    //public override IFlowableResult Invoke(Part previors, Node diagram)
    //{
    //    // Creates random point list
    //    var rand = new Random();
    //    var points = Enumerable.Range(0, 100).Select(_ =>
    //        new Point2f(rand.Next(0, Size), rand.Next(0, Size))).ToArray();

    //    using var imgExpr = Mat.Zeros(Size, Size, MatType.CV_8UC3);
    //    using var img = imgExpr.ToMat();
    //    foreach (var p in points)
    //    {
    //        img.Circle((Point)p, 4, Scalar.Red, -1);
    //    }

    //    // Initializes Subdiv2D
    //    using var subdiv = new OpenCvSharp.Subdiv2D();
    //    subdiv.InitDelaunay(new Rect(0, 0, Size, Size));
    //    subdiv.Insert(points);

    //    if (this.OutType == Subdiv2DOutType.Vonoroi)
    //    {  // Draws voronoi diagram
    //        subdiv.GetVoronoiFacetList(null, out var facetList, out var facetCenters);
    //        var vonoroi = img.Clone();
    //        foreach (var list in facetList)
    //        {
    //            var before = list.Last();
    //            foreach (var p in list)
    //            {
    //                vonoroi.Line((Point)before, (Point)p, new Scalar(64, 255, 128), 1);
    //                before = p;
    //            }
    //        }
    //        this.Mat = vonoroi;
    //    }

    //    if (this.OutType == Subdiv2DOutType.Delaunay)
    //    {
    //        Vec4f[] edgeList = subdiv.GetEdgeList();
    //        var delaunay = img.Clone();
    //        foreach (var edge in edgeList)
    //        {
    //            var p1 = new Point(edge.Item0, edge.Item1);
    //            var p2 = new Point(edge.Item2, edge.Item3);
    //            delaunay.Line(p1, p2, new Scalar(64, 255, 128), 1);
    //        }
    //        this.Mat = delaunay;
    //    }
    //    this.UpdateMatToView();
    //    return base.Invoke(previors, diagram);
    //}
}

public enum Subdiv2DOutType
{
    Vonoroi = 0, Delaunay
}
