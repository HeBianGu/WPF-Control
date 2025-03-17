// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using H.Controls.Diagram.Parts.Base;
using System;
using System.Windows;
using System.Windows.Controls;

namespace H.Controls.Diagram.Datas;

/// <summary>
/// 默认端口要显示的效果
/// </summary>
public class DefaultPortData : IPortData
{
    public DefaultPortData()
    {

    }
    public DefaultPortData(string nodeID)
    {
        this.NodeID = nodeID;
    }

    public string ID { get; set; } = Guid.NewGuid().ToString();

    public string NodeID { get; set; }

    public string Name { get; set; }


    public Dock Dock { get; set; }

    public PortType PortType { get; set; }

    public Thickness PortMargin { get; set; } = new Thickness(0, 0, 0, 0);

    public virtual void ApplayStyleTo(IPortData node)
    {

    }

    public bool CanDrop(Part part, out string message)
    {
        message = null;
        return false;
    }

    public ILinkData CreateLinkData()
    {
        return null;
    }

    public void InitLink(Link link)
    {

    }
}
