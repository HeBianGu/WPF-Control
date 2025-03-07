global using System.Windows.Media;

namespace H.Controls.Diagram.Presenters.OpenCV.Base;

public interface IOpenCVImageNodeData : INodeData, IOrderable
{

}

public abstract class OpenCVImageNodeDataBase : ImageImportNodeDataBase, IOpenCVImageNodeData
{
    public OpenCVImageNodeDataBase()
    {
        this.UseStart = true;
        this.Icon = "\xe843";
        this.SrcFilePath = GetDataPath(this.GetImagePath());
    }

    //protected override ImageSource CreateImageSource()
    //{
    //    this.SrcFilePath = GetDataPath(this.GetImagePath());
    //    return CreateImage(this.SrcFilePath);
    //}

    protected virtual string GetImagePath()
    {
        return null;
    }

}
