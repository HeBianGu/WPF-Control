namespace H.Controls.Diagram.Extensions.OpenCV.Base;

public interface IOpenCVNodeData
{
    Mat SrcMat { get; set; }
    Mat Mat { get; set; }
    string FilePath { get; set; }
}
