// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.ShapeBox.Drawings;
using H.VisionMaster.OpenCVs.TemplateMatch.NodeDatas;
using System.Windows.Media.Imaging;

namespace H.Components.Visions.OpenCV.Presenters;

[Display(Name = "œÒÀÿ∆•≈‰ƒ£∞Â…Ë÷√")]
public class Base64TemplateMatchManagerPresenter : TemplateManagerPresenterBase, IRectCropable
{
    private readonly Base64TemplateMatchNodeData _NodeData;
    public Base64TemplateMatchManagerPresenter(Base64TemplateMatchNodeData nodeData) : base(nodeData)
    {
        _NodeData = nodeData;
        this.Base64String = nodeData.Base64String;
        this.Rect = nodeData.Rect;
    }

    private System.Windows.Rect _Rect;
    [ReadOnly(true)]
    [Display(Name = "ƒ£∞Â∑∂Œß")]
    public System.Windows.Rect Rect
    {
        get { return _Rect; }
        set
        {
            _Rect = value;
            RaisePropertyChanged();
        }
    }
    public string Base64String { get; set; }

    protected override IEnumerable<IViewState> GetViewStates()
    {
        var result = new Base64TemplateMatchRectCropableState(this);
        result.RectChanged += (s, e) =>
        {
            var nRect = e;
            this.Rect = nRect;
            this.UpdateResult();
        };
        yield return result;
    }

    protected override void UpdateResult()
    {
        if (this.Rect.IsZoreOrEmpty())
        {
            this.Base64String = null;
            return;
        }

        Mat mat = this.FilePath != null ? new Mat(this.FilePath, ImreadModes.Grayscale) : this._NodeData.ResultImage?.Mat.Clone();
        if (mat == null || mat.Empty())
            return;
        var roiRect = this.Rect.ToCVRect();

        // ∑¿÷π‘ΩΩÁ
        roiRect.X = Math.Max(0, roiRect.X);
        roiRect.Y = Math.Max(0, roiRect.Y);
        roiRect.Width = Math.Min(roiRect.Width, mat.Width - roiRect.X);
        roiRect.Height = Math.Min(roiRect.Height, mat.Height - roiRect.Y);

        if (roiRect.Width <= 5 || roiRect.Height <= 5)
        {
            this.Base64String = null;
            return;
        }

        if (this.ImageSource is BitmapSource bitmapSource)
        {
            Int32Rect int32Rect = new Int32Rect((int)this.Rect.X, (int)this.Rect.Y, (int)this.Rect.Width, (int)this.Rect.Height);
            this.Base64String = bitmapSource.ToCroppedImageBase64String(int32Rect);
            this.Shapes = this.Base64String.ToImageShape().ToEnumerable().OfType<IShape>().ToObservable();
        }
    }
}

[Icon(FontIcons.Photo)]
[Display(Name = "Ωÿ»°Õº∆¨")]
public class Base64TemplateMatchRectCropableState : RectCropableState
{
    private readonly Base64TemplateMatchManagerPresenter _nodeData;
    public Base64TemplateMatchRectCropableState(Base64TemplateMatchManagerPresenter shapeTemplateMatch) : base(shapeTemplateMatch)
    {
        this._nodeData = shapeTemplateMatch;
    }

    public override void Enter()
    {
        base.Enter();
        this.OnRectChanged(this.ROIRectShape.Rect, this.ROIRectShape.Rect);
    }
}

