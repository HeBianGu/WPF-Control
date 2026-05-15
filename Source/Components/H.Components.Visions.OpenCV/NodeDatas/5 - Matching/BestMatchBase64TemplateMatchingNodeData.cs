// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

//using H.Controls.Diagram.Presenter.Flowables;
//using H.Controls.Form.Attributes;
//using H.Extensions.Common;
//using H.VisionMaster.NodeData;
//using H.VisionMaster.NodeData.Base;

//// Copyright (c) HeBianGu Authors. All Rights Reserved. 
//// Author: HeBianGu 
//// Github: https://github.com/HeBianGu/WPF-Control 
//// Document: https://hebiangu.github.io/WPF-Control-Docs  
//// QQ:908293466 Group:971261058 
//// bilibili: https://space.bilibili.com/370266611 
//// Licensed under the MIT License (the "License")

//using H.VisionMaster.NodeGroup.Groups.TemplateMatchings;
//using H.VisionMaster.OpenCV;
//using H.VisionMaster.ShapeBox.Drawings;
//using H.VisionMaster.ShapeBox.Shapes.Base;
//using OpenCvSharp;
//using System;

//namespace H.VisionMaster.OpenCVs.TemplateMatch.NodeDatas;

//[Display(Name = "图片匹配", GroupName = "特征匹配", Description = "BFMatcher（Brute-Force Matcher，暴力匹配器） 是OpenCV中用于特征匹配的常用工具，主要用于在两幅图像中找到相同的特征点（关键点）", Order = 30)]
//public class BestMatchBase64TemplateMatch : MatchingNodeData<IMatImage>, ITemplateMatchingGroupableNodeData
//{
//    private string _base64String;
//    [PropertyItem(typeof(Base64ShapeViewPropertyItem))]
//    [Display(Name = "模板图片", GroupName = VisionPropertyGroupNames.RunParameters)]
//    public string Base64String
//    {
//        get { return _base64String; }
//        set
//        {
//            _base64String = value;
//            RaisePropertyChanged();
//        }
//    }

//    protected virtual Feature2D CreateFeatureDetector()
//    {
//        //return AKAZE.Create();
//        //return BRISK.Create();
//        return ORB.Create(1000);
//        //return BRISK.Create();
//        //return FREAK.Create();
//        //return KAZE.Create();
//        //return MSER.Create();
//        //return StarDetector.Create();

//        //return SIFT.Create();
//    }

//    protected override FlowableResult<IMatImage> Invoke(IMatImage fromImage)
//    {
//        if (string.IsNullOrEmpty(this.Base64String))
//            return this.Error(fromImage, "运行完成，未绘制模板图片");
//        byte[] bytes = Convert.FromBase64String(this.Base64String);

//        Mat img2 = fromImage.Mat;
//        using Mat img1 = Cv2.ImDecode(bytes, ImreadModes.AnyColor);
//        using var orb = this.CreateFeatureDetector();
//        using Mat descriptors1 = new Mat();
//        using Mat descriptors2 = new Mat();
//        orb.DetectAndCompute(img1, null, out KeyPoint[] keyPoints1, descriptors1);
//        orb.DetectAndCompute(img2, null, out KeyPoint[] keyPoints2, descriptors2);

//        using BFMatcher bf = new BFMatcher(NormTypes.Hamming, crossCheck: true);
//        DMatch[] matches = bf.Match(descriptors1, descriptors2);

//        DMatch[] goodMatches = matches
//            .OrderBy(x => x.Distance)
//            .Take(1000)
//            .ToArray();

//        IEnumerable<Point2d> srcPts = goodMatches.Select(m => keyPoints1[m.QueryIdx].Pt).Select(p => new Point2d(p.X, p.Y));
//        IEnumerable<Point2d> dstPts = goodMatches.Select(m => keyPoints2[m.TrainIdx].Pt).Select(p => new Point2d(p.X, p.Y));

//        using Mat homography = Cv2.FindHomography(srcPts, dstPts, HomographyMethods.Ransac, 5, null);

//        int h = img1.Height, w = img1.Width;
//        Point2d[] img2Bounds = new[]
//        {
//            new Point2d(0, 0),
//            new Point2d(0, h-1),
//            new Point2d(w-1, h-1),
//            new Point2d(w-1, 0),
//        };
//        Point2d[] img2BoundsTransformed = Cv2.PerspectiveTransform(img2Bounds, homography);
//        Mat view = img2.Clone();
//        OpenCvSharp.Point[] drawingPoints = img2BoundsTransformed.Select(p => (OpenCvSharp.Point)p).ToArray();
//        //this.ResultShapes = drawingPoints.ToPolygonShape().ToShapeObservable();
//        var boundShape = drawingPoints.ToPolygonShape();
//        this.ResultShapes = dstPts.Select(x => x.ToPoint().ToPoint()).ToPointShapes().OfType<IShape>().ToObservable();
//        this.ResultShapes.Add(boundShape);
//        return this.OK(new MatImage(view));
//    }
//}

