// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

namespace H.Controls.Diagram
{
    public interface ILinkData : IData
    {
        string FromNodeID { get; set; }

        string ToNodeID { get; set; }

        string FromPortID { get; set; }

        string ToPortID { get; set; }

        void ApplayStyleTo(ILinkData node);
    }
}
