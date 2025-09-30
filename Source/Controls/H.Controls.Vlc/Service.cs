// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Extensions.TypeConverter;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace H.Controls.Vlc
{
    public class Service : IService
    {

    }

    public interface IService
    {

    }
    [TypeConverter(typeof(DisplayEnumConverter))]
    public enum FullScreenType
    {
        [Display(Name = "鼠标悬停")]
        MouseOver,
        [Display(Name = "默认")]
        Default,
        [Display(Name = "无")]
        None,
        [Display(Name = "鹰眼")]
        ScrollViewerTransfor
    }
}
