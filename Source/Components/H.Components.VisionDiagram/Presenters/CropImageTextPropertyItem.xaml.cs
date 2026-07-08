// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.Form.PropertyItems;
using H.Mvvm.Commands;

namespace H.Components.VisionDiagram.Presenters;
public class CropImageTextPropertyItem : TextPropertyItem
{
    public CropImageTextPropertyItem(PropertyInfo property, object obj) : base(property, obj)
    {
        var ta = property.GetCustomAttribute<PropertyTextBoxStyleAttribute>();
        if (ta != null)
        {
            this.TextWrapping = ta.TextWrapping;
            this.UseClear = ta.UseClear;

            UnitAttribute unit = property.GetCustomAttribute<UnitAttribute>();
            if (unit != null)
                this.Unit = unit.Unit;
        }
    }

    public RelayCommand DeleteCommand => new RelayCommand(x =>
    {
        this.Value = null;
    });
}

