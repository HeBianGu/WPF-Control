// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.Form.Attributes;
using H.Controls.Form.PropertyItem.Attribute;
using H.Controls.Form.PropertyItem.ComboBoxPropertyItems;
using H.Controls.ShapeBox.Base;
using H.Extensions.Setting;

namespace H.Controls.ShapeBox.Settables;
public interface IShapeStyleSetting
{
    R Create<R>() where R : new();
    void SaveTo(object r);
    void LoadBy(object r);
}

public interface IShapeStyleOption
{
    Brush Fill { get; set; }
    Brush Stroke { get; set; }
    double StrokeThickness { get; set; }
}

public class ShapeStyleSetting<T> : Settable<T>, IShapeStyleSetting, IShapeStyleOption where T : new()
{
    [GetHightlightBrushesSource]
    [PropertyItem(typeof(BrushComboBoxPropertyItem))]
    [DefaultValue(null)]
    [Display(Name = "线条颜色", GroupName = ShapePropertyGroupNames.StyleGroup)]
    public Brush Stroke { get; set; }
    [DefaultValue(-1)]
    [Display(Name = "线条粗细", GroupName = ShapePropertyGroupNames.StyleGroup)]
    public double StrokeThickness { get; set; } = -1;

    [GetStandardBrushesSourceAttribute]
    [PropertyItem(typeof(BrushComboBoxPropertyItem))]
    [DefaultValue(null)]
    [Display(Name = "填充色", GroupName = ShapePropertyGroupNames.StyleGroup)]
    public Brush Fill { get; set; }

    public R Create<R>() where R : new()
    {
        var result = new R();
        this.SaveTo(result);
        return result;
    }

    public void SaveTo(object r)
    {
        if (r == null)
            return;
        var ps = this.GetType().GetProperties();
        var rps = r.GetType().GetProperties();
        foreach (var p in ps)
        {
            var rp = rps.FirstOrDefault(x => x.Name == p.Name && x.PropertyType == p.PropertyType && x.CanWrite);
            rp?.SetValue(r, p.GetValue(this));
        }
    }

    public void LoadBy(object r)
    {
        var ps = r.GetType().GetProperties();
        var rps = this.GetType().GetProperties();
        foreach (var p in ps)
        {
            var rp = rps.FirstOrDefault(x => x.Name == p.Name && x.PropertyType == p.PropertyType && x.CanWrite);
            rp?.SetValue(this, p.GetValue(r));
        }
    }
}
