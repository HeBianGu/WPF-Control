global using H.Services.Logger;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace H.Controls.Diagram.Presenters.OpenCV.Base;
public abstract class OpenCVNodeData : OpenCVNodeDataBase
{
    ~OpenCVNodeData()
    {
        this.PreviourMat?.Dispose();
    }
    protected IOpenCVNodeData GetFromData(Node current)
    {
        Node from = current.GetFromNodes().FirstOrDefault();
        return from?.Data as IOpenCVNodeData;
    }

    protected void RefreshMatToView(Mat mat)
    {
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

    protected void RefreshMatToView()
    {
        this.RefreshMatToView(this.Mat);
    }

    protected virtual string GetDataPath(string dataPath)
    {
        if (string.IsNullOrEmpty(dataPath))
            return null;
        return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, dataPath);
    }

    private Mat _preMat;
    protected Mat PreviourMat => this._preMat;
    //private string _srcFilePath;
    //protected string SrcFilePath => this._srcFilePath;

    public override async Task<IFlowableResult> InvokeAsync(Part previors, Node current)
    {
        return await Task.Run((Func<Task<IFlowableResult>>)(async () =>
        {
            Node from = current.GetFromNodes().FirstOrDefault();
            if (from == null)
                return this.Invoke(previors, current);
            var fromData = this.GetFromData(current);
            this.SrcMat = fromData.SrcMat;
            //this._srcFilePath = fromData.SrcFilePath;
            this.SrcFilePath = fromData.SrcFilePath;
            this._preMat = fromData.Mat;
            if (this._preMat.Empty())
                return this.OK("数据为空");
            if (this.UseReview)
            {
                this.Mat = this._preMat;
                this.RefreshMatToView();
                await Task.Delay(1500);
            }

            return this.Invoke(previors, current);
        }));
    }

    public override IFlowableResult Invoke(Part previors, Node current)
    {
        return this.Invoke();
        //IFlowableResult r = this.Invoke();
        //return r.State == FlowableResultState.Error ? r : base.Invoke(previors, current);
    }

    protected virtual IFlowableResult Invoke()
    {
        Thread.Sleep(500);
        return this.OK();
    }

    #region - NotifyPropertyChanged -

    private bool _isRefreshing;
    public virtual void DispatcherRaisePropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
    {
        RaisePropertyChanged(propertyName);
        if (_isRefreshing)
            return;
        _isRefreshing = true;
        Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Input, new Action(() =>
        {
            _isRefreshing = false;
            OnDispatcherPropertyChanged();
        }));
    }

    protected virtual void OnDispatcherPropertyChanged()
    {
        if (this._preMat == null)
            return;
        try
        {
            this.Invoke();
        }
        catch (Exception ex)
        {
            this.Message = ex.Message;
            IocLog.Instance?.Error(ex);
        }
    }

    #endregion
}
