namespace H.Controls.Diagram.Presenters.OpenCV.Base;
[Icon(FontIcons.Photo)]
public abstract class DetectorOpenCVNodeDataBase : OpenCVNodeData, IDetectorOpenCVNodeData
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
