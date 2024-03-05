﻿// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace H.Controls.PropertyGrid
{
    internal class PropertyGridUtilities
    {
        internal static T GetAttribute<T>(PropertyDescriptor property) where T : Attribute
        {
            return property.Attributes.OfType<T>().FirstOrDefault();
        }




        internal static ITypeEditor CreateDefaultEditor(Type propertyType, TypeConverter typeConverter, PropertyItem propertyItem)
        {
            ITypeEditor editor = null;

            EditorTypeDescriptorContext context = new EditorTypeDescriptorContext(null, propertyItem.Instance, propertyItem.PropertyDescriptor);
            if ((typeConverter != null)
              && typeConverter.GetStandardValuesSupported(context)
              && typeConverter.GetStandardValuesExclusive(context)
              && (propertyType != typeof(bool)) && (propertyType != typeof(bool?)))  //Bool type always have a BooleanConverter with standardValues : True/False.
            {
                TypeConverter.StandardValuesCollection items = typeConverter.GetStandardValues(context);
                editor = new SourceComboBoxEditor(items, typeConverter);
            }
            else if (propertyType == typeof(string))
                editor = new TextBoxEditor();
            else if (propertyType == typeof(bool) || propertyType == typeof(bool?))
                editor = new CheckBoxEditor();
            else if (propertyType == typeof(decimal) || propertyType == typeof(decimal?))
                editor = new DecimalUpDownEditor();
            else if (propertyType == typeof(double) || propertyType == typeof(double?))
                editor = new DoubleUpDownEditor();
            else if (propertyType == typeof(int) || propertyType == typeof(int?))
                editor = new IntegerUpDownEditor();
            else if (propertyType == typeof(short) || propertyType == typeof(short?))
                editor = new ShortUpDownEditor();
            else if (propertyType == typeof(long) || propertyType == typeof(long?))
                editor = new LongUpDownEditor();
            else if (propertyType == typeof(float) || propertyType == typeof(float?))
                editor = new SingleUpDownEditor();
            else if (propertyType == typeof(byte) || propertyType == typeof(byte?))
                editor = new ByteUpDownEditor();
            else if (propertyType == typeof(sbyte) || propertyType == typeof(sbyte?))
                editor = new SByteUpDownEditor();
            else if (propertyType == typeof(uint) || propertyType == typeof(uint?))
                editor = new UIntegerUpDownEditor();
            else if (propertyType == typeof(ulong) || propertyType == typeof(ulong?))
                editor = new ULongUpDownEditor();
            else if (propertyType == typeof(ushort) || propertyType == typeof(ushort?))
                editor = new UShortUpDownEditor();
            else if (propertyType == typeof(DateTime) || propertyType == typeof(DateTime?))
                editor = new DateTimeUpDownEditor();
            else if ((propertyType == typeof(Color)) || (propertyType == typeof(Color?)))
                editor = new ColorEditor();
            else if (propertyType.IsEnum)
                editor = new EnumComboBoxEditor();
            else if (propertyType == typeof(TimeSpan) || propertyType == typeof(TimeSpan?))
                editor = new TimeSpanUpDownEditor();
            else if (propertyType == typeof(FontFamily) || propertyType == typeof(FontWeight) || propertyType == typeof(FontStyle) || propertyType == typeof(FontStretch))
                editor = new FontComboBoxEditor();
            else if (propertyType == typeof(Guid) || propertyType == typeof(Guid?))
                editor = new MaskedTextBoxEditor() { ValueDataType = propertyType, Mask = "AAAAAAAA-AAAA-AAAA-AAAA-AAAAAAAAAAAA" };
            else if (propertyType == typeof(char) || propertyType == typeof(char?))
                editor = new MaskedTextBoxEditor() { ValueDataType = propertyType, Mask = "&" };
            else if (propertyType == typeof(object))
                // If any type of object is possible in the property, default to the TextBoxEditor.
                // Useful in some case (e.g., Button.Content).
                // Can be reconsidered but was the legacy behavior on the PropertyGrid.
                editor = new TextBoxEditor();
            else
            {
                Type listType = ListUtilities.GetListItemType(propertyType);

                // A List of T
                if (listType != null)
                {
                    if (!listType.IsPrimitive && !listType.Equals(typeof(String)) && !listType.IsEnum)
                        editor = new CollectionEditor();
                    else
                        editor = new PrimitiveTypeCollectionEditor();
                }
                else
                {
                    Type[] dictionaryType = ListUtilities.GetDictionaryItemsType(propertyType);
                    Type collectionType = ListUtilities.GetCollectionItemType(propertyType);
                    // A dictionary of T or a Collection of T or an ICollection
                    if ((dictionaryType != null) || (collectionType != null) || typeof(ICollection).IsAssignableFrom(propertyType))
                    {
                        editor = new CollectionEditor();
                    }
                    else
                    {
                        // If the type is not supported, check if there is a converter that supports
                        // string conversion to the object type. Use TextBox in theses cases.
                        // Otherwise, return a TextBlock editor since no valid editor exists.
                        editor = (typeConverter != null && typeConverter.CanConvertFrom(typeof(string)))
                          ? new TextBoxEditor()
                          : new TextBlockEditor();
                    }
                }
            }

            return editor;
        }














        #region Private class

        private class EditorTypeDescriptorContext : ITypeDescriptorContext
        {
            private IContainer _container;
            private object _instance;
            private PropertyDescriptor _propertyDescriptor;

            internal EditorTypeDescriptorContext(IContainer container, object instance, PropertyDescriptor pd)
            {
                _container = container;
                _instance = instance;
                _propertyDescriptor = pd;
            }

            IContainer ITypeDescriptorContext.Container
            {
                get
                {
                    return _container;
                }
            }
            object ITypeDescriptorContext.Instance
            {
                get
                {
                    return _instance;
                }
            }
            PropertyDescriptor ITypeDescriptorContext.PropertyDescriptor
            {
                get
                {
                    return _propertyDescriptor;
                }
            }

            void ITypeDescriptorContext.OnComponentChanged()
            {
            }

            bool ITypeDescriptorContext.OnComponentChanging()
            {
                return false;
            }

            object IServiceProvider.GetService(Type serviceType)
            {
                return null;
            }
        }

        #endregion
    }
}
