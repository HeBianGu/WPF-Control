// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control
using H.Controls.Form.PropertyItem.Attribute.SourcePropertyItem;
using H.Controls.Form.PropertyItem.ComboBoxPropertyItems;
using H.Controls.Form.PropertyItem.EnumerablePropertyItems;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows;

namespace H.Controls.Form.PropertyItem
{
    public class SourcePropertyItemDemoModel
    {
        public SourcePropertyItemDemoModel()
        {
            this.ModelItemSource = Enumerable.Range(0, 10).Select(x => new DemoModelItem() { Name = "Item" + x }).ToArray();

            this.SelectSource = Enumerable.Range(0, 10).Select(x => "Item" + x).ToArray();
        }

        public string[] SelectSource { get; set; }

        public DemoModelItem[] ModelItemSource { get; set; }

        public DemoModelItem[] GetModelItemSource()
        {
            return this.ModelItemSource;
        }
    }

    public class ComboBoxPropertyItemDemoModel : SourcePropertyItemDemoModel
    {

        [Display(Name = "ComboBoxPropertyItem", Description = "演示应用PropertyItemAttribute自定义显示样式")]
        [PropertyNameSourcePropertyItem(typeof(ComboBoxPropertyItem), nameof(SelectSource))]
        public string SelectItem { get; set; }

        [Display(Name = "PresenterComboBoxPropertyItem", Description = "演示应用PropertyItemAttribute自定义显示样式")]
        [PropertyNameSourcePropertyItem(typeof(PresenterComboBoxPropertyItem), nameof(ModelItemSource))]
        public DemoModelItem PresenterSelectItem { get; set; }

        [Display(Name = "FormComboBoxPropertyItem", Description = "演示应用PropertyItemAttribute自定义显示样式")]
        [MethodNameSourcePropertyItem(typeof(PresenterComboBoxPropertyItem), nameof(GetModelItemSource))]
        public DemoModelItem FormSelectItem { get; set; }

        [Display(Name = "EnumSourcePropertyItem", Description = "演示应用PropertyItemAttribute自定义显示样式")]
        [EnumSourcePropertyItem(typeof(ComboBoxPropertyItem))]
        public HorizontalAlignment HorizontalAlignment { get; set; }

        [Display(Name = "EnumComboBoxPropertyItem", Description = "演示应用PropertyItemAttribute自定义显示样式")]
        [PropertyItem(typeof(EnumComboBoxPropertyItem))]
        public VerticalAlignment VerticalAlignment { get; set; }
    }

    public class ListBoxPropertyItemDemoModel : SourcePropertyItemDemoModel
    {

        [Display(Name = "ListBoxPropertyItem", Description = "演示应用PropertyItemAttribute自定义显示样式")]
        [PropertyNameSourcePropertyItem(typeof(ListBoxPropertyItem), nameof(SelectSource))]
        public string SelectItem { get; set; }

        [Display(Name = "MethodNameSourcePropertyItem", Description = "演示应用PropertyItemAttribute自定义显示样式")]
        [MethodNameSourcePropertyItem(typeof(ListBoxPropertyItem), nameof(GetModelItemSource))]
        public DemoModelItem FormSelectItem { get; set; }

        [Display(Name = "EnumSourcePropertyItem", Description = "演示应用PropertyItemAttribute自定义显示样式")]
        [EnumSourcePropertyItem(typeof(ListBoxPropertyItem))]
        public HorizontalAlignment HorizontalAlignment { get; set; }
    }

    public class MultiListBoxPropertyItemDemoModel : SourcePropertyItemDemoModel
    {
        public MultiListBoxPropertyItemDemoModel()
        {
            this.SelectItems = this.SelectSource.ToList();
        }
        [Display(Name = "MultiListBoxPropertyItem", Description = "演示应用PropertyItemAttribute自定义显示样式")]
        [PropertyNameSourcePropertyItem(typeof(MultiListBoxPropertyItem), nameof(SelectSource))]
        public IList<string> SelectItems { get; set; }
    }

    public class EnumerablePropertyItemDemoModel : SourcePropertyItemDemoModel
    {
        public EnumerablePropertyItemDemoModel()
        {
            this.SelectItems = this.ModelItemSource.ToList();
            this.SelectItemsDefault = this.ModelItemSource.ToList();
        }
        [Display(Name = "EnumerablePropertyItem", Description = "演示应用PropertyItemAttribute自定义显示样式")]
        [PropertyItem(typeof(EnumerablePropertyItem))]
        public IList<DemoModelItem> SelectItems { get; set; }

        [Display(Name = "Default", Description = "演示应用PropertyItemAttribute自定义显示样式")]
        public IList<DemoModelItem> SelectItemsDefault { get; set; }
    }

    public class DemoModelItem
    {
        [Display(Name = "名称")]
        public string Name { get; set; }

        [Display(Name = "值")]
        public string Value { get; set; }
    }
}
