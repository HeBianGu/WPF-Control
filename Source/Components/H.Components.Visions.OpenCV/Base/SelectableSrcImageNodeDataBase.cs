// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using H.Controls.Form.Attributes;
global using H.Controls.Form.PropertyItem.ComboBoxPropertyItems;

namespace H.Components.Visions.OpenCV.Base;
public abstract class SelectableSrcImageNodeDataBase : ResultPresenterNodeDataBase
{
    private ISrcFilesNodeData _selectedSrcNodeData;
    [GetMethodNameSource(nameof(GetSelectableSrcNodeDatas))]
    [PropertyItem(typeof(ComboBoxPropertyItem))]
    [Display(Name = "选择图像源", GroupName = "流程控制")]
    public ISrcFilesNodeData SelectedFromNodeData
    {
        get { return _selectedSrcNodeData; }
        set
        {
            _selectedSrcNodeData = value;
            RaisePropertyChanged();
        }
    }

    public IEnumerable<INodeData> GetSelectableSrcNodeDatas()
    {
        return this.FromNodeDatas.OfType<ISrcFilesNodeData>();
    }

}