namespace H.Controls.Diagram.Presenters.OpenCV.Base;

public interface IOpenCVNodeData
{
    Mat SrcMat { get; set; }
    Mat Mat { get; set; }
    string FilePath { get; set; }
}
