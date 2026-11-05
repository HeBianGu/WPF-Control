// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Common;
using H.Controls.Form.Attributes;
using H.Controls.Form.PropertyItems;
using H.Controls.Form.PropertyItems.Base;
using H.Controls.Form.Provider;
using H.Extensions.Behvaiors.DataGrids;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace H.Controls.Form;

public partial class FormDataGrid : DataGrid
{
    private readonly Dictionary<DataGridColumn, int> _generatedColumnOrders = new();
    private readonly Dictionary<DataGridColumn, DataGridColumnAttribute> _generatedColumnAttributes = new();
    private readonly HashSet<DataGridColumn> _typeGeneratedColumns = new();
    private bool _refreshingColumns;

    public FormDataGrid()
    {
        this.Loaded += (s, e) =>
        {
            if (this.Type != null)
                this.RefreshColumns();
        };
    }

    public string UsePropertyNames
    {
        get => (string)GetValue(UsePropertyNamesProperty);
        set => SetValue(UsePropertyNamesProperty, value);
    }

    public static readonly DependencyProperty UsePropertyNamesProperty =
        DependencyProperty.Register(nameof(UsePropertyNames), typeof(string), typeof(FormDataGrid),
            new FrameworkPropertyMetadata(null, OnOptionsChanged));

    protected virtual bool FilterPropertyInfo(PropertyInfo propertyInfo)
    {
        if (propertyInfo.GetMethod?.IsPublic != true || propertyInfo.GetIndexParameters().Length > 0)
            return false;
        if (propertyInfo.GetCustomAttribute<BrowsableAttribute>()?.Browsable == false)
            return false;
        if (this.UseDeclaredOnly && propertyInfo.DeclaringType != propertyInfo.ReflectedType)
            return false;
        var display = propertyInfo.GetCustomAttribute<DisplayAttribute>();
        if (this.UseDisplayOnly && display == null)
            return false;
        if (!string.IsNullOrWhiteSpace(this.UsePropertyNames)
            && !SplitNames(this.UsePropertyNames).Contains(propertyInfo.Name, StringComparer.Ordinal))
            return false;
        if (SplitNames(this.ExceptPropertyNames).Contains(propertyInfo.Name, StringComparer.Ordinal))
            return false;
        var declaringType = propertyInfo.ReflectedType ?? propertyInfo.DeclaringType;
        if (!MatchesNames(this.UseGroupNames,
            declaringType.GetPropertyGroupNameResx(propertyInfo.Name) ?? display?.GroupName))
            return false;
        if (!MatchesNames(this.UseTabNames, propertyInfo.GetCustomAttribute<TabAttribute>()?.Tab))
            return false;

        Type propertyType = propertyInfo.PropertyType;
        Type valueType = Nullable.GetUnderlyingType(propertyType) ?? propertyType;
        bool isCommand = typeof(ICommand).IsAssignableFrom(propertyType);
        if (this.UseCommandOnly && !isCommand || !this.UseCommand && isCommand)
            return false;
        if (!this.UseEnumerator && propertyType != typeof(string)
            && (typeof(IEnumerable).IsAssignableFrom(propertyType) || typeof(IEnumerator).IsAssignableFrom(propertyType)))
            return false;
        if (!this.UseEnum && valueType.IsEnum || !this.UseDateTime && valueType == typeof(DateTime)
            || !this.UseClass && propertyType.IsClass && propertyType != typeof(string)
            || !this.UseArray && propertyType.IsArray || !this.UseInterface && propertyType.IsInterface
            || !this.UsePrimitive && valueType.IsPrimitive || !this.UseString && valueType == typeof(string)
            || !this.UseBoolen && valueType == typeof(bool))
            return false;
        if (this.UseTypeConverterOnly && propertyInfo.GetCustomAttribute<TypeConverterAttribute>() == null
            && propertyType.GetCustomAttribute<TypeConverterAttribute>() == null)
            return false;

        return true;
    }

    protected virtual IPropertyItem CreatePropertyItem(PropertyInfo propertyInfo, object obj)
    {
        if (propertyInfo.PropertyType.IsClass && propertyInfo.PropertyType != typeof(string)
            && propertyInfo.GetCustomAttribute<UsePropertyPresenterAttribute>() != null)
            return new PresenterPropertyItem(propertyInfo, obj);
        return this.UsePropertyView ? propertyInfo.CreateView(obj) : propertyInfo.Create(obj);
    }

    internal FrameworkElement CreatePropertyItemElement(PropertyInfo propertyInfo, string propertyPath, DataGridCell cell, bool isEditing)
    {
        return new PropertyItemCell(this, propertyInfo, propertyPath, cell, isEditing);
    }

    public Type Type
    {
        get { return (Type)GetValue(TypeProperty); }
        set { SetValue(TypeProperty, value); }
    }

    public static readonly DependencyProperty TypeProperty =
        DependencyProperty.Register(nameof(Type), typeof(Type), typeof(FormDataGrid), new FrameworkPropertyMetadata(default(Type), OnOptionsChanged));

    protected override void OnItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue)
    {
        base.OnItemsSourceChanged(oldValue, newValue);
        if (this.Type != null)
            this.RefreshColumns();
    }

    protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);
        if (_typeGeneratedColumns != null && e.Property == AutoGenerateColumnsProperty && (this.Type != null || _typeGeneratedColumns.Count > 0))
            this.RefreshColumns();
    }

    private void RefreshColumns()
    {
        if (_refreshingColumns)
            return;
        _refreshingColumns = true;
        try
        {
            foreach (var column in _typeGeneratedColumns)
                this.Columns.Remove(column);
            _typeGeneratedColumns.Clear();
            _generatedColumnOrders.Clear();
            _generatedColumnAttributes.Clear();
            if (!this.AutoGenerateColumns)
                return;

            this.SetCurrentValue(AutoGenerateColumnsProperty, false);
            this.SetCurrentValue(AutoGenerateColumnsProperty, true);
            if (this.Type == null)
                return;

            var types = this.Type.IsInterface && !this.UseDeclaredOnly
                ? new[] { this.Type }.Concat(this.Type.GetInterfaces()) : new[] { this.Type };
            var properties = types.SelectMany(x => x.GetProperties(BindingFlags.Instance | BindingFlags.Public))
                .GroupBy(x => x.Name, StringComparer.Ordinal).Select(x => x.First())
                .Where(this.FilterPropertyInfo)
                .OrderBy(x => x.GetCustomAttribute<DisplayAttribute>()?.GetOrder() ?? int.MaxValue);
            foreach (var property in properties)
            {
                var column = this.CreateColumn(property, this.Type, false);
                this.Columns.Add(column);
                _typeGeneratedColumns.Add(column);
            }
        }
        finally
        {
            _refreshingColumns = false;
        }
    }

    private DataGridColumn CreateColumn(PropertyInfo propertyInfo, Type componentType, bool isReadOnly)
    {
        var display = propertyInfo.GetCustomAttribute<DisplayAttribute>();
        var name = display?.GetName() ?? propertyInfo.Name;
        var columnAttribute = propertyInfo.GetCustomAttribute<DataGridColumnAttribute>();
        var column = new PropertyItemColumn(this, propertyInfo, columnAttribute)
        {
            Header = componentType.GetPropertyNameResx(propertyInfo.Name, name) ?? name,
            SortMemberPath = string.Format(columnAttribute?.PropertyPath ?? "{0}", propertyInfo.Name),
            IsReadOnly = this.UsePropertyView || isReadOnly || propertyInfo.SetMethod?.IsPublic != true || propertyInfo.GetCustomAttribute<ReadOnlyAttribute>()?.IsReadOnly == true,
            Width = this.ColumnWidth,
            MinWidth = this.MinColumnWidth,
            MaxWidth = this.MaxColumnWidth
        };
        bool readOnly = column.IsReadOnly;
        columnAttribute?.ApplyTo(column, this);
        column.IsReadOnly = column.IsReadOnly || readOnly;
        if (columnAttribute != null)
            _generatedColumnAttributes[column] = columnAttribute;
        return column;
    }

    protected override void OnAutoGeneratingColumn(DataGridAutoGeneratingColumnEventArgs e)
    {
        if (this.Type != null)
        {
            base.OnAutoGeneratingColumn(e);
            e.Cancel = true;
            return;
        }
        var descriptor = e.PropertyDescriptor as PropertyDescriptor;
        var propertyInfo = e.PropertyDescriptor as PropertyInfo
            ?? descriptor?.ComponentType.GetProperty(e.PropertyName, BindingFlags.Instance | BindingFlags.Public);

        if (propertyInfo != null)
        {
            if (!this.FilterPropertyInfo(propertyInfo))
                e.Cancel = true;
            else
            {
                var display = propertyInfo.GetCustomAttribute<DisplayAttribute>();
                var column = this.CreateColumn(propertyInfo,
                    descriptor?.ComponentType ?? propertyInfo.ReflectedType ?? propertyInfo.DeclaringType, e.Column.IsReadOnly);
                e.Column = column;
                _generatedColumnOrders[column] = display?.GetOrder() ?? int.MaxValue;
            }
        }

        if (this.UsePropertyView)
            e.Column.IsReadOnly = true;
        base.OnAutoGeneratingColumn(e);
    }

    protected override void OnAutoGeneratedColumns(EventArgs e)
    {
        var columns = this.Columns.Where(x => _generatedColumnOrders.ContainsKey(x)).ToArray();
        var displayIndexes = columns.Select(x => x.DisplayIndex).OrderBy(x => x).ToArray();

        if (displayIndexes.Any(x => x < 0 || x >= this.Columns.Count)
            || displayIndexes.Distinct().Count() != displayIndexes.Length)
        {
            displayIndexes = columns.Select(x => this.Columns.IndexOf(x)).OrderBy(x => x).ToArray();
        }

        var orderedColumns = columns.OrderBy(x => _generatedColumnOrders[x]).ToArray();
        for (int i = 0; i < orderedColumns.Length; i++)
            orderedColumns[i].DisplayIndex = displayIndexes[i];
        _generatedColumnOrders.Clear();
        base.OnAutoGeneratedColumns(e);
    }

    private sealed class PropertyItemCell : ContentControl
    {
        private readonly FormDataGrid _owner;
        private readonly PropertyInfo _propertyInfo;
        private readonly string _propertyName;

        public PropertyItemCell(FormDataGrid owner, PropertyInfo propertyInfo, string propertyPath, DataGridCell cell, bool isEditing)
        {
            _owner = owner;
            _propertyInfo = propertyInfo;
            int separator = propertyPath.LastIndexOf('.');
            _propertyName = separator < 0 ? propertyPath : propertyPath.Substring(separator + 1);
            string ownerPath = separator < 0 ? nameof(DataGridCell.DataContext)
                : $"DataContext.{propertyPath.Substring(0, separator)}";
            this.HorizontalContentAlignment = HorizontalAlignment.Stretch;
            this.VerticalContentAlignment = VerticalAlignment.Center;
            this.IsHitTestVisible = isEditing;
            this.Focusable = false;
            KeyboardNavigation.SetTabNavigation(this, isEditing ? KeyboardNavigationMode.Continue : KeyboardNavigationMode.None);
            this.SetBinding(DataContextProperty, new Binding(ownerPath) { Source = cell });
            this.Loaded += (s, e) => this.RefreshPropertyItem();
            this.Unloaded += (s, e) => this.ClearPropertyItem();
            this.DataContextChanged += (s, e) =>
            {
                this.ClearPropertyItem();
                if (this.IsLoaded)
                    this.RefreshPropertyItem();
            };
        }

        private void RefreshPropertyItem()
        {
            this.ClearPropertyItem();
            var obj = this.DataContext;
            if (obj == null || ReferenceEquals(obj, CollectionView.NewItemPlaceholder))
                return;

            var propertyInfo = _propertyName == _propertyInfo.Name && _propertyInfo.DeclaringType.IsInstanceOfType(obj)
                ? _propertyInfo : obj.GetType().GetProperty(_propertyName, BindingFlags.Instance | BindingFlags.Public);
            if (propertyInfo?.GetMethod?.IsPublic != true || propertyInfo.GetIndexParameters().Length > 0)
                return;
            var propertyItem = _owner.CreatePropertyItem(propertyInfo, obj);
            if (propertyItem is ObjectPropertyItemBase objectPropertyItem)
            {
                objectPropertyItem.UseTitle = false;
                if (_owner.UsePropertyView)
                    objectPropertyItem.ReadOnly = true;
            }
            this.Content = propertyItem;
        }

        private void ClearPropertyItem()
        {
            if (this.Content is IDisposable disposable)
                disposable.Dispose();
            this.Content = null;
        }
    }
}
