// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Collections.Generic;

namespace H.Controls.Diagram
{
    public interface IDataSource<NodeDataType, LinkDataType>
    {
        List<NodeDataType> GetNodeType();

        List<LinkDataType> GetLinkType();
    }
}
