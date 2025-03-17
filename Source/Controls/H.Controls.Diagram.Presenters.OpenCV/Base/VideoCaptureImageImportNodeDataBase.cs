using H.Controls.Diagram.Flowables;
using H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Video;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace H.Controls.Diagram.Presenters.OpenCV.Base;

[Icon(FontIcons.Camera)]
public abstract class VideoCaptureImageImportNodeDataBase : ImageImportNodeDataBase, IVideoCaptureImageImportNodeData
{
    private int _sleepMilliseconds = 30;
    [Display(Name = "间隔时间", GroupName = "数据")]
    public int SleepMilliseconds
    {
        get { return _sleepMilliseconds; }
        set
        {
            _sleepMilliseconds = value;
            RaisePropertyChanged();
        }
    }

    protected virtual async Task<bool?> InvokeFrameMatAsync(IFlowablePartData previors, IFlowableDiagramData current, Mat frameMat)
    {
        var invokeable = current;
        Action<IPartData> invoking = x =>
        {
            //OpenCVNodeDataBase data = x.GetContent<OpenCVNodeDataBase>();
            //data.UseInfoLogger = false;
            //data.UseReview = false;
            //data.UseAnimation = false;
            //current.Dispatcher.Invoke(() =>
            //{
            //    invokeable?.OnInvokingPart(x);
            //});
        };

        Action<IPartData> invoked = x =>
        {
            if (this.State == FlowableState.Canceling)
                return;
            invokeable?.OnInvokedPart(x);
            //Thread.Sleep(1000);
        };
        invoking.Invoke(this);
        this.Mat = frameMat;
        this.SrcMat = this.Mat;
        UpdateMatToView();
        invoked.Invoke(this);
        var tos = this.GetToNodeDatas(current).OfType<IFlowableNodeData>();
        var to = tos.FirstOrDefault();
        if (to == null)
            return true;
        tos.GotoState(current, x => FlowableState.Wait);
        var r = await to.Invoke(current);
        await Task.Delay(1000);
        return r;
    }



    public async Task<IFlowableResult> InvokeVideoFlowable(IFlowableDiagramData current, Func<Task<IFlowableResult>> action)
    {
        IEnumerable<IVideoFlowable> videos = current.NodeDatas.OfType<IVideoFlowable>();
        foreach (IVideoFlowable video in videos)
        {
            video.Begin();
        }
        var r = action.Invoke();
        foreach (IVideoFlowable video in videos)
        {
            video.End();
        }
        return await r;
    }

}

