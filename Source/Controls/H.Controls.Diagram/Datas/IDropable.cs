// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using H.Controls.Diagram.Parts.Base;

namespace H.Controls.Diagram.Datas;

public interface IDropable
{
    /// <summary>
    /// 检查当前节点是否可以放下
    /// </summary>
    /// <param name="content"></param>
    /// <returns></returns>
    bool CanDrop(Part part, out string message);
}
