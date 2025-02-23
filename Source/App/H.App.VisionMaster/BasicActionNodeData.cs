using H.Controls.Diagram;
using H.Mvvm;
using System.ComponentModel.DataAnnotations;

namespace H.App.VisionMaster;

public interface IBasicActionNodeData : INodeData, IDisplayBindable
{

}
[Icon("\xE722")]
[Display(Name = "图像源")]
public abstract class BasicActionNodeDataBase : ActionNodeDataBase, IBasicActionNodeData
{

}

[Icon("\xE722")]
[Display(Name = "反转黑白", Order = 1)]
public class RevertActionNodeData : BasicActionNodeDataBase
{

}

[Icon("\xE722")]
[Display(Name = "色彩变换", Order = 0)]
public class ColorTransformActionNodeData : BasicActionNodeDataBase
{

}

