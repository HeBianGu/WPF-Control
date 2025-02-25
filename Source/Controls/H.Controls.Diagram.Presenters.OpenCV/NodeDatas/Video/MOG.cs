namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Video;
[Display(Name = "前景提取", GroupName = "视频操作", Order = 0)]
public class MOG : OpenCVNodeData, IVideoFlowable
{
    private BackgroundSubtractorMOG _mog = null;

    public void Begin()
    {
        if (_mog?.IsDisposed == false)
            _mog.Dispose();
        _mog = BackgroundSubtractorMOG.Create(this.History, this.nMixtures, this.BackgroundRatio, this.NoiseSigma);
    }

    public void End()
    {
        _mog.Dispose();
    }



    private int _history = 200;
    [Display(Name = "History", GroupName = "数据")]
    public int History
    {
        get { return _history; }
        set
        {
            _history = value;
            RaisePropertyChanged();
        }
    }


    private int _nMixtures = 5;
    [Display(Name = "nMixtures", GroupName = "数据")]
    public int nMixtures
    {
        get { return _nMixtures; }
        set
        {
            _nMixtures = value;
            RaisePropertyChanged();
        }
    }


    private double _backgroundRatio = 0.7;
    [Display(Name = "BackgroundRatio", GroupName = "数据")]
    public double BackgroundRatio
    {
        get { return _backgroundRatio; }
        set
        {
            _backgroundRatio = value;
            RaisePropertyChanged();
        }
    }


    private double _noiseSigma = 0;
    [Display(Name = "NoiseSigma", GroupName = "数据")]
    public double NoiseSigma
    {
        get { return _noiseSigma; }
        set
        {
            _noiseSigma = value;
            RaisePropertyChanged();
        }
    }

    private double _learningRate = 0.01;
    [Display(Name = "LearningRate", GroupName = "数据")]
    public double LearningRate
    {
        get { return _learningRate; }
        set
        {
            _learningRate = value;
            RaisePropertyChanged();
        }
    }

    private bool _useGetBackground;
    [Display(Name = "提取背景", GroupName = "数据")]
    public bool UseGetBackground
    {
        get { return _useGetBackground; }
        set
        {
            _useGetBackground = value;
            RaisePropertyChanged();
        }
    }


    public override IFlowableResult Invoke(Part previors, Node current)
    {
        Mat src = this.GetFromData(current).Mat;
        if (this.Mat == null)
            this.Mat = new Mat();
        //if (_srcMat.Empty())
        //    return this.OK("数据为空");

        _mog.Apply(src, this.Mat, this.LearningRate);
        if (this.UseGetBackground)
            _mog.GetBackgroundImage(this.Mat);
        this.RefreshMatToView();
        return base.Invoke(previors, current);
    }
}
