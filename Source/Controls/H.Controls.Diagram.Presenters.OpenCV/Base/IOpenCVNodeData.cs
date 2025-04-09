using H.Controls.Diagram.Presenter.Flowables;

namespace H.Controls.Diagram.Presenters.OpenCV.Base;

public interface IOpenCVNodeData : IFlowableNodeData
{
    Mat Mat { get; }
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
