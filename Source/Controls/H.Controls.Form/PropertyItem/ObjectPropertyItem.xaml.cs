// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using H.Mvvm;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Windows;

namespace H.Controls.Form
{
    /// <summary> 类型基类 </summary>
    public abstract class ObjectPropertyItem : DisplayBindableBase, IPropertyItem
    {
        public string TabGroup { get; set; }
        public PropertyInfo PropertyInfo { get; set; }
        public object Obj { get; set; }
        public bool ReadOnly { get; set; }
        public Visibility Visibility { get; set; }
        public Action<object> ValueChanged { get; set; }
        public string Unit { get; set; }
        public int Vip { get; set; }
        //~ObjectPropertyItem()
        //{
        //    ValueChanged = null; 
        //}

        public ObjectPropertyItem(PropertyInfo property, object obj)
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
            //DisplayAttribute displayer = property.GetCustomAttribute<DisplayAttribute>();
            //if (displayer != null)
            //{
            //    this.Name = displayer == null ? displayer.Name : displayer.Name;
            //    this.GroupName = displayer?.GroupName;
            //    this.Description = displayer?.Description;
            //    this.Order = displayer == null ? 999 : displayer.Order;
            //    this.Icon = displayer?.Icon;
            //}

            ReadOnlyAttribute readyOnly = property.GetCustomAttribute<ReadOnlyAttribute>();
            this.ReadOnly = readyOnly?.IsReadOnly == true;
            if (!this.PropertyInfo.CanWrite)
            {
                this.ReadOnly = true;
            }
            //  Do ：用于控制显示和隐藏
            BrowsableAttribute browsable = property.GetCustomAttribute<BrowsableAttribute>();
            this.Visibility = browsable == null || browsable.Browsable ? Visibility.Visible : Visibility.Collapsed;

            UnitAttribute unit = property.GetCustomAttribute<UnitAttribute>();
            if (unit != null)
                this.Unit = unit.Unit;

            //var vip = property.GetCustomAttribute<VipAttribute>();
            //if (vip != null)
            //    this.Vip = vip.Level;
        }

        /// <summary>
        /// 从实体中加载数据到页面
        /// </summary>
        public abstract void LoadValue();
    }













    ///// <summary> Double属性类型 </summary>
    //public class DoubleArrayPropertyItem : ObjectPropertyItem<double[]>
    //{
    //    public DoubleArrayPropertyItem(PropertyInfo property, object obj) : base(property, obj)
    //    {
    //    }
    //}
}
