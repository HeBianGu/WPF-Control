using OpenCvSharp.DnnSuperres;

namespace H.Controls.Diagram.Extensions.OpenCV.NodeDatas.Basic;
[Display(Name = "超分辨率处理", GroupName = "基础函数", Order = 0)]
public class DnnSuperres : BasicActionNodeDataBase
{
    private string _algo = "fsrcnn";
    [DefaultValue("fsrcnn")]
    [Display(Name = "算法", GroupName = "数据")]
    public string Algo
    {
        get { return _algo; }
        set
        {
            _algo = value;
            DispatcherRaisePropertyChanged();
        }
    }

    private int _scale = 4;
    [DefaultValue(4)]
    [Display(Name = "缩放系数", GroupName = "数据")]
    public int Scale
    {
        get { return _scale; }
        set
        {
            _scale = value;
            DispatcherRaisePropertyChanged();
        }
    }

    private string _modelFileName = "Data/Model/FSRCNN_x4.pb";
    [DefaultValue("Data/Model/FSRCNN_x4.pb")]
    [ReadOnly(true)]
    [Display(Name = "模型文件", GroupName = "数据")]
    public string ModelFileName
    {
        get { return _modelFileName; }
        set
        {
            _modelFileName = value;
            DispatcherRaisePropertyChanged();
        }
    }

    //public override IFlowableResult Invoke(Part previors, Node current)
    //{
    //    var src = this.GetFromMat(current);
    //    var dnn = new DnnSuperResImpl("fsrcnn", 4);
    //    string path = GetDataPath(this.ModelFileName);
    //    dnn.ReadModel(path);
    //    //using var src = new Mat(ImagePath.Mandrill, ImreadModes.Color);
    //    var dst = new Mat();
    //    dnn.Upsample(src, dst);
    //    this.RefreshMatToView(dst);
    //    return base.Invoke(previors, current);
    //}

    protected override IFlowableResult Refresh()
    {
        var src = this._preMat;
        var dnn = new DnnSuperResImpl("fsrcnn", 4);
        string path = GetDataPath(this.ModelFileName);
        dnn.ReadModel(path);
        //using var src = new Mat(ImagePath.Mandrill, ImreadModes.Color);
        var dst = new Mat();
        dnn.Upsample(src, dst);
        this.RefreshMatToView(dst);
        return base.Refresh();
    }
}
