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
    private DataGridLength? _width;
    private double? _minWidth;
    private double? _maxWidth;
    private bool? _isReadOnly;
    private bool? _canUserSort;
    private bool? _canUserResize;
    private bool? _canUserReorder;
    private Visibility? _visibility;

    public DataGridColumnAttribute(string width)
    {
        DataGridLengthConverter converter = new DataGridLengthConverter();
        this.Width = (DataGridLength)converter.ConvertFromString(width);
    }
    public DataGridColumnAttribute()
    {

    }
    public DataGridLength Width
    {
        get => _width ?? DataGridLength.Auto;
        set => _width = value;
    }
    public double MinWidth
    {
        get => _minWidth ?? 20.0;
        set => _minWidth = value;
    }
    public double MaxWidth
    {
        get => _maxWidth ?? double.PositiveInfinity;
        set => _maxWidth = value;
    }
    public bool IsReadOnly
    {
        get => _isReadOnly ?? false;
        set => _isReadOnly = value;
    }
    public bool CanUserSort
    {
        get => _canUserSort ?? true;
        set => _canUserSort = value;
    }
    public bool CanUserResize
    {
        get => _canUserResize ?? true;
        set => _canUserResize = value;
    }
    public bool CanUserReorder
    {
        get => _canUserReorder ?? true;
        set => _canUserReorder = value;
    }
    public Visibility Visibility
    {
        get => _visibility ?? System.Windows.Visibility.Visible;
        set => _visibility = value;
    }
    public int DisplayIndex { get; set; } = -1;
    public string Header { get; set; }
    public string HeaderStyleKey { get; set; }
    public string CellStyleKey { get; set; }
    public string SortMemberPath { get; set; }
    public Type Template { get; set; } = typeof(DataGridTextColumn);
    public Type ValueConvertType { get; set; }
    /// <summary>
    /// "{0}.Property"
    /// </summary>
    public string PropertyPath { get; set; } = "{0}";
    /// <summary>
    /// "{0:F2}"
    /// </summary>
    public string StringFormat { get; set; }
    public virtual DataGridColumn GetDataGridColumn(PropertyInfo propertyInfo)
    {
        DataGridColumn dataGridColumn;
        if (this.Template != null)
        {
            if (!typeof(DataGridColumn).IsAssignableFrom(this.Template) || this.Template.IsAbstract)
                throw new ArgumentException("Template 必须是可实例化的 DataGridColumn 类型", nameof(Template));
            dataGridColumn = (DataGridColumn)Activator.CreateInstance(this.Template);
        }
        else
        {
            Type type = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;
            dataGridColumn = type == typeof(bool) ? new DataGridCheckBoxColumn()
                : type.IsEnum ? new DataGridComboBoxColumn() : new DataGridTextColumn();
        }
        this.ApplyTo(dataGridColumn);
        return dataGridColumn;
    }

    public virtual void ApplyTo(DataGridColumn column, FrameworkElement resourceOwner = null)
    {
        if (column == null)
            throw new ArgumentNullException(nameof(column));
        if (_maxWidth.HasValue)
            column.MaxWidth = _maxWidth.Value;
        if (_minWidth.HasValue)
            column.MinWidth = _minWidth.Value;
        if (_width.HasValue)
            column.Width = _width.Value;
        if (_isReadOnly.HasValue)
            column.IsReadOnly = _isReadOnly.Value;
        if (_canUserSort.HasValue)
            column.CanUserSort = _canUserSort.Value;
        if (_canUserResize.HasValue)
            column.CanUserResize = _canUserResize.Value;
        if (_canUserReorder.HasValue)
            column.CanUserReorder = _canUserReorder.Value;
        if (_visibility.HasValue)
            column.Visibility = _visibility.Value;
        if (this.Header != null)
            column.Header = this.Header;
        if (this.SortMemberPath != null)
            column.SortMemberPath = this.SortMemberPath;
        if (resourceOwner != null)
        {
            if (this.HeaderStyleKey != null)
                column.HeaderStyle = this.GetStyle(resourceOwner, this.HeaderStyleKey);
            if (this.CellStyleKey != null)
                column.CellStyle = this.GetStyle(resourceOwner, this.CellStyleKey);
        }
    }

    public virtual void ApplyDisplayIndex(DataGrid grid, DataGridColumn column)
    {
        if (this.DisplayIndex == -1)
            return;
        if (this.DisplayIndex < 0 || this.DisplayIndex >= grid.Columns.Count || !grid.Columns.Contains(column))
            throw new ArgumentOutOfRangeException(nameof(DisplayIndex), this.DisplayIndex, "列序必须位于已生成列的有效范围内");
        column.DisplayIndex = this.DisplayIndex;
    }

    private Style GetStyle(FrameworkElement resourceOwner, string key)
    {
        return resourceOwner.TryFindResource(key) as Style
            ?? throw new ArgumentException($"未找到 Style 资源：{key}");
    }
}
