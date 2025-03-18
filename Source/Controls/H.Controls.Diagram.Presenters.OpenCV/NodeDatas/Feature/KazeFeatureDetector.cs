namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Feature;
[Display(Name = "KAZE", GroupName = "特征提取", Order = 0)]
public class KazeFeatureDetector : FeatureOpenCVNodeDataBase
{
    private bool _extended = false;
    [Display(Name = "Extended", GroupName = "数据")]
    public bool Extended
    {
        get { return _extended; }
        set
        {
            _extended = value;
            RaisePropertyChanged();
        }
    }

    private bool _upright = false;
    [Display(Name = "Upright", GroupName = "数据")]
    public bool Upright
    {
        get { return _upright; }
        set
        {
            _upright = value;
            RaisePropertyChanged();
        }
    }

    private float _threshold = 0.001f;
    [Display(Name = "Threshold", GroupName = "数据")]
    public float Threshold
    {
        get { return _threshold; }
        set
        {
            _threshold = value;
            RaisePropertyChanged();
        }
    }

    private int _nOctaves = 4;
    [Display(Name = "nOctaves", GroupName = "数据")]
    public int nOctaves
    {
        get { return _nOctaves; }
        set
        {
            _nOctaves = value;
            RaisePropertyChanged();
        }
    }

    private int _nOctaveLayers = 4;
    [Display(Name = "nOctaveLayers", GroupName = "数据")]
    public int nOctaveLayers
    {
        get { return _nOctaveLayers; }
        set
        {
            _nOctaveLayers = value;
            RaisePropertyChanged();
        }
    }

    private KAZEDiffusivityType _diffusivity = KAZEDiffusivityType.DiffPmG2;
    [Display(Name = "Diffusivity", GroupName = "数据")]
    public KAZEDiffusivityType Diffusivity
    {
        get { return _diffusivity; }
        set
        {
            _diffusivity = value;
            RaisePropertyChanged();
        }
    }

    protected override FlowableResult<Mat> Invoke(ISrcImageNodeData srcImageNodeData, IOpenCVNodeData from, IFlowableDiagramData diagram)
    {
        Mat gray = from.Mat;
        KAZE kaze = KAZE.Create(this.Extended, this.Upright, this.Threshold, this.nOctaves, this.nOctaveLayers, this.Diffusivity);
        //var akaze = AKAZE.Create();

        Mat kazeDescriptors = new Mat();
        Mat akazeDescriptors = new Mat();
        KeyPoint[] kazeKeyPoints = null;
        TimeSpan kazeTime = MeasureTime(() =>
            kaze.DetectAndCompute(gray, null, out kazeKeyPoints, kazeDescriptors));
        //var akazeTime = MeasureTime(() =>
        //    akaze.DetectAndCompute(gray, null, out akazeKeyPoints, akazeDescriptors));

        Mat dstKaze = new Mat();
        Mat dstAkaze = new Mat();
        Cv2.DrawKeypoints(gray, kazeKeyPoints, dstKaze);
        //Cv2.DrawKeypoints(gray, akazeKeyPoints, dstAkaze);

        //using (new Window(String.Format("KAZE [{0:F2}ms]", kazeTime.TotalMilliseconds), dstKaze))
        //using (new Window(String.Format("AKAZE [{0:F2}ms]", akazeTime.TotalMilliseconds), dstAkaze))
        //{
        //    Cv2.WaitKey();
        //}
        return this.OK(dstKaze);
    }

    private TimeSpan MeasureTime(Action action)
    {
        Stopwatch watch = Stopwatch.StartNew();
        action();
        watch.Stop();
        return watch.Elapsed;
    }
}
