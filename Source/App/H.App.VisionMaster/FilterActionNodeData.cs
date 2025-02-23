using H.Controls.Diagram;
using H.Mvvm;
using System.ComponentModel.DataAnnotations;

namespace H.App.VisionMaster;

public interface IFilterActionNodeData : INodeData, IDisplayBindable
{

}
[Icon("\xE722")]
[Display(Name = "滤波、降噪，模糊")]
public abstract class FilterActionNodeDataBase : ActionNodeDataBase, IFilterActionNodeData
{

}

[Icon("\xE722")]
[Display(Name = "基础滤波", Order = 1)]
public class BasicFilterActionNodeData : FilterActionNodeDataBase
{

}

[Icon("\xE722")]
[Display(Name = "细节增强", Order = 0)]
public class EnhancementActionNodeData : FilterActionNodeDataBase
{

}

