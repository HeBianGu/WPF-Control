using H.Controls.Diagram;
using H.Mvvm;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace H.App.VisionMaster;

[Icon("\xE722")]
[Display(Name = "采集", GroupName = "设置输入图像", Order = 0)]
public class ImageDataGroup : NodeDataGroupBase
{
    protected override IEnumerable<INodeData> CreateNodeDatas()
    {
        return typeof(IImageImportNodeData).Assembly.GetInstances<IImageImportNodeData>().OrderBy(x => x.Order); ;
    }
}

[Icon("\xE722")]
[Display(Name = "滤波", GroupName = "对图像进行滤波，降噪，模糊处理", Order = 2)]
public class FilterDataGroup : NodeDataGroupBase
{
    protected override IEnumerable<INodeData> CreateNodeDatas()
    {
        return typeof(IFilterActionNodeData).Assembly.GetInstances<IFilterActionNodeData>().OrderBy(x => x.Order); ;
    }
}

