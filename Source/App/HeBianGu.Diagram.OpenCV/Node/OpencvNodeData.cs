using H.Controls.Diagram.Extension;
using H.Services.Logger;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Xml.Serialization;

namespace HeBianGu.Diagram.OpenCV
{
    public interface IOpenCVNodeData
    {
        Mat SrcMat { get; set; }
        Mat Mat { get; set; }
        string FilePath { get; set; }
    }

    public abstract class OpenCVNodeDataBase : ImageNodeDataBase, IOpenCVNodeData
    {
        [Browsable(false)]
        [XmlIgnore]
        public Mat Mat { get; set; }

        [Browsable(false)]
        [XmlIgnore]
        public Mat SrcMat { get; set; }

        private bool _useReview = true;
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
    }

    public abstract class OpenCVNodeData : OpenCVNodeDataBase
    {
        public override void LoadDefault()
        {
            base.LoadDefault();
            this.Width = 300;
            this.Height = 200;
        }

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
            var from = current.GetFromNodes().FirstOrDefault();
            if (from == null)
                return null;
            return from.GetContent<IOpenCVNodeData>();
        }

        protected void RefreshMatToView(Mat mat)
        {
            if (this.ImageSource == null)
            {
                System.Windows.Application.Current.Dispatcher.Invoke(() =>
                {
                    if (mat.Empty())
                        this.ImageSource = null;
                    else
                        this.ImageSource = mat?.ToWriteableBitmap();
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
                        if (mat.Empty())
                            this.ImageSource = null;
                        else
                            this.ImageSource = mat?.ToWriteableBitmap();
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
                var from = current.GetFromNodes().FirstOrDefault();
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
            var r = this.Refresh();
            if (r.State == FlowableResultState.Error)
                return r;
            return base.Invoke(previors, current);
        }

        protected virtual IFlowableResult Refresh()
        {
            return this.OK();
        }
    }

    public abstract class StartNodeDataBase : OpenCVNodeData
    {
        public StartNodeDataBase()
        {
            this.UseStart = true;
            this.Icon = "\xe843";
        }

        protected override ImageSource CreateImageSource()
        {
            this.FilePath = GetDataPath(this.GetImagePath());
            return CreateImage(this.FilePath);
        }

        protected virtual string GetImagePath()
        {
            return null;
        }

        public override IFlowableResult Invoke(Part previors, Node current)
        {
            this.Mat = new Mat(this.FilePath, ImreadModes.Color);
            this.SrcMat = this.Mat;
            return base.Invoke(previors, current);
        }
    }
}
