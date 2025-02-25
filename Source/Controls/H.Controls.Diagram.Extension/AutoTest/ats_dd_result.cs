using H.Extensions.DataBase;
using System;
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
