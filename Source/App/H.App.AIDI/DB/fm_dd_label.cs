// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.App.AIDI.DB;

public class fm_dd_label : DbModelBase
{
    [Display(Name = "标注名称")]
    public string LabelName { get; set; }
    [Display(Name = "标注颜色")]
    public Color LabelColor { get; set; }
    [Display(Name = "标注描述")]
    public string LabelDescription { get; set; }
    [Display(Name = "范围")]
    public Rect BoundingBox { get; set; }

    public string ImageID { get; set; }
    public virtual fm_dd_image Image { get; set; }
}
