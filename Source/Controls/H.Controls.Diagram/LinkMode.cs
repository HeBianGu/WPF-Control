// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control



using H.Extensions.TypeConverter;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace H.Controls.Diagram;

[TypeConverter(typeof(DisplayEnumConverter))]
public enum LinkMode
{
    [Display(Name = "节点")]
    Node = 0,
    [Display(Name = "端口")]
    Port
}
