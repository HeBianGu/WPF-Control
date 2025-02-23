using System.Windows.Threading;

namespace H.Controls.Diagram.Extensions.OpenCV;
public abstract class MorphologyActionNodeDataBase : OpenCVNodeData, IMorphology
{
    public override void LoadDefault()
    {
        base.LoadDefault();
        this.KernelValues = new Int32Collection(new int[] { 0, 1, 0, 1, 1, 1, 0, 1, 0 });
    }
    private Point? _anchor = null;
    [DefaultValue(null)]
    [Display(Name = "Anchor", GroupName = "数据")]
    public Point? Anchor
    {
        get { return _anchor; }
        set
        {
            _anchor = value;
            DispatcherRaisePropertyChanged();
        }
    }

    private int _iterations = 1;
    [DefaultValue(1)]
    [Display(Name = "Iterations", GroupName = "数据")]
    public int Iterations
    {
        get { return _iterations; }
        set
        {
            _iterations = value;
            DispatcherRaisePropertyChanged();
        }
    }


    private BorderTypes _borderTypes = BorderTypes.Constant;
    [DefaultValue(BorderTypes.Constant)]
    [Display(Name = "BorderType", GroupName = "数据")]
    public BorderTypes BorderType
    {
        get { return _borderTypes; }
        set
        {
            _borderTypes = value;
            DispatcherRaisePropertyChanged();
        }
    }

    private bool _useKernel = true;
    [DefaultValue(3)]
    [Display(Name = "UseKernel", GroupName = "数据")]
    public bool UseKernel
    {
        get { return _useKernel; }
        set
        {
            _useKernel = value;
            DispatcherRaisePropertyChanged();
        }
    }

    private Int32Collection _kernelValues = new Int32Collection(new int[] { 0, 1, 0, 1, 1, 1, 0, 1, 0 });
    [Display(Name = "KernelValues", GroupName = "数据")]
    public Int32Collection KernelValues
    {
        get { return _kernelValues; }
        set
        {
            _kernelValues = value;
            DispatcherRaisePropertyChanged();
        }
    }

    private int _kernelRows = 3;
    [DefaultValue(3)]
    [Display(Name = "KernelRows", GroupName = "数据")]
    public int KernelRows
    {
        get { return _kernelRows; }
        set
        {
            _kernelRows = value;
            DispatcherRaisePropertyChanged();
        }
    }


    private int _kernelCols = 3;
    [DefaultValue(3)]
    [Display(Name = "KernelCols", GroupName = "数据")]
    public int KernelCols
    {
        get { return _kernelCols; }
        set
        {
            _kernelCols = value;
            DispatcherRaisePropertyChanged();
        }
    }


    protected abstract MorphTypes GetMorphType();

    protected override IFlowableResult Refresh()
    {
        var src = this._preMat;
        var dst = new Mat();

        if (this.UseKernel)
        {
            byte[] kernelValues = this.KernelValues.GetDispatcherValue(x => x.Select(x => (byte)x).ToArray()); // cross (+)
            var kernel = new Mat(KernelRows, KernelCols, MatType.CV_8UC1, kernelValues);
            Cv2.MorphologyEx(src, dst, this.GetMorphType(), kernel, Anchor, Iterations, BorderType);
        }
        else
        {
            Cv2.MorphologyEx(src, dst, this.GetMorphType(), null, Anchor, Iterations, BorderType);
        }

        Mat = dst;
        RefreshMatToView();
        return base.Refresh();
    }
}
