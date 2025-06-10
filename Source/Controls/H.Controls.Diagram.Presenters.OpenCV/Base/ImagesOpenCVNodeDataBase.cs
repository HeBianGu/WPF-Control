// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Text.Json.Serialization;
using H.Controls.Form.PropertyItem.Attribute.SourcePropertyItem;
using H.Controls.Form.PropertyItem.ComboBoxPropertyItems;

namespace H.Controls.Diagram.Presenters.OpenCV.Base;

public abstract class ImagesOpenCVNodeDataBase : OpenCVNodeDataBase
{
    //[ReadOnly(true)]
    //[Display(Name = "图像源区域", GroupName = "图像源")]
    //public Int32Rect? SrcRect
    //{
    //    get
    //    {
    //        var image = this.SrcImage;
    //        if (image == null)
    //            return null;
    //        image.GetImageSize(out int width, out int height);
    //        return new Int32Rect(0, 0, width, height);
    //    }
    //}


    private ISrcImageNodeData _selectedSrcImageNodeData;
    [PropertyNameSourcePropertyItem(typeof(ComboBoxPropertyItem), nameof(SrcImageNodeDatas))]
    [Display(Name = "输入源", GroupName = "基本参数")]
    public ISrcImageNodeData SelectedSrcImageNodeData
    {
        get { return _selectedSrcImageNodeData; }
        set
        {
            _selectedSrcImageNodeData = value;
            RaisePropertyChanged();
        }
    }

    [JsonIgnore]
    [Browsable(false)]
    public IEnumerable<INodeData> SrcImageNodeDatas
    {
        get
        {
            return this.AllFromNodeDatas;
        }
    }


}
