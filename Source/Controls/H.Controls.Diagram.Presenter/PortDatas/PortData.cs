// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.Diagram.Presenter.PortDatas;

public abstract class PortData : PortDataBase, IDropable, ILinkDataCreator
{
    public PortData()
    {

    }

    public PortData(string nodeID) : base(nodeID)
    {

    }

    public virtual bool CanDrop(Part part, out string message)
    {
        message = null;
        if (part.Content is IPortData port)
        {
            if (port.PortType == PortType.OutPut)
            {
                message = "不能连接输出端口";
                return false;
            }

            if (port.NodeID == this.NodeID)
            {
                message = "不能连接同一个节点";
                return false;
            }

        }

        if (part.Content?.GetType() != this.GetType())
        {
            message = $"不是<{this.Name}>类型端口";
            return false;
        }

        return true;
    }

    public virtual ILinkData CreateLinkData()
    {
        return new DefaultLinkData() { FromNodeID = this.NodeID, FromPortID = this.ID };
    }
}
