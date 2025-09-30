// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.Diagram.Parts;

namespace H.Controls.Diagram;

public static class DockExtension
{
    public static Dock GetRevert(this Dock dock)
    {
        if (dock == Dock.Left) return Dock.Right;
        if (dock == Dock.Right) return Dock.Left;
        return dock == Dock.Top ? Dock.Bottom : Dock.Top;
    }

    public static Thickness GetTextMargin(this Dock dock, int offset = -30)
    {
        //return new Thickness(0, 0, 0, 0);
        if (dock == Dock.Left) return new Thickness(offset, 0, 0, 0);
        if (dock == Dock.Right) return new Thickness(0, 0, offset, 0);
        return dock == Dock.Top ? new Thickness(0, offset, 0, 0) : new Thickness(0, 0, 0, offset);
    }

    public static Vector GetNormalOffset(this Dock dock)
    {
        if (dock == Dock.Left)
            return new Vector(1, 0);
        if (dock == Dock.Right)
            return new Vector(-1, 0);
        if (dock == Dock.Top)
            return new Vector(0, 1);
        return new Vector(0, -1);
    }

    public static Vector GetOffset(this Dock dock, double spanwidth, double spanheight)
    {
        var nv = dock.GetNormalOffset();
        return new Vector(nv.X * spanwidth, nv.Y * spanheight);
    }

    public static string GetIcon(this IPortData portData)
    {
        string down = "\xE74B";
        string up = "\xE74A";
        string left = "\xE72B";
        string right = "\xE72A";
        if (portData.PortType == PortType.Both)
            return null;
        if (portData.Dock == Dock.Top)
            return portData.PortType == PortType.Input ? down : up;
        else if (portData.Dock == Dock.Bottom)
            return portData.PortType == PortType.Input ? up : down;
        else if (portData.Dock == Dock.Left)
            return portData.PortType == PortType.Input ? right : left;
        else if (portData.Dock == Dock.Right)
            return portData.PortType == PortType.Input ? left : right;
        return null;
    }

}
