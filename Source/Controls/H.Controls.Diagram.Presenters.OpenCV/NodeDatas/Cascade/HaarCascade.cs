
global using H.Extensions.TypeConverter;

namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Cascade;

[Display(Name = "HAAR", GroupName = "人脸检测", Order = 0)]
public class HaarCascade : CascadeClassifierOpenCVNodeDataBase
{
    private HaarType _haarType = HaarType.FrontalFace;
    [DefaultValue(HaarType.FrontalFace)]
    [Display(Name = "检测类型", GroupName = "数据")]
    public HaarType HaarType
    {
        get { return _haarType; }
        set
        {
            _haarType = value;
            RaisePropertyChanged();
        }
    }

    protected override FlowableResult<Mat> Invoke(ISrcImageNodeData srcImageNodeData, IOpenCVNodeData from, IFlowableDiagramData diagram)
    {
        string dataPath = this.GetDataPathByName();
        // Load the cascades
        CascadeClassifier haarCascade = new CascadeClassifier(dataPath);
        // Detect faces
        Mat haarResult = DetectFace(haarCascade, from.Mat);
        return this.OK(haarResult);
    }

    private string GetDataPathByName()
    {
        if (this.HaarType == HaarType.Eye)
            return this.GetDataPath(CascadeData.Eye);
        if (this.HaarType == HaarType.FrontalFace)
            return this.GetDataPath(CascadeData.Frontalface);
        if (this.HaarType == HaarType.Profileface)
            return this.GetDataPath(CascadeData.Profileface);
        if (this.HaarType == HaarType.FullBody)
            return this.GetDataPath(CascadeData.Fullbody);
        if (this.HaarType == HaarType.LeftEye)
            return this.GetDataPath(CascadeData.Lefteye);
        if (this.HaarType == HaarType.RightEye)
            return this.GetDataPath(CascadeData.Righteye);
        if (this.HaarType == HaarType.LowerBody)
            return this.GetDataPath(CascadeData.Lowerbody);
        if (this.HaarType == HaarType.UpperBody)
            return this.GetDataPath(CascadeData.Upperbody);
        if (this.HaarType == HaarType.Smile)
            return this.GetDataPath(CascadeData.Smile);
        if (this.HaarType == HaarType.Eyeglass)
            return this.GetDataPath(CascadeData.Eyeglasses);
        if (this.HaarType == HaarType.FrontalcatFace)
            return this.GetDataPath(CascadeData.Frontalcatface);
        if (this.HaarType == HaarType.LicencePlate)
            return this.GetDataPath(CascadeData.Licence_plate);
        return this.HaarType == HaarType.RussianPlate
            ? this.GetDataPath(CascadeData.Russian_plate_number)
            : throw new ArgumentException("没有识别参数");
    }
}

[TypeConverter(typeof(DisplayEnumConverter))]
public enum HaarType
{
    [Display(Name = "左眼和右眼")]
    Eye = 0,
    [Display(Name = "正脸")]
    FrontalFace,
    [Display(Name = "侧脸")]
    Profileface,
    [Display(Name = "全身")]
    FullBody,
    [Display(Name = "左眼")]
    LeftEye,
    [Display(Name = "右眼")]
    RightEye,
    [Display(Name = "下半身")]
    LowerBody,
    [Display(Name = "上半身")]
    UpperBody,
    [Display(Name = "嘴部")]
    Smile,
    [Display(Name = "墨镜")]
    Eyeglass,
    [Display(Name = "猫脸")]
    FrontalcatFace,
    [Display(Name = "证件")]
    LicencePlate,
    [Display(Name = "字母车牌")]
    RussianPlate

}
