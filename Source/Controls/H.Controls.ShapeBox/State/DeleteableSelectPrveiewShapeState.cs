// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Extensions.Mvvm.Commands;
using System.Runtime.CompilerServices;

namespace H.Controls.ShapeBox.State;

public class DeleteableSelectPrveiewShapeState : SelectablePrveiewShapeState
{
    private IDeletableShapes _deletableShapes;
    public DeleteableSelectPrveiewShapeState(IDeletableShapes deletableShapes)
    {
        this._deletableShapes = deletableShapes;
    }
    public bool UseDelete { get; set; } = true;
    public bool UseClear { get; set; } = true;

    [Icon(FontIcons.Delete)]
    [Display(Name = "删除选中形状", GroupName = "右键菜单", Description = "删除当前选中的形状")]
    public DisplayCommand DeleteSelectShapeCommand => new DisplayCommand(x =>
    {
        if (this.View is ISelectShapeBox shapeBox)
        {
            var selectedShapes = shapeBox.SelectedShapes;
            if (selectedShapes == null || selectedShapes.Count() == 0)
                return;
            foreach (var item in selectedShapes)
                _deletableShapes.DeleteShapes(item);
        }
    }, x =>
    {
        if (!this.UseSelect)
            return false;
        if (!this.UseDelete)
            return false;
        if (this.View is ISelectShapeBox shapeBox)
            return shapeBox.SelectedShapes.Any();
        return false;
    });

    [Icon(FontIcons.Delete)]
    [Display(Name = "清空所有形状", GroupName = "右键菜单", Description = "清空所有形状")]
    public DisplayCommand ClearShapesCommand => new DisplayCommand(x =>
    {
        _deletableShapes.DeleteShapes(this.GetShapes().ToArray());
    }, x =>
    {
        if (!this.UseDelete)
            return false;
        return this.GetShapes().Count() > 0;
    });
}
