// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Extensions.Behvaiors.DataGrids;

namespace H.Controls.Form;

/// <summary>在FormDataGrid.Columns中按属性名或点分属性路径声明表单属性编辑列。</summary>
public class PropertyItemColumn : DataGridColumn
{
    private readonly FormDataGrid _owner;
    private readonly PropertyInfo _propertyInfo;
    private readonly DataGridColumnAttribute _attribute;
    private PropertyInfo _cachedProperty;
    private IValueConverter _attributeConverter;

    public PropertyItemColumn()
    {
    }

    internal PropertyItemColumn(FormDataGrid owner, PropertyInfo propertyInfo, DataGridColumnAttribute attribute)
    {
        _owner = owner;
        _propertyInfo = propertyInfo;
        _attribute = attribute;
        this.PropertyPath = propertyInfo.Name;
    }

    public string PropertyPath { get; set; }
    public string StringFormat { get; set; }
    public IValueConverter Converter { get; set; }

    protected override FrameworkElement GenerateElement(DataGridCell cell, object dataItem)
    {
        return this.CreateElement(cell, dataItem);
    }

    protected override FrameworkElement GenerateEditingElement(DataGridCell cell, object dataItem)
    {
        return this.CreateElement(cell, dataItem);
    }

    private FrameworkElement CreateElement(DataGridCell cell, object dataItem)
    {
        FormDataGrid owner = this.DataGridOwner as FormDataGrid ?? _owner;
        if (owner == null || string.IsNullOrWhiteSpace(this.PropertyPath))
            return new TextBlock();
        PropertyInfo property = this.ResolveProperty(owner, dataItem);
        if (property == null)
            return owner.CreatePropertyItemElement(null, this.PropertyPath, cell);
        var attribute = _attribute ?? property.GetCustomAttribute<DataGridColumnAttribute>();
        string path = string.Format(attribute?.PropertyPath ?? "{0}", this.PropertyPath);
        if (this.Header == null)
            this.Header = property.GetCustomAttribute<DisplayAttribute>()?.GetName() ?? property.Name;
        if (string.IsNullOrEmpty(this.SortMemberPath))
            this.SortMemberPath = path;
        if (property.SetMethod?.IsPublic != true || property.GetCustomAttribute<ReadOnlyAttribute>()?.IsReadOnly == true || owner.UsePropertyView)
            this.IsReadOnly = true;

        if (!ReferenceEquals(_cachedProperty, property))
        {
            _cachedProperty = property;
            _attributeConverter = attribute?.ValueConvertType == null ? null
                : Activator.CreateInstance(attribute.ValueConvertType) as IValueConverter
                    ?? throw new ArgumentException("ValueConvertType 必须实现 IValueConverter");
        }
        IValueConverter converter = this.Converter ?? _attributeConverter;
        string format = this.StringFormat ?? attribute?.StringFormat ?? property.GetCustomAttribute<DisplayFormatAttribute>()?.DataFormatString;
        if (!owner.UsePropertyView && (converter != null || format != null))
        {
            var text = new TextBlock { VerticalAlignment = VerticalAlignment.Center };
            text.SetBinding(TextBlock.TextProperty, new Binding($"DataContext.{path}")
            {
                Source = cell,
                Mode = BindingMode.OneWay,
                Converter = converter,
                StringFormat = format
            });
            return text;
        }
        return owner.CreatePropertyItemElement(property, path, cell);
    }

    private PropertyInfo ResolveProperty(FormDataGrid owner, object dataItem)
    {
        return owner.Type.ResolvePropertyPath(dataItem, this.PropertyPath, out _)
            ?? (_propertyInfo?.Name == this.PropertyPath ? _propertyInfo : null);
    }
}
