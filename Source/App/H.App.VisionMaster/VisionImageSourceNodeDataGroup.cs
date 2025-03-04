using H.Controls.Diagram.Datas;
using H.Controls.Diagram.Presenter.DiagramDatas.Base;
using H.Extensions.FontIcon;
using H.Mvvm;
using H.Mvvm.Attributes;
using H.Mvvm.ViewModels.Base;
using System.Collections.Generic;

namespace H.App.VisionMaster;

[Icon(FontIcons.ImageExport)]
public class VisionImageSourceNodeDataGroup : NodeDataGroupBase
{
    public VisionImageSourceNodeDataGroup()
    {
        this.Name = "图像源";
        this.Order = 0;
    }

    protected override IEnumerable<INodeData> CreateNodeDatas()
    {
        yield return new VisionImageSourceStartNodeData();
    }
}
