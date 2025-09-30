// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

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

    public static class ConditionOperates
    {
        public static ConditionOperate All = ConditionOperate.All;
        public static ConditionOperate Any = ConditionOperate.Any;
        public static ConditionOperate AnyNot = ConditionOperate.AnyNot;
        public static ConditionOperate None = ConditionOperate.None;
    }
}
