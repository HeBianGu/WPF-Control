using H.Controls.Diagram.Flowables;
using H.Services.Common;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Video;
public interface IVideoFlowable
{
    void Begin();
    void End();
}

[Display(Name = "视频读取", GroupName = "数据源", Description = "降噪成黑白色", Order = 0)]
public class VideoCapture : VideoCaptureSrcNodeDataBase, ISrcFilePathableStartNodeData
{
    public VideoCapture()
    {
        this.UseAnimation = true;
        this.SrcFilePath = GetDataPath(MoviePath.Bach);
    }
    private string _srcFilePath;
    [Browsable(true)]
    [Display(Name = "源文件地址", GroupName = "数据")]
    [PropertyItem(typeof(OpenFileDialogPropertyItem))]
    public override string SrcFilePath
    {
        get { return _srcFilePath; }
        set
        {
            _srcFilePath = value;
            RaisePropertyChanged();
        }
    }
    private int _startFrame = 0;
    [DefaultValue(0)]
    [Display(Name = "开始位置(帧)", GroupName = "数据")]
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
    [Display(Name = "结束位置(帧)", GroupName = "数据")]
    public int EndFrame
    {
        get { return _endFrame; }
        set
        {
            _endFrame = value;
            RaisePropertyChanged();
        }
    }

    private int _spanFrame = 60;
    [DefaultValue(60)]
    [Display(Name = "采样间隔(帧)", GroupName = "数据")]
    public int SpanFrame
    {
        get { return _spanFrame; }
        set
        {
            _spanFrame = value;
            RaisePropertyChanged();
        }
    }
    //protected override string GetFilter()
    //{
    //    return "视频文件|*.asf;*.wav;*.mp4;*.mpg;*wmv;mtv";
    //}

    public override async Task<IFlowableResult> InvokeAsync(IFlowableLinkData previors, IFlowableDiagramData diagram)
    {
        return await Task.Run(async () =>
        {
            if (this.State == FlowableState.Canceling)
                return this.Error("用户取消");
            // Opens MP4 file (ffmpeg is probably needed)
            using var capture = new OpenCvSharp.VideoCapture(this.SrcFilePath);
            if (File.Exists(this.SrcFilePath) == false)
                return this.Error("视频文件不存在");
            if (!capture.IsOpened())
                return this.Error("视频打开失败");
            int sleepTime = (int)Math.Round(this.SleepMilliseconds / capture.Fps);
            return await this.InvokeVideoFlowable(diagram, async () =>
              {
                  int index = 0;
                  while (true)
                  {
                      if (this.State == FlowableState.Canceling)
                          return this.Error("用户取消");
                      capture.Set(VideoCaptureProperties.PosFrames, index);
                      using Mat frameMat = new Mat();
                      capture.Read(frameMat); // same as cvQueryFrame
                      if (frameMat.Empty())
                          break;
                      //index++;
                      index = index + this.SpanFrame;
                      if (index < this.StartFrame)
                          continue;
                      if (index > this.EndFrame)
                          continue;
                      if (index % this.SpanFrame != 0)
                          continue;
                      this.Message = $"{index}/{capture.FrameCount}[{Math.Round(index * 100.0 / capture.FrameCount, 1) }%]";
                      var r = await this.InvokeFrameMatAsync(previors, diagram, frameMat);
                      if (r == null)
                          return this.Error("用户取消");
                      else if (r == false)
                          return this.Error();
                      Cv2.WaitKey(sleepTime);
                  }

                  return this.OK("运行成功");
              });
            // Frame image buffer
            // When the movie playback reaches end, Mat.data becomes NULL.
            //return this.OK("运行成功");
        });
    }

}
