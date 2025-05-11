// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Reflection;

namespace H.Extensions.Behvaiors.DataGrids;

[AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
public class DataGridColumnAttribute : Attribute, IDataGridColumn
{
    public DataGridColumnAttribute(string width)
    {
        DataGridLengthConverter converter = new DataGridLengthConverter();
        this.Width = (DataGridLength)converter.ConvertFromString(width);
    }
    public DataGridColumnAttribute()
    {

    }
    public DataGridLength Width { get; set; } = DataGridLength.Auto;
    public Type Template { get; set; } = typeof(DataGridTextColumn);
    public Type ConvertyType { get; set; }
    /// <summary>
    /// "{0}.Property"
    /// </summary>
    public string PropertyPath { get; set; } = "{0}";
    public virtual DataGridColumn GetDataGridColumn(PropertyInfo propertyInfo)
    {
        DataGridColumn dataGridColumn = Activator.CreateInstance(this.Template) as DataGridColumn;
        if (dataGridColumn == null)
        {
            if (propertyInfo.PropertyType == typeof(bool))
                return new DataGridCheckBoxColumn() { Width = this.Width, IsReadOnly = false };
            else if (propertyInfo.PropertyType.IsEnum)
            {
                return new DataGridComboBoxColumn() { Width = this.Width, IsReadOnly = false };
            }
            else
            {
                return new DataGridTextColumn() { Width = this.Width, IsReadOnly = false };
            }
        }
        dataGridColumn.Width = this.Width;
        return dataGridColumn;
    }
}
