using H.Controls.Form;

namespace H.Controls.Diagram.Presenters.OpenCV.Base;
[Icon(FontIcons.Photo)]
public abstract class DetectorOpenCVNodeDataBase : OpenCVNodeDataBase, IDetectorOpenCVNodeData
{
    private PreviewType _detectorPreviewType = PreviewType.Src;
    [Display(Name = "输出预览类型", GroupName = "数据", Description = "设置从原图输出匹配结果还是上一结果中输出")]
    public PreviewType DetectorPreviewType
    {
        get { return _detectorPreviewType; }
        set
        {
            _detectorPreviewType = value;
            RaisePropertyChanged();
        }
    }

    protected virtual Mat GetPrviewMat(ISrcImageNodeData srcImageNodeData, IOpenCVNodeData from, Mat result)
    {
        if (this.DetectorPreviewType == PreviewType.Previous)
            return from.Mat?.Clone();
        if (this.DetectorPreviewType == PreviewType.Result)
            return result.Clone();
        return srcImageNodeData.Mat?.Clone();
    }
}

public enum PreviewType
{
    [Display(Name = "原图")]
    Src = 0,
    [Display(Name = "前图")]
    Previous = 1,
    [Display(Name = "识别结果")]
    Result = 2
}
