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
    public enum FilterOperate
    {
        [Display(GroupName = "String,Double,Int,DateTime", Name = "等于")]
        Equals,
        [Display(GroupName = "String,Double,Int,DateTime", Name = "不等于")]
        UnEquals,
        [Display(GroupName = "String", Name = "包含")]
        Contain,
        [Display(GroupName = "String", Name = "不包含")]
        UnContain,
        [Display(GroupName = "String", Name = "已设置")]
        Setted,
        [Display(GroupName = "String", Name = "未设置")]
        Unset,
        [Display(GroupName = "String", Name = "从..开始")]
        StartWith,
        [Display(GroupName = "String", Name = "以..结束")]
        EndWith,
        [Display(GroupName = "Double,Int,DateTime", Name = "大于")]
        Greater,
        [Display(GroupName = "Double,Int,DateTime", Name = "小于")]
        Less,
        [Display(GroupName = "Double,Int,DateTime", Name = "大于等于")]
        GreaterAndEqual,
        [Display(GroupName = "Double,Int,DateTime", Name = "小于等于")]
        LessAndEqual,
        [Display(GroupName = "String", Name = "选择数据源")]
        SelectSource
    }
}
