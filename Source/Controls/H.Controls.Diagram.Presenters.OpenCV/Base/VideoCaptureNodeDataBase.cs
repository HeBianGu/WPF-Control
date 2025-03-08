namespace H.Controls.Diagram.Presenters.OpenCV.Base;

public interface IVideoCaptureNodeData : INodeData, IOrderable
{

}

[Icon(FontIcons.Video)]
public abstract class VideoCaptureNodeDataBase : ImageImportNodeDataBase, IVideoCaptureNodeData
{

}

