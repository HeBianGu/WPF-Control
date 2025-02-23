// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase




using OpenCvSharp;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace HeBianGu.Diagram.OpenCV
{
    
    [Display(Name = "平面细分", GroupName = "基础函数", Description = "降噪成黑白色", Order = 0)]
    public class Subdiv2D : OpenCVNodeData
    {
        private Subdiv2DOutType _outType = Subdiv2DOutType.Vonoroi;
        [DefaultValue(Subdiv2DOutType.Vonoroi)]
        [Display(Name = "OutType", GroupName = "数据")]
        public Subdiv2DOutType OutType
        {
            get { return _outType; }
            set
            {
                _outType = value;
                DispatcherRaisePropertyChanged();
            }
        }

        private int _size = 600;
        [DefaultValue(600)]
        [Display(Name = "Size", GroupName = "数据")]
        public int Size
        {
            get { return _size; }
            set
            {
                _size = value;
                DispatcherRaisePropertyChanged();
            }
        }

        protected override IFlowableResult Refresh()
        {
            // Creates random point list
            var rand = new Random();
            var points = Enumerable.Range(0, 100).Select(_ =>
                new Point2f(rand.Next(0, Size), rand.Next(0, Size))).ToArray();

            using var imgExpr = Mat.Zeros(Size, Size, MatType.CV_8UC3);
            using var img = imgExpr.ToMat();
            foreach (var p in points)
            {
                img.Circle((Point)p, 4, Scalar.Red, -1);
            }

            // Initializes Subdiv2D
            using var subdiv = new OpenCvSharp.Subdiv2D();
            subdiv.InitDelaunay(new Rect(0, 0, Size, Size));
            subdiv.Insert(points);

            if (this.OutType == Subdiv2DOutType.Vonoroi)
            {  // Draws voronoi diagram
                subdiv.GetVoronoiFacetList(null, out var facetList, out var facetCenters);
                var vonoroi = img.Clone();
                foreach (var list in facetList)
                {
                    var before = list.Last();
                    foreach (var p in list)
                    {
                        vonoroi.Line((Point)before, (Point)p, new Scalar(64, 255, 128), 1);
                        before = p;
                    }
                }
                this.Mat = vonoroi;
            }

            if (this.OutType == Subdiv2DOutType.Delaunay)
            {
                Vec4f[] edgeList = subdiv.GetEdgeList();
                var delaunay = img.Clone();
                foreach (var edge in edgeList)
                {
                    var p1 = new Point(edge.Item0, edge.Item1);
                    var p2 = new Point(edge.Item2, edge.Item3);
                    delaunay.Line(p1, p2, new Scalar(64, 255, 128), 1);
                }
                this.Mat = delaunay;
            }
            this.RefreshMatToView();
           return base.Refresh();
        }

        //public override IFlowableResult Invoke(Part previors, Node current)
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
        //    this.RefreshMatToView();
        //    return base.Invoke(previors, current);
        //}
    }

    public enum Subdiv2DOutType
    {
        Vonoroi = 0, Delaunay
    }
}
