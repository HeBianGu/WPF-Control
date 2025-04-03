using H.Controls.Diagram.Flowables;
using H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Video;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace H.Controls.Diagram.Presenters.OpenCV.Base;

[Icon(FontIcons.Camera)]
public abstract class VideoCaptureSrcNodeDataBase : SrcImageNodeDataBase, IVideoNodeData
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

    protected virtual async Task<bool?> InvokeFrameMatAsync(IFlowablePartData previors, IFlowableDiagramData diagram, Mat frameMat)
    {
        var invokeable = diagram;
        Action<IPartData> invoking = x =>
        {
            //OpenCVNodeDataBase data = x.GetContent<OpenCVNodeDataBase>();
            //data.UseInfoLogger = false;
            //data.UseReview = false;
            //data.UseAnimation = false;
            //diagram.Dispatcher.Invoke(() =>
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
        //this.SrcMat = this.Mat;
        UpdateMatToView();
        invoked.Invoke(this);
        var tos = this.GetToNodeDatas(diagram).OfType<IFlowableNodeData>();
        var to = tos.FirstOrDefault();
        if (to == null)
            return true;
        tos.GotoState(diagram, x => FlowableState.Wait);
        var r = await to.Start(diagram);
        await Task.Delay(1000);
        return r;
    }



    public async Task<IFlowableResult> InvokeVideoFlowable(IFlowableDiagramData diagram, Func<Task<IFlowableResult>> action)
    {
        IEnumerable<IVideoFlowable> videos = diagram.NodeDatas.OfType<IVideoFlowable>();
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

