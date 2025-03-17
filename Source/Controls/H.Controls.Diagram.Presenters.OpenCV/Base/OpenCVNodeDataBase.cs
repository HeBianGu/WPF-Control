using H.Services.Common;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using System.Xml.Serialization;

namespace H.Controls.Diagram.Presenters.OpenCV.Base;
public abstract class OpenCVNodeDataBase : ActionNodeDataBase, IOpenCVNodeData, IFilePathable
{
    ~OpenCVNodeDataBase()
    {
        this.Mat?.Dispose();
        this.SrcMat?.Dispose();
        this.PreviourMat?.Dispose();
    }
    [JsonIgnore]
    [Browsable(false)]
    [XmlIgnore]
    public Mat Mat { get; set; }

    [JsonIgnore]
    [Browsable(false)]
    [XmlIgnore]
    public Mat SrcMat { get; set; }

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


    private string _srcFilePath;
    //[JsonIgnore]
    [Browsable(false)]
    //[Display(Name = "源文件地址", GroupName = "数据")]
    //[PropertyItem(typeof(OpenFileDialogPropertyItem))]
    public virtual string SrcFilePath
    {
        get { return _srcFilePath; }
        set
        {
            _srcFilePath = value;
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

    protected virtual async Task<IFlowableResult> BeforeInvokeAsync(IFlowablePartData previors, IFlowableDiagramData current)
    {
        return await Task.FromResult(this.OK());
    }

    protected IOpenCVNodeData GetFromData(IFlowableDiagramData current)
    {
        return this.GetFromNodeDatas(current).OfType<IOpenCVNodeData>().FirstOrDefault();
    }

    private Mat _preMat;
    protected Mat PreviourMat => this._preMat;
    //private string _srcFilePath;
    //protected string SrcFilePath => this._srcFilePath;

    public override async Task<IFlowableResult> InvokeAsync(IFlowablePartData previors, IFlowableDiagramData current)
    {
        return await Task.Run((async () =>
        {
            var fromData = this.GetFromData(current);
            if (fromData == null)
                return this.Invoke(previors, current);
            this.SrcMat = fromData.SrcMat;
            //this._srcFilePath = fromData.SrcFilePath;
            this.SrcFilePath = fromData.SrcFilePath;
            this._preMat = fromData.Mat;
            if (this._preMat == null || this._preMat.Empty())
                return this.Error("传入图像数据为空");
            if (this.UseReview)
            {
                this.Mat = this._preMat;
                this.UpdateMatToView();
                await Task.Delay(this.PreviewMillisecondsDelay);
            }

            return this.Invoke(previors, current);
        }));
    }

    public override IFlowableResult Invoke(IFlowablePartData previors, IFlowableDiagramData current)
    {
        return this.Invoke();
        //IFlowableResult r = this.Invoke();
        //return r.State == FlowableResultState.Error ? r : base.Invoke(previors, current);
    }

    protected virtual IFlowableResult Invoke()
    {
        Thread.Sleep(this.InvokeMillisecondsDelay);
        return this.OK();
    }

    protected void UpdateMatToView(Mat mat)
    {
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
