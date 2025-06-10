// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Text.Json.Serialization;
using System.Windows;
using System.Xml.Serialization;
using H.Controls.Diagram.Presenter.Extensions;

namespace H.Controls.Diagram.Presenters.OpenCV.Base;

public abstract class OpenCVNodeDataBase : OpenCVStyleNodeDataBase, IOpenCVNodeData
{
 
    ~OpenCVNodeDataBase()
    {
        this.Mat?.Dispose();
    }
    [JsonIgnore]
    [Browsable(false)]
    [XmlIgnore]
    public Mat Mat { get; set; }

    private bool _useReview = true;
    [JsonIgnore]
    [Browsable(false)]
    [XmlIgnore]
    public bool UseReview
    {
        get { return _useReview; }
        set
        {
            _useReview = value;
            RaisePropertyChanged();
        }
    }

    private ImageSource _imageSource;
    [JsonIgnore]
    [Browsable(false)]
    [XmlIgnore]
    public ImageSource ImageSource
    {
        get { return _imageSource; }
        set
        {
            _imageSource = value;
            RaisePropertyChanged();
        }
    }

    private int _previewMillisecondsDelay = 1500;
    [DefaultValue(1500)]
    [Display(Name = "预览延迟", GroupName = "流程", Description = "设置生成图像后预览等待时间")]
    public int PreviewMillisecondsDelay
    {
        get { return _previewMillisecondsDelay; }
        set
        {
            _previewMillisecondsDelay = value;
            RaisePropertyChanged();
        }
    }

    private int _invokeMillisecondsDelay = 500;
    [DefaultValue(500)]
    [Display(Name = "执行延迟", GroupName = "流程", Description = "执行完成后等待时间")]
    public int InvokeMillisecondsDelay
    {
        get { return _invokeMillisecondsDelay; }
        set
        {
            _invokeMillisecondsDelay = value;
            RaisePropertyChanged();
        }
    }

    //public override void LoadDefault()
    //{
    //    base.LoadDefault();
    //    this.Width = 240;
    //    this.Height = 80;
    //}

    //private Mat _preMat;
    //protected Mat PreviourMat => this._preMat;
    //private string _srcFilePath;
    //protected string SrcFilePath => this._srcFilePath;

    //public override async Task<IFlowableResult> InvokeAsync(IFlowableLinkData previors, IFlowableDiagramData diagram)
    //{
    //    return await Task.Run((async () =>
    //    {
    //        var fromData = this.GetFromNodeData<IOpenCVNodeData>(diagram, previors);
    //        if (fromData == null)
    //            return this.Invoke(previors, diagram);
    //        this.SrcMat = fromData.SrcMat;
    //        //this._srcFilePath = fromData.SrcFilePath;
    //        this.SrcFilePath = fromData.SrcFilePath;
    //        this._preMat = fromData.Mat;
    //        if (this._preMat == null || this._preMat.Empty())
    //            return this.Error("传入图像数据为空");
    //        if (this.UseReview)
    //        {
    //            this.Mat = this._preMat;
    //            this.UpdateMatToView();
    //            await Task.Delay(this.PreviewMillisecondsDelay);
    //        }

    //        return this.Invoke(previors, diagram);
    //    }));
    //}
    public override IFlowableResult Invoke(IFlowableLinkData previors, IFlowableDiagramData diagram)
    {
        var srcData = diagram.GetStartNodeDatas().OfType<ISrcImageNodeData>().FirstOrDefault();
        var fromData = this.GetFromNodeData<IOpenCVNodeData>(diagram, previors);
        var result = this.Invoke(srcData, fromData ?? srcData, diagram);
        this.Mat?.Dispose();
        this.Mat = result.Value;
        if (this.UseReview)
        {
            this.UpdateMatToView();
            Thread.Sleep(this.PreviewMillisecondsDelay);
        }
        return result;
    }
    protected abstract FlowableResult<Mat> Invoke(ISrcImageNodeData srcImageNodeData, IOpenCVNodeData from, IFlowableDiagramData diagram);

    protected void UpdateMatToView(Mat mat)
    {
        if (this.Mat != mat)
            this.Mat?.Dispose();
        this.Mat = mat;
        if (this.ImageSource == null)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                this.ImageSource = mat.Empty() ? null : mat?.ToWriteableBitmap();
            });
        }
        else
        {
            if (this.ImageSource.CheckAccess())
            {
                this.ImageSource = mat?.ToWriteableBitmap();
            }
            else
            {
                System.Windows.Application.Current.Dispatcher.Invoke(() =>
                {
                    this.ImageSource = mat.Empty() ? null : mat?.ToWriteableBitmap();
                });
            }
        }
    }

    protected void UpdateMatToView()
    {
        this.UpdateMatToView(this.Mat);
    }

    protected virtual FlowableResult<Mat> OK(Mat mat, string message = "运行成功")
    {
        this.Message = message;
        return new FlowableResult<Mat>(mat, message) { State = FlowableResultState.OK };
    }

    protected virtual FlowableResult<Mat> Error(Mat mat, string message = "运行错误")
    {
        this.Message = message;
        return new FlowableResult<Mat>(mat, message) { State = FlowableResultState.Error };
    }
}
