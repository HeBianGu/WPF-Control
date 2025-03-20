// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control
// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control
namespace H.Controls.Diagram.Datas;

public interface ILinkData : IPartData
{
    string FromNodeID { get; set; }
    string ToNodeID { get; set; }
    string FromPortID { get; set; }
    string ToPortID { get; set; }
}
