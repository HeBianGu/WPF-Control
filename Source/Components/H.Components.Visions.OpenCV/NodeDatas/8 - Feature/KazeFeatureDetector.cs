// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.VisionMaster.OpenCV.NodeDatas.Feature;
[Icon(FontIcons.GenericScan)]
[Display(Name = "KAZE特征提取", GroupName = "特征提取", Order = 0)]
public class KazeFeatureDetector : FeatureOpenCVNodeDataBase
{
    private bool _extended = false;
    [Display(Name = "Extended", GroupName = VisionPropertyGroupNames.RunParameters)]
    public bool Extended
    {
        get { return _extended; }
        set
        {
            _extended = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private bool _upright = false;
    [Display(Name = "Upright", GroupName = VisionPropertyGroupNames.RunParameters)]
    public bool Upright
    {
        get { return _upright; }
        set
        {
            _upright = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private float _threshold = 0.001f;
    [Display(Name = "Threshold", GroupName = VisionPropertyGroupNames.RunParameters)]
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
    [Display(Name = "nOctaves", GroupName = VisionPropertyGroupNames.RunParameters)]
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
    [Display(Name = "nOctaveLayers", GroupName = VisionPropertyGroupNames.RunParameters)]
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
    [Display(Name = "Diffusivity", GroupName = VisionPropertyGroupNames.RunParameters)]
    public KAZEDiffusivityType Diffusivity
    {
        get { return _diffusivity; }
        set
        {
            _diffusivity = value;
            RaisePropertyChanged();
        }
    }

    protected override FlowableResult<IMatImage> Invoke(Mat fromImage)
    {
        using Mat gray = fromImage.Clone();
        KAZE kaze = KAZE.Create(this.Extended, this.Upright, this.Threshold, this.nOctaves, this.nOctaveLayers, this.Diffusivity);
        //var akaze = AKAZE.Create();

        using Mat kazeDescriptors = new Mat();
        using Mat akazeDescriptors = new Mat();
        KeyPoint[] keypoints = null;
        TimeSpan kazeTime = MeasureTime(() =>
            kaze.DetectAndCompute(gray, null, out keypoints, kazeDescriptors));
        //var akazeTime = MeasureTime(() =>
        //    akaze.DetectAndCompute(gray, null, out akazeKeyPoints, akazeDescriptors));

        Mat dst = new Mat();
        //Mat dstAkaze = new Mat();
        Cv2.DrawKeypoints(gray, keypoints, dst);
        //Cv2.DrawKeypoints(gray, akazeKeyPoints, dstAkaze);

        //using (new Window(String.Format("KAZE [{0:F2}ms]", kazeTime.TotalMilliseconds), dstKaze))
        //using (new Window(String.Format("AKAZE [{0:F2}ms]", akazeTime.TotalMilliseconds), dstAkaze))
        //{
        //    Cv2.WaitKey();
        //}
        this.FeatureCountResult = keypoints.Length;
        return this.OK(dst, keypoints.ToResultPresenter(), this.FeatureCountResult.ToDetectSuccessMessage());
    }

    private TimeSpan MeasureTime(Action action)
    {
        Stopwatch watch = Stopwatch.StartNew();
        action();
        watch.Stop();
        return watch.Elapsed;
    }
}
