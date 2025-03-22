namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Video;
[Display(Name = "前景提取", GroupName = "视频操作", Order = 0)]
public class MOG : OpenCVNodeDataBase, IVideoFlowable
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
    [Display(Name = "历史帧数", GroupName = "数据", Description = "设置高斯混合模型的历史帧数")]
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
    [Display(Name = "成分数量", GroupName = "数据", Description = "设置高斯混合模型中混合成分的数量")]
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
    [Display(Name = "背景比例", GroupName = "数据", Description = "设置背景建模过程中前景和背景的比例")]
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
    [Display(Name = "噪声水平", GroupName = "数据", Description = "它决定了在背景建模过程中如何处理图像中的噪声")]
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
    [Display(Name = "学习速率", GroupName = "数据", Description = "它决定了新帧对背景模型的影响程度")]
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


    protected override FlowableResult<Mat> Invoke(ISrcImageNodeData srcImageNodeData, IOpenCVNodeData from, IFlowableDiagramData diagram)
    {
        Mat src = from.Mat;
        if (this.Mat == null)
            this.Mat = new Mat();
        //if (_srcMat.Empty())
        //    return this.OK("数据为空");

        _mog.Apply(src, this.Mat, this.LearningRate);
        if (this.UseGetBackground)
            _mog.GetBackgroundImage(this.Mat);
        return this.OK(this.Mat);
    }
}
