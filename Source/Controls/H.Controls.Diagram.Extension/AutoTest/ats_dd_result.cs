// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Extensions.DataBase;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace H.Controls.Diagram.Extension.AutoTest;

public class ats_dd_result : DbModelBase
{
    [Display(Name = "批次号")]
    public string BatchCode { get; set; } = DateTime.Now.ToString("yyyyMMddHHmmssfff");

    [Display(Name = "日期")]
    public DateTime Time { get; set; } = DateTime.Now;

    [Browsable(false)]
    [Display(Name = "预览图")]
    public string Image { get; set; }

    [Browsable(false)]
    [Display(Name = "详情数据")]
    public string Diagram { get; set; }

    [Display(Name = "测量结果")]
    public string Result { get; set; }

    [Display(Name = "不良描述")]
    public string Description { get; set; }

    [Display(Name = "备注")]
    public string Mark { get; set; }

    [Display(Name = "操作员")]
    public string User { get; set; }
}
