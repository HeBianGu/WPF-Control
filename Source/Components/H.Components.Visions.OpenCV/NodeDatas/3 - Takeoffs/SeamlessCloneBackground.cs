// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Components.Visions.OpenCV.NodeDatas.Other;
[Display(Name = "无缝融合背景", GroupName = "分割提取", Description = "将一幅图像中的指定目标复制后粘贴到另一幅图像中，并自然的融合", Order = 80)]
public class SeamlessCloneBackground : OpenCVNodeDataBase, ITakeoffGroupableNodeData
{
    public SeamlessCloneBackground()
    {
        this.BackgroundFilePath = "Assets\\OpenCV\\asahiyama.jpg";
    }

    private NodeDataExpression _ForegroundImageExpression;
    [GetMethodNameSource(nameof(GetImageFromExpressions))]
    [PropertyItem(typeof(ExpressionComboBoxPropertyItem))]
    [Display(Name = "前景图像", GroupName = VisionPropertyGroupNames.RunParameters, Description = "选择应用掩码的前景图像")]
    public NodeDataExpression ForegroundImageExpression
    {
        get { return _ForegroundImageExpression; }
        set
        {
            _ForegroundImageExpression = value;
            RaisePropertyChanged();
        }
    }

    private string _backgroudFilePath;
    [PropertyItem(typeof(OpenFileDialogPropertyItem))]
    [Display(Name = "背景图片", GroupName = VisionPropertyGroupNames.RunParameters)]
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
    [Display(Name = "启用无缝融合", GroupName = VisionPropertyGroupNames.RunParameters)]
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
    [Display(Name = "启用背景模糊", GroupName = VisionPropertyGroupNames.RunParameters)]
    public bool UseBlur
    {
        get { return _useBlur; }
        set
        {
            _useBlur = value;
            RaisePropertyChanged();
        }
    }

    private bool _useInvokeLogAction = false;
    [DefaultValue(false)]
    [Display(Name = "启用步骤输出", GroupName = VisionPropertyGroupNames.DisplayParameters)]
    public bool UseInvokeLogAction
    {
        get { return _useInvokeLogAction; }
        set
        {
            _useInvokeLogAction = value;
            RaisePropertyChanged();
        }
    }

    protected override async Task<IFlowableResult> BeforeInvokeAsync(IFlowableLinkData previors, IFlowableDiagramData diagram)
    {
        if (!File.Exists(this.BackgroundFilePath))
        {
            bool? r = await System.Windows.Application.Current.Dispatcher.Invoke(async () =>
            {
                return await IocMessage.Form?.ShowEdit(this, x => x.Title = "请先选择一个背景图片文件", null, x =>
                {
                    x.UsePropertyNames = $"{nameof(BackgroundFilePath)}";
                });
            });

            if (r != true)
                return this.Error("背景图片文件不存在");
        }
        return await base.BeforeInvokeAsync(previors, diagram);
    }

    protected virtual void InvokeLogAction(IMatImage mat, string message)
    {
        if (!this.UseInvokeLogAction)
            return;
        //  Do ：记录输出日志(非必选，只是在输出日志可以看到运行过程)
        this.ResultImage = mat;
        this.ResultImageSource = mat.ToImageSource();
        this.Message = message;
        if (this.DiagramData is IFlowableDiagramData diagram)
            diagram?.OnInvokedPart(this);
        Thread.Sleep(this.PreviewMillisecondsDelay);
    }

    protected override FlowableResult<IMatImage> Invoke(Mat fromImage)
    {
        //Action<Mat, IPartData, string> logAction = (mat, x, message) =>
        //{
        //    //  Do ：记录输出日志(非必选，只是在输出日志可以看到运行过程)
        //    this.Mat = mat;
        //    this.UpdateResultImageSource();
        //    this.Message = message;
        //    diagram?.OnInvokedPart(x);
        //    Thread.Sleep(this.PreviewMillisecondsDelay);
        //};
        //  Do ：接收前序流程提取前景的mask掩膜
        Mat mask = fromImage;
        string path = this.BackgroundFilePath;
        using Mat background = new Mat(path, ImreadModes.Color);
        this.InvokeLogAction(new MatImage(background), "加载的背景图片");

        Mat src = fromImage;
        if (this.TryGetExpressionValue(this.ForegroundImageExpression, out MatImage foreground))
            src = foreground.Mat;

        using Mat resizeBackground = background.Resize(src.Size(), 0, 0, InterpolationFlags.Lanczos4);
        this.InvokeLogAction(new MatImage(resizeBackground), "对背景图片设置跟源图片一样的尺寸");

        if (this.UseBlur)
        {
            Cv2.Blur(resizeBackground, resizeBackground, new OpenCvSharp.Size(8, 8));
            this.InvokeLogAction(new MatImage(resizeBackground), "对背景图片模糊处理");
        }

        //扣除mask区域
        using Mat maskedForeground = new Mat();
        Cv2.BitwiseAnd(src, src, maskedForeground, mask);
        this.InvokeLogAction(new MatImage(maskedForeground), "扣除原图片进行mask掩膜区域");

        using Mat revertMask = new Mat();
        Cv2.BitwiseNot(mask, revertMask);
        this.InvokeLogAction(new MatImage(revertMask), "反转mask掩膜区域");

        using Mat maskBackground = new Mat();
        Cv2.BitwiseAnd(resizeBackground, resizeBackground, maskBackground, revertMask);
        this.InvokeLogAction(new MatImage(maskBackground), "扣除背景图片进行反转mask掩膜区域");

        if (this.UseSeamlessClone)
        {
            OpenCvSharp.Point center = new OpenCvSharp.Point(resizeBackground.Width / 2, resizeBackground.Height / 2);
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
