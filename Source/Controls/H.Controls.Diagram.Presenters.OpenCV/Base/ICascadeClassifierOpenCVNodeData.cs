global using H.Mvvm;
namespace H.Controls.Diagram.Presenters.OpenCV.Base;

public interface ICascadeClassifierOpenCVNodeData : INodeData, IDisplayBindable
{
    HaarDetectionTypes Flags { get; set; }
    Size? MaxSize { get; set; }
    int MinNeighbors { get; set; }
    Size? MinSize { get; set; }
    double ScaleFactor { get; set; }
}