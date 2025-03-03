using H.Controls.Diagram.Presenters.OpenCV.Base;
using H.Extensions.FontIcon;
using H.Mvvm;
using System.ComponentModel.DataAnnotations;

namespace H.App.VisionMaster;

[Icon(FontIcons.ImageExport)]
[Display(Name = "图像源")]
public class VisionImageSourceStartNodeData : StartNodeDataBase, IVisionImageSourceStartNodeData
{

}
