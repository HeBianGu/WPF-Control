using H.Controls.Diagram.Flowables;
using H.Mvvm;
using System;
using System.ComponentModel.DataAnnotations;

namespace H.App.VisionMaster;

public class VisionMessage : BindableBase, IVisionMessage
{
    [Display(Name ="执行序号")]
    public int Index { get; set; }
    [Display(Name = "时间")]
    public TimeSpan TimeSpan { get; set; }
    [Display(Name = "模块")]
    public string Type { get; set; }
    [Display(Name = "数据")]
    public string Message { get; set; }
    [Display(Name = "状态")]
    public FlowableState State { get; set; }
}
