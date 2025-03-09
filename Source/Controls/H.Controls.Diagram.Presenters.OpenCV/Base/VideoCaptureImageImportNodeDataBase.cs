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

    protected virtual async Task<bool?> InvokeFrameMatAsync(Part previors, Node current, Mat frameMat)
    {
        var diagram = current.GetDiagram();
        var invokeable = diagram.InvokeDispatcher(x => x.DataContext) as IPartInvokeable;
        Action<Part> invoking = x =>
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

        Action<Part> invoked = x =>
        {
            if (this.State == FlowableState.Canceling)
                return;
            current.Dispatcher.Invoke(() =>
            {
                invokeable?.OnInvokedPart(x);
            });
            //Thread.Sleep(1000);
        };
        invoking.Invoke(current);
        this.Mat = frameMat;
        this.SrcMat = this.Mat;
        RefreshMatToView();
        invoked.Invoke(current);
        var tos = current.GetToNodes();
        Node to = tos.FirstOrDefault();
        if (to == null)
            return true;

        await to.Dispatcher.InvokeAsync(() =>
        {
            tos.Wait();
        });
        //await to.Dispatcher.InvokeAsync(async () =>
        //    {
        var r = await to.InvokeNode(invoking, invoked);
        await Task.Delay(1000);
        return r;

        //});
        //x =>
        //{
        //    OpenCVNodeDataBase data = x.GetContent<OpenCVNodeDataBase>();
        //    data.UseInfoLogger = false;
        //    data.UseReview = false;
        //    data.UseAnimation = false;
        //}, 

    }



    public async Task<IFlowableResult> InvokeVideoFlowable(Node current, Func<Task<IFlowableResult>> action)
    {
        IEnumerable<IVideoFlowable> videos = current.GetAllParts().Select(x => x.GetContent<IFlowable>()).OfType<IVideoFlowable>();
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

