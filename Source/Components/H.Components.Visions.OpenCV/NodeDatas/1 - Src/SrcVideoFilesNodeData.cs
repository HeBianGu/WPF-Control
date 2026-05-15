// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using H.Services.AppPath;
global using H.Services.Message;
global using H.Services.Message.IODialog;
global using System.IO;

namespace H.Components.Visions.OpenCV.NodeDatas.Src;
public interface IVideoFlowable
{
    void Begin();
    void End();
}

[Icon(FontIcons.Video)]
[Display(Name = "本地视频源", GroupName = "数据源", Description = "降噪成黑白色", Order = 100)]
public class SrcVideoFilesNodeData : VideoCaptureNodeDataBase, ISrcImageGroupableNodeData
{
    public SrcVideoFilesNodeData()
    {
        this.UseAnimation = true;
        //this.SrcFilePath = MoviePath.Bach.ToDataPath();
    }

    private int _startFrame = 0;
    [DefaultValue(0)]
    [Display(Name = "开始位置(帧)", GroupName = VisionPropertyGroupNames.RunParameters)]
    public int StartFrame
    {
        get { return _startFrame; }
        set
        {
            _startFrame = value;
            RaisePropertyChanged();
        }
    }

    private int _endFrame = int.MaxValue;
    [DefaultValue(int.MaxValue)]
    [Display(Name = "结束位置(帧)", GroupName = VisionPropertyGroupNames.RunParameters)]
    public int EndFrame
    {
        get { return _endFrame; }
        set
        {
            _endFrame = value;
            RaisePropertyChanged();
        }
    }

    private int _spanFrame = 1;
    [Range(1, int.MaxValue)]
    [DefaultValue(1)]
    [Display(Name = "采样间隔(帧)", GroupName = VisionPropertyGroupNames.RunParameters)]
    public int SpanFrame
    {
        get { return _spanFrame; }
        set
        {
            _spanFrame = value;
            RaisePropertyChanged();
        }
    }

    public override async Task<IFlowableResult> InvokeAsync(IFlowableLinkData previors, IFlowableDiagramData diagram)
    {
        DateTime dateTime = DateTime.Now;
        return await Task.Run(async () =>
        {
            if (this.State == FlowableState.Canceling)
                return this.Error("用户取消");
            // Opens MP4 file (ffmpeg is probably needed)
            using VideoCapture capture = new VideoCapture(this.SrcFilePath);
            if (File.Exists(this.SrcFilePath) == false)
                return this.Error("视频文件不存在");
            if (!capture.IsOpened())
                return this.Error("视频打开失败");
            int sleepTime = (int)Math.Round(this.InvokeMillisecondsDelay / capture.Fps);
            return await this.InvokeVideoFlowable(diagram, async () =>
              {
                  int index = this.StartFrame;
                  while (true)
                  {
                      if (this.State == FlowableState.Canceling)
                          return this.Error("用户取消");

                      diagram.Wait(x => x != this);
                      diagram.Message = "发送采集图像...";
                      capture.Set(VideoCaptureProperties.PosFrames, index);
                      Mat frameMat = new Mat();
                      capture.Read(frameMat); // same as cvQueryFrame
                      if (frameMat.Empty())
                          break;
                      index = index + this.SpanFrame;
                      //if (index < this.StartFrame)
                      //    continue;
                      if (index > this.EndFrame)
                          continue;
                      //if (index % this.SpanFrame != 0)
                      //    continue;

                      this.Message = $"{index}/{capture.FrameCount}[{Math.Round(index * 100.0 / capture.FrameCount, 1)}%]";
                      bool? r = await this.InvokeFrameMatAsync(new MatImage(frameMat));
                      //  Do ：最后一帧不释放，传递给后续流程
                      if (index != capture.FrameCount)
                          frameMat.Dispose();
                      this.TimeSpan = DateTime.Now - dateTime;
                      if (r == null)
                          return this.Error("用户取消");
                      else if (r == false)
                          return this.Error();
                      Cv2.WaitKey(sleepTime);
                  }
                  GC.Collect();
                  return this.OK("运行成功");
              });
            // Frame image buffer
            // When the movie playback reaches end, Mat.data becomes NULL.
            //return this.OK("运行成功");
        });
    }

    public override void LoadDefault()
    {
        base.LoadDefault();
        IEnumerable<string> imagePaths = AppDomianPaths.Assets.GetAllVedios();
        this.SrcFilePaths.Clear();
        foreach (string imagePath in imagePaths)
        {
            string relatvePath = Path.GetRelativePath(AppDomain.CurrentDomain.BaseDirectory, imagePath);
            this.SrcFilePaths.Add(relatvePath);
        }
        this.SrcFilePath = this.SrcFilePaths?.FirstOrDefault();
    }

    protected override void AddFiles()
    {
        IocMessage.IOFolderDialog.ShowOpenFolderAction(x =>
        {
            string selectedFolderPath = x;
            IEnumerable<string> images = selectedFolderPath.GetAllVedios();
            this.SrcFilePaths.AddRange(images);
            if (this.SrcFilePaths.Count == 0)
                this.SrcFilePath = this.SrcFilePaths.FirstOrDefault();
        });
    }

    protected override void AddFile()
    {
        IocMessage.IOFileDialog.ShowOpenVideoFiles(x =>
        {
            foreach (string item in x)
            {
                this.SrcFilePaths.Add(item);
            }
            if (this.SrcFilePaths.Count == 0)
                this.SrcFilePath = this.SrcFilePaths.FirstOrDefault();
        });
    }
}
