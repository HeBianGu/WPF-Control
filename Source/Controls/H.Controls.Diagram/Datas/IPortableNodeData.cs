using System.Collections.Generic;

namespace H.Controls.Diagram.Datas;

public interface IPortableNodeData : ILinkDataCreator
{
    public List<IPortData> PortDatas { get; set; }
}
