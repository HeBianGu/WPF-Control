global using System.Windows.Media;

namespace H.Controls.Diagram.Presenters.OpenCV.Base;

public abstract class StartNodeDataBase : ImageImportNodeDataBase, IImageImportNodeData
{
    public StartNodeDataBase()
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

    public override IFlowableResult Invoke(Part previors, Node current)
    {
        this.Mat = new Mat(this.SrcFilePath, ImreadModes.Color);
        this.SrcMat = this.Mat;
        return base.Invoke(previors, current);
    }
}
