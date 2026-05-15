// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Components.Vision.NodeGroups.TemplateMatchings;
using H.Components.Vision.Presenters;
using H.Components.Visions.OpenCV.Presenters;
using H.Controls.ShapeBox.Drawings;
using H.Extensions.Mvvm.Commands;

namespace H.VisionMaster.OpenCVs.TemplateMatch.NodeDatas;

[Display(Name = "像素匹配", GroupName = "模板匹配", Description = "模板匹配对于简单的对象定位非常有效，但对于旋转、缩放或透视变换的对象可能效果不佳", Order = 0)]
public class Base64TemplateMatchNodeData : MatchingNodeData<IMatImage>, ITemplateMatchingGroupableNodeData, IBase64MatchingNodeData, IOpenCVNodeData
{
    private TemplateMatchModes _templateMatchModes = TemplateMatchModes.CCoeffNormed;
    [Display(Name = "匹配类型", GroupName = VisionPropertyGroupNames.RunParameters)]
    public TemplateMatchModes TemplateMatchModes
    {
        get { return _templateMatchModes; }
        set
        {
            _templateMatchModes = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }
    private string _base64String;
    [PropertyItem(typeof(Base64ShapeViewPropertyItem))]
    [Display(Name = "模板图片", GroupName = VisionPropertyGroupNames.RunParameters)]
    public string Base64String
    {
        get { return _base64String; }
        set
        {
            _base64String = value;
            RaisePropertyChanged();
        }
    }

    protected override FlowableResult<IMatImage> Invoke(IMatImage fromImage)
    {
        if (string.IsNullOrEmpty(this.Base64String))
            return this.Error(fromImage, "未设置模板图片");

        byte[] bytes = Convert.FromBase64String(this.Base64String);
        Mat src = fromImage.Mat;

        ImreadModes imreadModes = src.Channels() switch
        {
            1 => ImreadModes.Grayscale,
            3 => ImreadModes.Color,
            4 => ImreadModes.Unchanged,
            _ => ImreadModes.Color
        };

        using (Mat template = Cv2.ImDecode(bytes, imreadModes))
        {
            if (template.Empty())
                return this.Error(fromImage, "模板图片解码失败");

            if (src.Cols < template.Cols || src.Rows < template.Rows)
                return this.Error(fromImage, "模板尺寸大于源图，无法匹配");

            using Mat result = new Mat();

            int resultCols = src.Cols - template.Cols + 1;
            int resultRows = src.Rows - template.Rows + 1;
            result.Create(resultRows, resultCols, MatType.CV_32FC1);

            Cv2.MatchTemplate(src, template, result, this.TemplateMatchModes);

            Cv2.MinMaxLoc(result, out _, out double maxVal, out _, out OpenCvSharp.Point maxLoc);

            double threshold = 0.8;
            Mat view = fromImage.Mat.Clone();

            if (maxVal >= threshold)
            {
                OpenCvSharp.Rect rect2F = new OpenCvSharp.Rect(maxLoc.X, maxLoc.Y, template.Cols, template.Rows);

                this.MatchingCountResult = 1;
                this.Confidence = maxVal;

                var shapes = new RectShape(rect2F.ToWindowRect()) { Title = $"置信度：{Math.Round(maxVal, 2)}" }.ToEnumerable();

                if (this.DetectDisplayMode == DetectDisplayMode.Dimension)
                {
                    var dimensionShapes = rect2F.ToWindowRect().ToDimensionShapes(x => x.Text = this.GetWorldDistance(x.Length));
                    this.ResultShapes = dimensionShapes.OfType<IShape>().ToObservable();
                }
                else if (this.DetectDisplayMode == DetectDisplayMode.Default)
                {
                    this.ResultShapes = shapes.OfType<IShape>().ToObservable();
                }
                else
                {
                    Cv2.Rectangle(view, maxLoc, new OpenCvSharp.Point(maxLoc.X + template.Cols, maxLoc.Y + template.Rows), VisionSettings.Instance.OutputColor.ToScalar(), view.ToThickness());
                }

                var resultPresenter = shapes.ToResultPresenter();
                return this.OK(new MatImage(view), resultPresenter, this.MatchingCountResult.ToDetectSuccessMessage());
            }

            this.MatchingCountResult = 0;
            this.Confidence = 0.0;
            return this.OK(new MatImage(view), "没有匹配到模板");
        }
    }

}

public class Base64ShapeViewPropertyItem : ShapeViewPropertyItem
{
    public Base64ShapeViewPropertyItem(PropertyInfo property, object obj) : base(property, obj)
    {

    }

    protected override IEnumerable<IShape> GetShapes(object value)
    {
        if (value is string base64)
            yield return base64.ToImageShape();
    }

    [Icon(FontIcons.Setting)]
    [Display(Name = "模板设置")]
    public DisplayCommand ShowTemplateManagerCommand => new DisplayCommand(async x =>
    {
        if (this.Obj is Base64TemplateMatchNodeData nodeData)
        {
            var p = new Base64TemplateMatchManagerPresenter(nodeData);
            var r = await IocMessage.ShowDialog(p, x =>
            {
                x.HorizontalContentAlignment = HorizontalAlignment.Center;
                x.VerticalContentAlignment = VerticalAlignment.Center;
                x.VerticalAlignment = VerticalAlignment.Center;
                x.Margin = new Thickness(50);
                x.Title = "模板设置";
            });
            if (r != true)
                return;
            nodeData.Base64String = p.Base64String;
            nodeData.Rect = p.Rect;
        }
    });

    [Icon(FontIcons.Delete)]
    [Display(Name = "删除模板")]
    public DisplayCommand DeleteTemplateCommand => new DisplayCommand(x =>
    {
        if (this.Obj is Base64TemplateMatchNodeData nodeData)
        {
            nodeData.Base64String = null;
            nodeData.Rect = System.Windows.Rect.Empty;
        }
    });
}

