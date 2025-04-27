global using System.Windows.Media;
using H.Common.Interfaces;

namespace H.Controls.Diagram.Presenters.OpenCV.Base;

public interface IOpenCVImageNodeData : INodeData, IOrderable
{

}

public abstract class OpenCVImageNodeDataBase : SrcImageNodeDataBase, IOpenCVImageNodeData
{
    public OpenCVImageNodeDataBase()
    {
        this.UseStart = true;
        this.Icon = "\xe843";
        this.SrcFilePath = this.GetImagePath().ToDataPath();
    }

    protected virtual string GetImagePath()
    {
        return null;
    }

}
