global using H.Services.Logger;
using System.Threading.Tasks;

namespace H.Controls.Diagram.Presenters.OpenCV.Base;
public abstract class OpenCVNodeData : OpenCVNodeDataBase
{
    //public override void LoadDefault()
    //{
    //    base.LoadDefault();
    //    this.Width = 300;
    //    this.Height = 200;
    //}

    protected override ImageSource CreateImageSource()
    {
        return null;
    }


    //protected Mat GetFromMat(Node current)
    //{
    //    var from = current.GetFromNodes().FirstOrDefault();
    //    var data = from?.GetContent<IOpenCVNodeData>();
    //    return data?.Mat;
    //}

    //protected string GetFromFilePath(Node current)
    //{
    //    var from = current.GetFromNodes().FirstOrDefault();
    //    if (from == null)
    //        return null;
    //    var data = from.GetContent<IOpenCVNodeData>();
    //    return data.FilePath;
    //}

    protected IOpenCVNodeData GetFromData(Node current)
    {
        Node from = current.GetFromNodes().FirstOrDefault();
        return from == null ? null : from.GetContent<IOpenCVNodeData>();
    }

    protected void RefreshMatToView(Mat mat)
    {
        if (this.ImageSource == null)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                this.ImageSource = mat.Empty() ? null : (ImageSource)(mat?.ToWriteableBitmap());
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
                    this.ImageSource = mat.Empty() ? null : (ImageSource)(mat?.ToWriteableBitmap());
                });
            }
        }
    }

    protected void RefreshMatToView()
    {
        this.RefreshMatToView(this.Mat);
    }


    protected override void OnDispatcherPropertyChanged()
    {
        if (this._preMat == null)
            return;
        try
        {
            this.Refresh();
        }
        catch (Exception ex)
        {
            this.Message = ex.Message;
            IocLog.Instance?.Error(ex);
        }
    }

    protected virtual string GetDataPath(string dataPath)
    {
        return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, dataPath);
    }

    protected Mat _preMat;
    protected string _srcFilePath;
    public override async Task<IFlowableResult> InvokeAsync(Part previors, Node current)
    {
        return await Task.Run(async () =>
        {
            Node from = current.GetFromNodes().FirstOrDefault();
            if (from == null)
            {
                return this.Invoke(previors, current);
            }
            this.SrcMat = this.GetFromData(current).SrcMat;
            this._srcFilePath = this.GetFromData(current).FilePath;
            this.FilePath = _srcFilePath;
            this._preMat = this.GetFromData(current).Mat;
            if (_preMat.Empty())
                return this.OK("数据为空");
            if (this.UseReview)
            {
                this.Mat = _preMat;
                this.RefreshMatToView();
                await Task.Delay(1500);
            }

            return this.Invoke(previors, current);
        });
    }

    public override IFlowableResult Invoke(Part previors, Node current)
    {
        IFlowableResult r = this.Refresh();
        return r.State == FlowableResultState.Error ? r : base.Invoke(previors, current);
    }

    protected virtual IFlowableResult Refresh()
    {
        return this.OK();
    }
}
