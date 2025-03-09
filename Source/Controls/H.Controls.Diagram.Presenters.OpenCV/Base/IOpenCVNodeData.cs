namespace H.Controls.Diagram.Presenters.OpenCV.Base;

public interface IOpenCVNodeData : IFlowableNodeData
{
    Mat SrcMat { get; set; }
    Mat Mat { get; set; }
    string SrcFilePath { get; set; }
    ImageSource ImageSource { get; set; }
}

public static class MatExtension
{
    public static ImageSource ToImageSource(this Mat mat)
    {
        if (mat?.IsDisposed != false)
            return null;
        return mat.ToBitmapSource();
    }
}
