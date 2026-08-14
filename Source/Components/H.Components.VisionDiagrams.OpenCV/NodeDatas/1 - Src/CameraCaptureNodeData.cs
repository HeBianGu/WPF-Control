// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Components.VisionDiagrams.OpenCV.NodeDatas.Src;

public interface ICameraCaptureNodeData
{

}

[Icon(FontIcons.Webcam2)]
[Display(Name = "摄像头", GroupName = "视频采集", Description = "降噪成黑白色", Order = 10)]
public class CameraCaptureNodeData : VideoCaptureNodeDataBase, ISrcImageGroupableNodeData, ICameraCaptureNodeData
{
    public CameraCaptureNodeData()
    {
        this.InvokeMillisecondsDelay = 0;
    }
    private VideoCaptureAPIs _videoCaptureAPIs = VideoCaptureAPIs.ANY;
    [Tab(VisionTabNames.RunParameters)]
    [Display(Name = "摄像头API", GroupName = VisionTabNames.RunParameters, Description = "选择 OpenCV 打开摄像头时使用的视频采集后端")]
    public VideoCaptureAPIs VideoCaptureAPIs
    {
        get { return _videoCaptureAPIs; }
        set
        {
            _videoCaptureAPIs = value;
            RaisePropertyChanged();
        }
    }

    private int _VideoCaptureIndex;
    [Tab(VisionTabNames.RunParameters)]
    [Display(Name = "摄像头序号", GroupName = VisionTabNames.RunParameters, Description = "设置需要打开的摄像头设备索引")]
    public int VideoCaptureIndex
    {
        get { return _VideoCaptureIndex; }
        set
        {
            _VideoCaptureIndex = value;
            RaisePropertyChanged();
        }
    }
    protected override async Task<IFlowableResult> BeforeInvokeAsync(IFlowableLinkData previors, IFlowableDiagramData diagram)
    {
        return await Task.FromResult(this.OK());
    }

    public override bool IsValid(out string message)
    {
        message = null;
        return true;
    }

    public override async Task<IFlowableResult> InvokeAsync(IFlowableLinkData previors, IFlowableDiagramData diagram)
    {
        //using BackgroundSubtractorMOG mog = BackgroundSubtractorMOG.Create();
        DateTime dateTime = DateTime.Now;
        return await Task.Run(async () =>
        {
            // Opens MP4 file (ffmpeg is probably needed)
            using VideoCapture capture = new VideoCapture();
            capture.Open(this.VideoCaptureIndex, this.VideoCaptureAPIs);
            if (!capture.IsOpened())
                return this.Error("摄像头打开失败");
            int sleepTime = (int)Math.Round(this.InvokeMillisecondsDelay / capture.Fps);
            return await this.InvokeVideoFlowable(diagram, async () =>
            {
                int index = 0;
                while (true)
                {
                    if (this.State == FlowableState.Canceling)
                        return this.Error("用户取消");
                    diagram.Wait(x => x != this);
                    diagram.Message = "发送采集图像...";
                    Mat frameMat = capture.RetrieveMat();
                    this.Message = $"{index++}";
                    bool? r = await this.InvokeFrameMatAsync(new MatImage(frameMat));
                    frameMat.Dispose();
                    this.TimeSpan = DateTime.Now - dateTime;
                    if (r == null)
                        return this.Error("用户取消");
                    else if (r == false)
                        return this.Error();
                    Cv2.WaitKey(sleepTime);
                }
            });
        });
    }

    public override void LoadDefault()
    {
        base.LoadDefault();
        this.SrcFilePaths.Clear();
        this.SrcFilePath = null;
    }
}
