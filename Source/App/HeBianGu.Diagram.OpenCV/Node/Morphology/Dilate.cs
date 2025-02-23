namespace HeBianGu.Diagram.OpenCV
{
    
    
    [Display(Name = "膨胀", GroupName = "形态学", Description = "使边缘更清晰", Order = 0)]
    public class Dilate : MorphologyBase
    {
        protected override MorphTypes GetMorphType()
        {
            return MorphTypes.Dilate;
        }
        //private Point? _anchor = null;
        //[Display(Name = "Anchor", GroupName = "数据")]
        //public Point? Anchor
        //{
        //    get { return _anchor; }
        //    set
        //    {
        //        _anchor = value;
        //        RaisePropertyChanged();
        //    }
        //}



        //private int _iterations = 1;
        //[Display(Name = "Iterations", GroupName = "数据")]
        //public int Iterations
        //{
        //    get { return _iterations; }
        //    set
        //    {
        //        _iterations = value;
        //        RaisePropertyChanged();
        //    }
        //}


        //private BorderTypes _borderTypes = BorderTypes.Constant;
        //[Display(Name = "BorderType", GroupName = "数据")]
        //public BorderTypes BorderType
        //{
        //    get { return _borderTypes; }
        //    set
        //    {
        //        _borderTypes = value;
        //        RaisePropertyChanged();
        //    }
        //}

        //private bool _useKernel;
        //[Display(Name = "UseKernel", GroupName = "数据")]
        //public bool UseKernel
        //{
        //    get { return _useKernel; }
        //    set
        //    {
        //        _useKernel = value;
        //        RaisePropertyChanged();
        //    }
        //}

        //private Int32Collection _kernelValues = new Int32Collection(new int[] { 0, 1, 0, 1, 1, 1, 0, 1, 0 });
        //[Display(Name = "KernelValues", GroupName = "数据")]
        //public Int32Collection KernelValues
        //{
        //    get { return _kernelValues; }
        //    set
        //    {
        //        _kernelValues = value;
        //        RaisePropertyChanged();
        //    }
        //}


        //private int _kernelRows = 3;
        //[Display(Name = "KernelRows", GroupName = "数据")]
        //public int KernelRows
        //{
        //    get { return _kernelRows; }
        //    set
        //    {
        //        _kernelRows = value;
        //        RaisePropertyChanged();
        //    }
        //}


        //private int _kernelCols = 3;
        //[Display(Name = "KernelCols", GroupName = "数据")]
        //public int KernelCols
        //{
        //    get { return _kernelCols; }
        //    set
        //    {
        //        _kernelCols = value;
        //        RaisePropertyChanged();
        //    }
        //}



        //public override IFlowableResult Invoke(Part previors, Node current)
        //{
        //    var src = GetFromMat(current);
        //    var dilate = new Mat();

        //    if (UseKernel)
        //    {
        //        byte[] kernelValues = KernelValues.Select(x => (byte)x).ToArray(); // cross (+)
        //        var kernel = new Mat(KernelRows, KernelCols, MatType.CV_8UC1, kernelValues);
        //        Cv2.Dilate(src, dilate, kernel, Anchor, Iterations, BorderType);
        //    }
        //    else
        //    {
        //        Cv2.Dilate(src, dilate, null, Anchor, Iterations, BorderType);
        //    }

        //    Mat = dilate;
        //    RefreshMatToView();
        //    return base.Invoke(previors, current);
        //}
    }
}
