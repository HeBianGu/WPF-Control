namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Basic;

[Display(Name = "无缝融合背景", GroupName = "基础函数", Description = "将一幅图像中的指定目标复制后粘贴到另一幅图像中，并自然的融合", Order = 80)]
public class SeamlessCloneBackground : Blur
{
    public SeamlessCloneBackground()
    {
        this._backgroudFilePath = this.GetDataPath(ImagePath.Asahiyama);
    }

    private string _backgroudFilePath;
    [PropertyItem(typeof(OpenFileDialogPropertyItem))]
    [Display(Name = "背景图片", GroupName = "数据")]
    public string BackgroundFilePath
    {
        get { return _backgroudFilePath; }
        set
        {
            _backgroudFilePath = value;
            RaisePropertyChanged();
        }
    }

    private bool _useSeamlessClone;
    [DefaultValue(false)]
    [Display(Name = "启用无缝融合", GroupName = "数据")]
    public bool UseSeamlessClone
    {
        get { return _useSeamlessClone; }
        set
        {
            _useSeamlessClone = value;
            RaisePropertyChanged();
        }
    }

    private bool _useBlur;
    [DefaultValue(false)]
    [Display(Name = "启用背景模糊", GroupName = "数据")]
    public bool UseBlur
    {
        get { return _useBlur; }
        set
        {
            _useBlur = value;
            RaisePropertyChanged();
        }
    }


    protected override FlowableResult<Mat> Invoke(ISrcImageNodeData srcImageNodeData, IOpenCVNodeData from, IFlowableDiagramData diagram)
    {
        // 接收前景的掩膜
        Mat mask = from.Mat;
        string path = this.BackgroundFilePath;
        using Mat background = new Mat(path, ImreadModes.Color);
        Mat src = srcImageNodeData.Mat;
        using Mat resizeBackground = background.Resize(src.Size(), 0, 0, InterpolationFlags.Lanczos4);

        if(this.UseBlur)
            Cv2.Blur(resizeBackground, resizeBackground, KSize, Anchor, BorderType);

        //扣除mask区域
        using Mat maskedForeground = new Mat();
        Cv2.BitwiseAnd(src, src, maskedForeground, mask);

        using Mat maskBackground = new Mat();
        using Mat revertMask = new Mat();
        Cv2.BitwiseNot(mask, revertMask);
        Cv2.BitwiseAnd(resizeBackground, resizeBackground, maskBackground, revertMask);

        if(this.UseSeamlessClone)
        {
            Point center = new Point(resizeBackground.Width / 2, resizeBackground.Height / 2);
            // 执行无缝融合
            Mat result = new Mat();
            Cv2.SeamlessClone(
                maskedForeground,         // 前景图像
                maskBackground,         // 背景图像
                mask,               // 掩膜
                center,             // 放置位置中心
                result,             // 输出结果
                SeamlessCloneMethods.NormalClone  // 融合方法
            );
            return this.OK(result);
        }
        else
        {
            Mat bitwiseAnd = new Mat();
            Cv2.BitwiseOr(maskBackground, maskedForeground, bitwiseAnd);
            return this.OK(bitwiseAnd);
        }
    }
}
