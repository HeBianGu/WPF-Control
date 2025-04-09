namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Image;

[Display(Name = "图像源", GroupName = "数据源", Order = 0)]
public class OpenCVSrcImageNodeData : OpenCVImageNodeDataBase, ISrcFilePathableStartNodeData
{
    private string _srcFilePath;
    [Browsable(true)]
    [Display(Name = "源文件地址", GroupName = "数据")]
    [PropertyItem(typeof(OpenFileDialogPropertyItem))]
    public override string SrcFilePath
    {
        get { return _srcFilePath; }
        set
        {
            _srcFilePath = value;
            RaisePropertyChanged();
        }
    }

    protected override string GetImagePath()
    {
        return null;
    }
}