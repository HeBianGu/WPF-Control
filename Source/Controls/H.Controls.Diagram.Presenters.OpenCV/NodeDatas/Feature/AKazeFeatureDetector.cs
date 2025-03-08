global using System.Diagnostics;
namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Feature;


[Display(Name = "AKAZE", GroupName = "特征提取", Order = 0)]
public class AKazeFeatureDetector : FeatureOpenCVNodeDataBase
{
    private AKAZEDescriptorType _descriptorType = AKAZEDescriptorType.MLDB;
    [Display(Name = "DescriptorType", GroupName = "数据")]
    public AKAZEDescriptorType DescriptorType
    {
        get { return _descriptorType; }
        set
        {
            _descriptorType = value;
            DispatcherRaisePropertyChanged();
        }
    }


    private int _descriptorSize = 0;
    [Display(Name = "DescriptorSize", GroupName = "数据")]
    public int DescriptorSize
    {
        get { return _descriptorSize; }
        set
        {
            _descriptorSize = value;
            DispatcherRaisePropertyChanged();
        }
    }


    private int _descriptorChannels = 3;
    [Display(Name = "DescriptorChannels", GroupName = "数据")]
    public int DescriptorChannels
    {
        get { return _descriptorChannels; }
        set
        {
            _descriptorChannels = value;
            DispatcherRaisePropertyChanged();
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
            DispatcherRaisePropertyChanged();
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
            DispatcherRaisePropertyChanged();
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
            DispatcherRaisePropertyChanged();
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
            DispatcherRaisePropertyChanged();
        }
    }

    //public override IFlowableResult Invoke(Part previors, Node current)
    //{
    //    var filePath = GetFromFilePath(current);
    //    var gray = this.GetFromMat(current);
    //    var akaze = AKAZE.Create(this.DescriptorType, this.DescriptorSize, this.DescriptorChannels, this.Threshold, this.nOctaves, this.nOctaveLayers, this.Diffusivity);
    //    //var kazeDescriptors = new Mat();
    //    var akazeDescriptors = new Mat();
    //    KeyPoint[] akazeKeyPoints = null;
    //    var akazeTime = MeasureTime(() =>
    //        akaze.DetectAndCompute(gray, null, out akazeKeyPoints, akazeDescriptors));
    //    var dstAkaze = new Mat();
    //    Cv2.DrawKeypoints(gray, akazeKeyPoints, dstAkaze);
    //    RefreshMatToView(dstAkaze);
    //    return base.Invoke(previors, current);
    //}

    protected override IFlowableResult Invoke()
    {
        string filePath = this.SrcFilePath;
        Mat gray = this.PreviourMat;
        AKAZE akaze = AKAZE.Create(this.DescriptorType, this.DescriptorSize, this.DescriptorChannels, this.Threshold, this.nOctaves, this.nOctaveLayers, this.Diffusivity);
        //var kazeDescriptors = new Mat();
        Mat akazeDescriptors = new Mat();
        KeyPoint[] akazeKeyPoints = null;
        TimeSpan akazeTime = MeasureTime(() =>
            akaze.DetectAndCompute(gray, null, out akazeKeyPoints, akazeDescriptors));
        Mat dstAkaze = new Mat();
        Cv2.DrawKeypoints(gray, akazeKeyPoints, dstAkaze);
        RefreshMatToView(dstAkaze);
        return base.Invoke();
    }

    private TimeSpan MeasureTime(Action action)
    {
        Stopwatch watch = Stopwatch.StartNew();
        action();
        watch.Stop();
        return watch.Elapsed;
    }
}
