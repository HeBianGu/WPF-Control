global using System.Windows.Media;

namespace H.Controls.Diagram.Extensions.OpenCV.Base;

public abstract class StartNodeDataBase : ImageImportNodeDataBase, IImageImportNodeData
{
    public StartNodeDataBase()
    {
        this.UseStart = true;
        this.Icon = "\xe843";
    }

    protected override ImageSource CreateImageSource()
    {
        this.FilePath = GetDataPath(this.GetImagePath());
        return CreateImage(this.FilePath);
    }

    protected virtual string GetImagePath()
    {
        return null;
    }

    public override IFlowableResult Invoke(Part previors, Node current)
    {
        this.Mat = new Mat(this.FilePath, ImreadModes.Color);
        this.SrcMat = this.Mat;
        return base.Invoke(previors, current);
    }
}
