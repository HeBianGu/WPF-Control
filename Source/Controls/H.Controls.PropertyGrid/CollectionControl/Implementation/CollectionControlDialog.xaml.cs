﻿// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Security;
using System.Windows;
using System.Windows.Media;

namespace H.Controls.PropertyGrid
{
    public partial class CollectionControlDialogBase : Window
    {

    }

    public partial class CollectionControlDialog : CollectionControlDialogBase
    {
        #region Private Members

        private IList originalData = new List<object>();

        #endregion

        #region Properties

        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(CollectionControlDialog), new UIPropertyMetadata(null));
        public IEnumerable ItemsSource
        {
            get
            {
                return (IEnumerable)GetValue(ItemsSourceProperty);
            }
            set
            {
                SetValue(ItemsSourceProperty, value);
            }
        }

        public static readonly DependencyProperty ItemsSourceTypeProperty = DependencyProperty.Register("ItemsSourceType", typeof(Type), typeof(CollectionControlDialog), new UIPropertyMetadata(null));
        public Type ItemsSourceType
        {
            get
            {
                return (Type)GetValue(ItemsSourceTypeProperty);
            }
            set
            {
                SetValue(ItemsSourceTypeProperty, value);
            }
        }

        public static readonly DependencyProperty NewItemTypesProperty = DependencyProperty.Register("NewItemTypes", typeof(IList), typeof(CollectionControlDialog), new UIPropertyMetadata(null));
        public IList<Type> NewItemTypes
        {
            get
            {
                return (IList<Type>)GetValue(NewItemTypesProperty);
            }
            set
            {
                SetValue(NewItemTypesProperty, value);
            }
        }

        public static readonly DependencyProperty IsReadOnlyProperty = DependencyProperty.Register("IsReadOnly", typeof(bool), typeof(CollectionControlDialog), new UIPropertyMetadata(false));
        public bool IsReadOnly
        {
            get
            {
                return (bool)GetValue(IsReadOnlyProperty);
            }
            set
            {
                SetValue(IsReadOnlyProperty, value);
            }
        }

        public CollectionControl CollectionControl
        {
            get
            {
                return _collectionControl;
            }
        }

        #endregion //Properties

        #region Constructors

        public CollectionControlDialog()
        {
            InitializeComponent();
        }

        public CollectionControlDialog(Type itemsourceType)
          : this()
        {
            this.ItemsSourceType = itemsourceType;
        }

        public CollectionControlDialog(Type itemsourceType, IList<Type> newItemTypes)
          : this(itemsourceType)
        {
            this.NewItemTypes = newItemTypes;
        }

        #endregion //Constructors

        #region Overrides

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            //Backup data if case "Cancel" is clicked.
            if (this.ItemsSource != null)
            {
                foreach (object item in this.ItemsSource)
                {
                    originalData.Add(this.Clone(item));
                }
            }
        }

        #endregion

        #region Event Handlers

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.ItemsSource is IDictionary)
            {
                if (!this.AreDictionaryKeysValid())
                {
                    MessageBox.Show("All dictionary items should have distinct non-null Key values.", "Warning");
                    return;
                }
            }

            _collectionControl.PersistChanges();
            this.DialogResult = true;
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            _collectionControl.PersistChanges(originalData);
            this.DialogResult = false;
            this.Close();
        }

        #endregion //Event Hanlders

        #region Private Methods

        [SecuritySafeCritical]
        private object Clone(object source)
        {
            if (source == null)
                return null;

            object result = null;
            Type sourceType = source.GetType();

            if (source is Array)
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    //var formatter = new BinaryFormatter();
                    //formatter.Serialize( stream, source );
                    //stream.Seek( 0, SeekOrigin.Begin );
                    //result = ( Array )formatter.Deserialize( stream );
                }
            }
            // For IDictionary, we need to create EditableKeyValuePair to edit the Key-Value.
            else if ((this.ItemsSource is IDictionary)
              && sourceType.IsGenericType
              && typeof(KeyValuePair<,>).IsAssignableFrom(sourceType.GetGenericTypeDefinition()))
            {
                result = this.GenerateEditableKeyValuePair(source);
            }
            else
            {
                // Initialized a new object with default values
                try
                {
                    result = FormatterServices.GetUninitializedObject(sourceType);
                }
                catch (Exception)
                {
                }

                System.Reflection.ConstructorInfo constructor = sourceType.GetConstructor(Type.EmptyTypes);
                if (constructor != null)
                {
                    constructor.Invoke(result, null);
                }
                else
                {
                    result = source;
                }
            }
            Debug.Assert(result != null);
            if (result != null)
            {
                System.Reflection.PropertyInfo[] properties = sourceType.GetProperties();

                foreach (System.Reflection.PropertyInfo propertyInfo in properties)
                {
                    System.Reflection.ParameterInfo[] parameters = propertyInfo.GetIndexParameters();
                    object[] index = parameters.GetLength(0) == 0 ? null : new object[] { parameters.GetLength(0) - 1 };
                    object propertyInfoValue = propertyInfo.GetValue(source, index);

                    if (propertyInfo.CanWrite)
                    {
                        // Look for nested object
                        if (propertyInfo.PropertyType.IsClass
                          && (propertyInfo.PropertyType != typeof(Transform))
                          && !propertyInfo.PropertyType.Equals(typeof(string)))
                        {
                            // We have a Collection/List of T.
                            if (propertyInfo.PropertyType.IsGenericType)
                            {
                                // Clone sub-objects if the T are non-primitive types objects. 
                                Type arg = propertyInfo.PropertyType.GetGenericArguments().FirstOrDefault();
                                if ((arg != null) && !arg.IsPrimitive && !arg.Equals(typeof(String)) && !arg.IsEnum)
                                {
                                    object nestedObject = this.Clone(propertyInfoValue);
                                    propertyInfo.SetValue(result, nestedObject, null);
                                }
                                else
                                {
                                    // copy object if the T are primitive types objects.
                                    propertyInfo.SetValue(result, propertyInfoValue, null);
                                }
                            }
                            else
                            {
                                object nestedObject = this.Clone(propertyInfoValue);
                                if (nestedObject != null)
                                {
                                    // For T object included in List/Collections, Add it to the List/Collection of T.
                                    if (index != null)
                                    {
                                        result.GetType().GetMethod("Add").Invoke(result, new[] { nestedObject });
                                    }
                                    else
                                    {
                                        propertyInfo.SetValue(result, nestedObject, null);
                                    }
                                }
                            }
                        }
                        else
                        {
                            // For T object included in List/Collections, Add it to the List/Collection of T.
                            if (index != null)
                            {
                                result.GetType().GetMethod("Add").Invoke(result, new[] { propertyInfoValue });
                            }
                            else
                            {
                                // copy regular object
                                propertyInfo.SetValue(result, propertyInfoValue, null);
                            }
                        }
                    }
                }
            }

            return result;
        }

        private object GenerateEditableKeyValuePair(object source)
        {
            Type sourceType = source.GetType();
            if ((sourceType.GetGenericArguments() == null) || (sourceType.GetGenericArguments().GetLength(0) != 2))
                return null;

            System.Reflection.PropertyInfo propInfoKey = sourceType.GetProperty("Key");
            System.Reflection.PropertyInfo propInfoValue = sourceType.GetProperty("Value");
            if ((propInfoKey != null) && (propInfoValue != null))
            {
                return ListUtilities.CreateEditableKeyValuePair(propInfoKey.GetValue(source, null)
                                                                  , sourceType.GetGenericArguments()[0]
                                                                  , propInfoValue.GetValue(source, null)
                                                                  , sourceType.GetGenericArguments()[1]);
            }
            return null;
        }

        private bool AreDictionaryKeysValid()
        {
            IEnumerable<object> keys = _collectionControl.Items.Select(x =>
            {
                System.Reflection.PropertyInfo keyType = x.GetType().GetProperty("Key");
                if (keyType != null)
                {
                    return keyType.GetValue(x, null);
                }
                return null;
            });

            return (keys.Distinct().Count() == _collectionControl.Items.Count)
                   && keys.All(x => x != null);
        }

        #endregion
    }
}
