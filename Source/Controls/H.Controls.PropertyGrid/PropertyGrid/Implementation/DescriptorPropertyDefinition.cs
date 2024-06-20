// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup.Primitives;
#if !VS2008
using System.ComponentModel.DataAnnotations;
#endif
using System.Globalization;

namespace H.Controls.PropertyGrid
{
    internal class DescriptorPropertyDefinition : DescriptorPropertyDefinitionBase
    {
        #region Members

        private object _selectedObject;
        private PropertyDescriptor _propertyDescriptor;
        private DependencyPropertyDescriptor _dpDescriptor;
        private MarkupObject _markupObject;

        #endregion

        #region Constructor

        internal DescriptorPropertyDefinition(PropertyDescriptor propertyDescriptor, object selectedObject, IPropertyContainer propertyContainer)
           : base(propertyContainer.IsCategorized
                 )
        {
            this.Init(propertyDescriptor, selectedObject);
        }

        #endregion

        #region Custom Properties

        internal override PropertyDescriptor PropertyDescriptor
        {
            get
            {
                return _propertyDescriptor;
            }
        }

        private object SelectedObject
        {
            get
            {
                return _selectedObject;
            }
        }

        #endregion

        #region Override Methods

        internal override ObjectContainerHelperBase CreateContainerHelper(IPropertyContainer parent)
        {
            return new ObjectContainerHelper(parent, this.Value);
        }

        internal override void OnValueChanged(object oldValue, object newValue)
        {
            base.OnValueChanged(oldValue, newValue);
            this.RaiseContainerHelperInvalidated();
        }

        protected override BindingBase CreateValueBinding()
        {
            object selectedObject = this.SelectedObject;
            string propertyName = this.PropertyDescriptor.Name;

            //Bind the value property with the source object.
            Binding binding = new Binding(propertyName)
            {
                Source = this.GetValueInstance(selectedObject),
                Mode = this.PropertyDescriptor.IsReadOnly ? BindingMode.OneWay : BindingMode.TwoWay,
                ValidatesOnDataErrors = true,
                ValidatesOnExceptions = true,
                ConverterCulture = CultureInfo.CurrentCulture
            };

            return binding;
        }

        protected override bool ComputeIsReadOnly()
        {
            return this.PropertyDescriptor.IsReadOnly;
        }

        internal override ITypeEditor CreateDefaultEditor(PropertyItem propertyItem)
        {
            return PropertyGridUtilities.CreateDefaultEditor(this.PropertyDescriptor.PropertyType, this.PropertyDescriptor.Converter, propertyItem);
        }

        protected override bool ComputeCanResetValue()
        {
            if (!this.PropertyDescriptor.IsReadOnly)
            {
                object defaultValue = this.ComputeDefaultValueAttribute();
                if (defaultValue != null)
                    return true;

                return this.PropertyDescriptor.CanResetValue(this.SelectedObject);
            }

            return false;
        }

        protected override object ComputeAdvancedOptionsTooltip()
        {
            object tooltip;
            UpdateAdvanceOptionsForItem(_markupObject, this.SelectedObject as DependencyObject, _dpDescriptor, out tooltip);

            return tooltip;
        }

        protected override string ComputeCategory()
        {
#if VS2008
        return PropertyDescriptor.Category;
#else
            DisplayAttribute displayAttribute = PropertyGridUtilities.GetAttribute<DisplayAttribute>(this.PropertyDescriptor);
            return ((displayAttribute != null) && (displayAttribute.GetGroupName() != null)) ? displayAttribute.GetGroupName() : this.PropertyDescriptor.Category;
#endif
        }

        protected override string ComputeCategoryValue()
        {
            return this.PropertyDescriptor.Category;
        }

        protected override bool ComputeExpandableAttribute()
        {
            return (bool)this.ComputeExpandableAttributeForItem(this.PropertyDescriptor);
        }

        protected override object ComputeDefaultValueAttribute()
        {
            return this.ComputeDefaultValueAttributeForItem(this.PropertyDescriptor);
        }

        protected override bool ComputeIsExpandable()
        {
            return this.Value != null
              ;
        }

        protected override IList<Type> ComputeNewItemTypes()
        {
            return (IList<Type>)ComputeNewItemTypesForItem(this.PropertyDescriptor);
        }
        protected override string ComputeDescription()
        {
            return (string)ComputeDescriptionForItem(this.PropertyDescriptor);
        }

        protected override int ComputeDisplayOrder(bool isPropertyGridCategorized)
        {
            this.IsPropertyGridCategorized = isPropertyGridCategorized;
            return (int)ComputeDisplayOrderForItem(this.PropertyDescriptor);
        }

        protected override void ResetValue()
        {
            this.PropertyDescriptor.ResetValue(this.SelectedObject);
        }

        internal override ITypeEditor CreateAttributeEditor()
        {
            EditorAttribute editorAttribute = GetAttribute<EditorAttribute>();
            if (editorAttribute != null)
            {
                Type type = null;
#if VS2008
        type = Type.GetType( editorAttribute.EditorTypeName );
#else
                try
                {
                    type = Type.GetType(editorAttribute.EditorTypeName, (name) =>
                    {
                        return AppDomain.CurrentDomain.GetAssemblies().Where(l => l.FullName == name.FullName).FirstOrDefault();
                    }
                    , null, true);
                }
                catch (Exception)
                {
                    type = Type.GetType(editorAttribute.EditorTypeName);
                }
#endif

                // If the editor does not have any public parameterless constructor, forget it.
                if (typeof(ITypeEditor).IsAssignableFrom(type)
                  && (type.GetConstructor(new Type[0]) != null))
                {
                    ITypeEditor instance = Activator.CreateInstance(type) as ITypeEditor;
                    Debug.Assert(instance != null, "Type was expected to be ITypeEditor with public constructor.");
                    if (instance != null)
                        return instance;
                }
            }

            ItemsSourceAttribute itemsSourceAttribute = GetAttribute<ItemsSourceAttribute>();
            if (itemsSourceAttribute != null)
                return new ItemsSourceAttributeEditor(itemsSourceAttribute);

            return null;
        }

        #endregion

        #region Private Methods

        private T GetAttribute<T>() where T : Attribute
        {
            return PropertyGridUtilities.GetAttribute<T>(this.PropertyDescriptor);
        }

        private void Init(PropertyDescriptor propertyDescriptor, object selectedObject)
        {
            if (propertyDescriptor == null)
                throw new ArgumentNullException("propertyDescriptor");

            if (selectedObject == null)
                throw new ArgumentNullException("selectedObject");

            _propertyDescriptor = propertyDescriptor;
            _selectedObject = selectedObject;
            _dpDescriptor = DependencyPropertyDescriptor.FromProperty(propertyDescriptor);
            _markupObject = MarkupWriter.GetMarkupObjectFor(this.SelectedObject);
        }

        #endregion //Private Methods

    }
}
