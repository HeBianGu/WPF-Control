namespace H.Controls.Diagram.Presenters.OpenCV.Base;

public interface IVideoCaptureNodeData : INodeData, IOrderable
{

}

public abstract class VideoCaptureNodeDataBase : ImageImportNodeDataBase, IVideoCaptureNodeData
{

}

