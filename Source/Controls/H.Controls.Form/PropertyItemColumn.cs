using H.Extensions.Behvaiors.DataGrids;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace H.Controls.Form;

/// <summary>在FormDataGrid.Columns中按属性名声明表单属性编辑列。</summary>
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
        this.PropertyName = propertyInfo.Name;
    }

    public string PropertyName { get; set; }
    public string StringFormat { get; set; }
    public IValueConverter Converter { get; set; }

    protected override FrameworkElement GenerateElement(DataGridCell cell, object dataItem)
    {
        return this.CreateElement(cell, dataItem, false);
    }

    protected override FrameworkElement GenerateEditingElement(DataGridCell cell, object dataItem)
    {
        return this.CreateElement(cell, dataItem, true);
    }

    private FrameworkElement CreateElement(DataGridCell cell, object dataItem, bool isEditing)
    {
        FormDataGrid owner = this.DataGridOwner as FormDataGrid ?? _owner;
        if (owner == null || string.IsNullOrWhiteSpace(this.PropertyName))
            return new TextBlock();
        PropertyInfo property = this.ResolveProperty(owner, dataItem);
        if (property == null)
            return new TextBlock();
        var attribute = _attribute ?? property.GetCustomAttribute<DataGridColumnAttribute>();
        string path = string.Format(attribute?.PropertyPath ?? "{0}", property.Name);
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
        if (!isEditing && !owner.UsePropertyView && (converter != null || format != null))
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
        return owner.CreatePropertyItemElement(property, path, cell, isEditing);
    }

    private PropertyInfo ResolveProperty(FormDataGrid owner, object dataItem)
    {
        if (_propertyInfo != null)
            return _propertyInfo;
        Type type = owner.Type ?? dataItem?.GetType();
        if (type == null)
            return null;
        PropertyInfo property = type.GetProperty(this.PropertyName, BindingFlags.Instance | BindingFlags.Public);
        if (property == null && type.IsInterface)
            property = type.GetInterfaces().Select(x => x.GetProperty(this.PropertyName, BindingFlags.Instance | BindingFlags.Public))
                .FirstOrDefault(x => x != null);
        return property?.GetMethod?.IsPublic == true && property.GetIndexParameters().Length == 0 ? property : null;
    }
}
