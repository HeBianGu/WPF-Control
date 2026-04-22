// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.ComponentModel.DataAnnotations.Schema;

namespace H.App.AIDI.DB;
public class fm_dd_image : fm_dd_file
{
    [Browsable(false)]
    [ReadOnly(true)]
    [Display(Name = "宽度")]
    public int PixelWidth { get; set; }

    [Browsable(false)]
    [ReadOnly(true)]
    [Display(Name = "高度")]
    public int PixelHeight { get; set; }

    [Display(Name = "简介")]
    public string Introduction { get; set; }

    /// <summary> 宽高比 1920x1080 </summary>
    [Display(Name = "宽高比")]
    [NotMapped]
    public string Aspect => $"{this.PixelWidth}x{this.PixelHeight}";

    [Browsable(false)]
    [Display(Name = "标注")]
    public virtual ObservableCollection<fm_dd_label> Labels { get; set; } = new ObservableCollection<fm_dd_label>();

    [NotMapped]
    [ReadOnly(true)]
    [Display(Name = "标注数量")]
    public int LabelsCount => this.Labels.Count;

    private DatasetType _DatasetType;
    [Display(Name = "数据集类型")]
    public DatasetType DatasetType
    {
        get { return _DatasetType; }
        set
        {
            _DatasetType = value;
            RaisePropertyChanged();
        }
    }
}

[TypeConverter(typeof(DisplayEnumConverter))]
public enum DatasetType
{
    [Display(Name = "训练集")]
    Train,
    [Display(Name = "测试集")]
    Test,
    [Display(Name = "验证集")]
    Validation
}