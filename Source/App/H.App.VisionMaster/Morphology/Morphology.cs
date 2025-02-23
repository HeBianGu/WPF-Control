using H.Controls.Diagram;
using H.Mvvm;
using System.ComponentModel.DataAnnotations;

namespace H.App.VisionMaster.Morphology;

public interface IMorphology : INodeData, IDisplayBindable
{

}
[Icon("\xE722")]
[Display(Name = "滤波、降噪，模糊")]
public abstract class MorphologyBase : ActionNodeDataBase, IMorphology
{

}

[Display(Name = "开运算", GroupName = "形态学", Description = "腐蚀 + 膨胀 ，去除大图形外的小图形", Order = 0)]
public class Open : MorphologyBase
{

}

[Display(Name = "顶帽", GroupName = "形态学", Description = " 原图 - 开运算  ，大图形外的小图形", Order = 0)]
public class TopHat : MorphologyBase
{

}
[Display(Name = "黑帽", GroupName = "形态学", Description = " 原图 - 闭运算  ，大图形内的小图形", Order = 0)]
public class BlackHat : MorphologyBase
{

}
[Display(Name = "闭运算", GroupName = "形态学", Description = " 膨胀 + 腐蚀  ，去除大图形内的小图形", Order = 0)]
public class Close : MorphologyBase
{

}
[Display(Name = "膨胀", GroupName = "形态学", Description = "使边缘更清晰", Order = 0)]
public class Dilate : MorphologyBase
{

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

[Display(Name = "腐蚀", GroupName = "形态学", Description = "去除散点", Order = 0)]
public class Erode : MorphologyBase
{
}
[Display(Name = "梯度", GroupName = "形态学", Description = " 原图 - 腐蚀  ，求图形的边缘", Order = 0)]
public class Gradient : MorphologyBase
{

}
