using H.Mvvm;
using System.ComponentModel.DataAnnotations;

namespace H.App.VisionMaster;

[Icon("\xE722")]
[Display(Name = "图像源")]
public class SourceImageNodeData : ImageImportNodeDataBase
{
    public SourceImageNodeData()
    {
        this.ImagePath = "C:\\Users\\Public\\Pictures\\Sample Pictures\\Desert.jpg";
        this.ImageName = "Desert";
        this.ImageDescription = "Desert Image";
    }
}

[Icon("\xE722")]
[Display(Name = "图像采集")]
public class CaptureImageNodeData : ImageImportNodeDataBase
{
    public CaptureImageNodeData()
    {
        this.ImagePath = "C:\\Users\\Public\\Pictures\\Sample Pictures\\Desert.jpg";
        this.ImageName = "Desert";
        this.ImageDescription = "Desert Image";
    }
}

[Icon("\xE722")]
[Display(Name = "输出图像")]
public class OutputImageNodeData : ImageImportNodeDataBase
{
    public OutputImageNodeData()
    {
        this.ImagePath = "C:\\Users\\Public\\Pictures\\Sample Pictures\\Desert.jpg";
        this.ImageName = "Desert";
        this.ImageDescription = "Desert Image";
    }
}

[Icon("\xE722")]
[Display(Name = "缓存图像")]
public class CatcheOutputImageNodeData : ImageImportNodeDataBase
{
    public CatcheOutputImageNodeData()
    {
        this.ImagePath = "C:\\Users\\Public\\Pictures\\Sample Pictures\\Desert.jpg";
        this.ImageName = "Desert";
        this.ImageDescription = "Desert Image";
    }
}
