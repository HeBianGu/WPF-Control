// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace H.Controls.Diagram
{
    public interface INodeData : IData
    {
        string ID { get; set; }

        Point Location { get; set; }

        void ApplayStyleTo(INodeData node);
    }
}
