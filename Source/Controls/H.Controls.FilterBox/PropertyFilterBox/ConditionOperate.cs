// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using H.Extensions.TypeConverter;

namespace H.Controls.FilterBox
{
    [TypeConverter(typeof(DisplayEnumConverter))]
    public enum ConditionOperate
    {
        [Display(Name = "满足所有条件")]
        All = 0,
        [Display(Name = "满足任一条件")]
        Any,
        [Display(Name = "任一条件不满足")]
        AnyNot,
        [Display(Name = "全部不满足条件")]
        None
    }
}
