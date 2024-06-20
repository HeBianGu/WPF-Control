// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;
using System.Collections.Generic;
using System.ComponentModel;
#if !VS2008
using System.ComponentModel.DataAnnotations;
#endif
using System.Diagnostics;
using System.Linq;


namespace H.Controls.PropertyGrid
{
    internal class ObjectContainerHelper : ObjectContainerHelperBase
    {
        private object _selectedObject;

        public ObjectContainerHelper(IPropertyContainer propertyContainer, object selectedObject)
          : base(propertyContainer)
        {
            _selectedObject = selectedObject;
        }

        private object SelectedObject
        {
            get
            {
                return _selectedObject;
            }
        }

        protected override string GetDefaultPropertyName()
        {
            object selectedObject = this.SelectedObject;
            return (selectedObject != null) ? ObjectContainerHelperBase.GetDefaultPropertyName(this.SelectedObject) : null;
        }

        protected override IEnumerable<PropertyItem> GenerateSubPropertiesCore()
        {
            List<PropertyItem> propertyItems = new List<PropertyItem>();

            if (this.SelectedObject != null)
            {
                try
                {
                    List<PropertyDescriptor> descriptors = new List<PropertyDescriptor>();
                    {
                        descriptors = ObjectContainerHelperBase.GetPropertyDescriptors(this.SelectedObject, this.PropertyContainer.HideInheritedProperties);
                    }

                    foreach (PropertyDescriptor descriptor in descriptors)
                    {
                        PropertyDefinition propertyDef = this.GetPropertyDefinition(descriptor);
                        bool isBrowsable = false;

                        bool? isPropertyBrowsable = this.PropertyContainer.IsPropertyVisible(descriptor);
                        if (isPropertyBrowsable.HasValue)
                        {
                            isBrowsable = isPropertyBrowsable.Value;
                        }
                        else
                        {
#if !VS2008
                            DisplayAttribute displayAttribute = PropertyGridUtilities.GetAttribute<DisplayAttribute>(descriptor);
                            if (displayAttribute != null)
                            {
                                bool? autoGenerateField = displayAttribute.GetAutoGenerateField();
                                isBrowsable = this.PropertyContainer.AutoGenerateProperties
                                              && ((autoGenerateField.HasValue && autoGenerateField.Value) || !autoGenerateField.HasValue);
                            }
                            else
#endif
                            {
                                isBrowsable = descriptor.IsBrowsable && this.PropertyContainer.AutoGenerateProperties;
                            }

                            if (propertyDef != null)
                            {
                                isBrowsable = propertyDef.IsBrowsable.GetValueOrDefault(isBrowsable);
                            }
                        }

                        if (isBrowsable)
                        {
                            PropertyItem prop = this.CreatePropertyItem(descriptor, propertyDef);
                            if (prop != null)
                            {
                                propertyItems.Add(prop);
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    //TODO: handle this some how
                    Debug.WriteLine("Property creation failed.");
                    Debug.WriteLine(e.StackTrace);
                }
            }

            return propertyItems;
        }


        private PropertyItem CreatePropertyItem(PropertyDescriptor property, PropertyDefinition propertyDef)
        {
            DescriptorPropertyDefinition definition = new DescriptorPropertyDefinition(property, this.SelectedObject, this.PropertyContainer);
            definition.InitProperties();

            this.InitializeDescriptorDefinition(definition, propertyDef);
            PropertyItem propertyItem = new PropertyItem(definition);
            Debug.Assert(this.SelectedObject != null);
            propertyItem.Instance = this.SelectedObject;
            propertyItem.CategoryOrder = this.GetCategoryOrder(definition.CategoryValue);

            propertyItem.WillRefreshPropertyGrid = this.GetWillRefreshPropertyGrid(property);
            return propertyItem;
        }

        private int GetCategoryOrder(object categoryValue)
        {
            Debug.Assert(this.SelectedObject != null);

            if (categoryValue == null)
                return int.MaxValue;

            int order = int.MaxValue;
            object selectedObject = this.SelectedObject;
            CategoryOrderAttribute[] orderAttributes = (selectedObject != null)
              ? (CategoryOrderAttribute[])selectedObject.GetType().GetCustomAttributes(typeof(CategoryOrderAttribute), true)
              : new CategoryOrderAttribute[0];

            CategoryOrderAttribute orderAttribute = orderAttributes
              .FirstOrDefault((a) => object.Equals(a.CategoryValue, categoryValue));

            if (orderAttribute != null)
            {
                order = orderAttribute.Order;
            }

            return order;
        }








    }
}
