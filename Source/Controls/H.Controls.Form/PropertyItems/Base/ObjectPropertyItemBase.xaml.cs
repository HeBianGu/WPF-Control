// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace H.Controls.Form.PropertyItems.Base;


/// <summary> 类型基类 </summary>
public abstract class ObjectPropertyItemBase : DisplayBindableBase, IPropertyItem, IValueChangeable
{
    public string TabGroup { get; set; }
    public PropertyInfo PropertyInfo { get; set; }
    public object Obj { get; set; }
    public bool ReadOnly { get; set; }
    public Visibility Visibility { get; set; }
    public Action<object> ValueChanged { get; set; }
    public string Unit { get; set; }
    //public int Vip { get; set; }
    //~ObjectPropertyItem()
    //{
    //    ValueChanged = null; 
    //}

    public ObjectPropertyItemBase(PropertyInfo property, object obj)
    {
        this.PropertyInfo = property;
        this.Obj = obj;
        DisplayAttribute display = property.GetCustomAttribute<DisplayAttribute>();
        this.Name = property.Name;
        if (display != null)
        {
            this.Name = display == null ? property.Name : display.Name;
            this.TabGroup = display?.Prompt;
            this.GroupName = display?.GroupName;
            this.Description = display?.Description;
            this.Order = display == null ? 999 : display.GetOrder().HasValue ? display.GetOrder().Value : 999;
        }

        ReadOnlyAttribute readyOnly = property.GetCustomAttribute<ReadOnlyAttribute>();
        this.ReadOnly = readyOnly?.IsReadOnly == true;
        if (!this.PropertyInfo.CanWrite)
            this.ReadOnly = true;
        //  Do ：用于控制显示和隐藏
        BrowsableAttribute browsable = property.GetCustomAttribute<BrowsableAttribute>();
        this.Visibility = browsable == null || browsable.Browsable ? Visibility.Visible : Visibility.Collapsed;

        UnitAttribute unit = property.GetCustomAttribute<UnitAttribute>();
        if (unit != null)
            this.Unit = unit.Unit;
    }

    /// <summary>
    /// 从实体中加载数据到页面
    /// </summary>
    public abstract void LoadValue();

}
