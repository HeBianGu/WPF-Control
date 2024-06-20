﻿// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace H.Controls.PropertyGrid
{
    [TemplatePart(Name = PART_NewItemTypesComboBox, Type = typeof(ComboBox))]
    [TemplatePart(Name = PART_PropertyGrid, Type = typeof(PropertyGrid))]
    [TemplatePart(Name = PART_ListBox, Type = typeof(ListBox))]
    public class CollectionControl : System.Windows.Controls.Control
    {
        private const string PART_NewItemTypesComboBox = "PART_NewItemTypesComboBox";
        private const string PART_PropertyGrid = "PART_PropertyGrid";
        private const string PART_ListBox = "PART_ListBox";

        #region Private Members

        private ComboBox _newItemTypesComboBox;
        private PropertyGrid _propertyGrid;
        private ListBox _listBox;

        #endregion

        #region Properties

        #region IsReadOnly Property

        public static readonly DependencyProperty IsReadOnlyProperty = DependencyProperty.Register("IsReadOnly", typeof(bool), typeof(CollectionControl), new UIPropertyMetadata(false));
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

        #endregion  //Items

        #region Items Property

        public static readonly DependencyProperty ItemsProperty = DependencyProperty.Register("Items", typeof(ObservableCollection<object>), typeof(CollectionControl), new UIPropertyMetadata(null));
        public ObservableCollection<object> Items
        {
            get
            {
                return (ObservableCollection<object>)GetValue(ItemsProperty);
            }
            set
            {
                SetValue(ItemsProperty, value);
            }
        }

        #endregion  //Items

        #region ItemsSource Property

        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(CollectionControl), new UIPropertyMetadata(null, OnItemsSourceChanged));
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

        private static void OnItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CollectionControl CollectionControl = (CollectionControl)d;
            if (CollectionControl != null)
                CollectionControl.OnItemSourceChanged((IEnumerable)e.OldValue, (IEnumerable)e.NewValue);
        }

        public void OnItemSourceChanged(IEnumerable oldValue, IEnumerable newValue)
        {
            if (newValue != null)
            {
                IDictionary dict = newValue as IDictionary;
                if (dict != null)
                {
                    // A Dictionary contains KeyValuePair that can't be edited.
                    // We need to Add EditableKeyValuePairs from DictionaryEntries.
                    foreach (DictionaryEntry item in dict)
                    {
                        Type keyType = (item.Key != null)
                                      ? item.Key.GetType()
                                      : (dict.GetType().GetGenericArguments().Count() > 0) ? dict.GetType().GetGenericArguments()[0] : typeof(object);
                        Type valueType = (item.Value != null)
                                      ? item.Value.GetType()
                                      : (dict.GetType().GetGenericArguments().Count() > 1) ? dict.GetType().GetGenericArguments()[1] : typeof(object);
                        object editableKeyValuePair = ListUtilities.CreateEditableKeyValuePair(item.Key
                                                                                            , keyType
                                                                                            , item.Value
                                                                                            , valueType);
                        this.Items.Add(editableKeyValuePair);
                    }
                }
                else
                {
                    foreach (object item in newValue)
                    {
                        if (item != null)
                        {
                            this.Items.Add(item);
                        }
                    }
                }
            }
        }

        #endregion  //ItemsSource

        #region ItemsSourceType Property

        public static readonly DependencyProperty ItemsSourceTypeProperty = DependencyProperty.Register("ItemsSourceType", typeof(Type), typeof(CollectionControl), new UIPropertyMetadata(null));
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

        #endregion //ItemsSourceType

        #region NewItemType Property

        public static readonly DependencyProperty NewItemTypesProperty = DependencyProperty.Register("NewItemTypes", typeof(IList), typeof(CollectionControl), new UIPropertyMetadata(null));
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

        #endregion  //NewItemType

        #region SelectedItem Property

        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register("SelectedItem", typeof(object), typeof(CollectionControl), new UIPropertyMetadata(null));
        public object SelectedItem
        {
            get
            {
                return GetValue(SelectedItemProperty);
            }
            set
            {
                SetValue(SelectedItemProperty, value);
            }
        }

        #endregion  //SelectedItem

        #endregion //Properties

        #region Override Methods

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (_newItemTypesComboBox != null)
            {
                _newItemTypesComboBox.Loaded -= new RoutedEventHandler(this.NewItemTypesComboBox_Loaded);
            }
            _newItemTypesComboBox = GetTemplateChild(PART_NewItemTypesComboBox) as ComboBox;
            if (_newItemTypesComboBox != null)
            {
                _newItemTypesComboBox.Loaded += new RoutedEventHandler(this.NewItemTypesComboBox_Loaded);
            }

            _listBox = this.GetTemplateChild(PART_ListBox) as ListBox;

            if (_propertyGrid != null)
            {
                _propertyGrid.PropertyValueChanged -= this.PropertyGrid_PropertyValueChanged;
            }
            _propertyGrid = GetTemplateChild(PART_PropertyGrid) as PropertyGrid;
            if (_propertyGrid != null)
            {
                _propertyGrid.PropertyValueChanged += this.PropertyGrid_PropertyValueChanged;
            }
        }

        public PropertyGrid PropertyGrid
        {
            get
            {
                if (_propertyGrid == null)
                {
                    this.ApplyTemplate();
                }
                return _propertyGrid;
            }
        }




        #endregion

        #region Constructors

        static CollectionControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CollectionControl), new FrameworkPropertyMetadata(typeof(CollectionControl)));
        }

        public CollectionControl()
        {
            this.Items = new ObservableCollection<object>();
            this.CommandBindings.Add(new CommandBinding(ApplicationCommands.New, AddNew, CanAddNew));
            this.CommandBindings.Add(new CommandBinding(ApplicationCommands.Delete, Delete, CanDelete));
            this.CommandBindings.Add(new CommandBinding(ComponentCommands.MoveDown, MoveDown, CanMoveDown));
            this.CommandBindings.Add(new CommandBinding(ComponentCommands.MoveUp, MoveUp, CanMoveUp));
        }

        #endregion //Constructors

        #region Events

        #region ItemDeleting Event

        public delegate void ItemDeletingRoutedEventHandler(object sender, ItemDeletingEventArgs e);

        public static readonly RoutedEvent ItemDeletingEvent = EventManager.RegisterRoutedEvent("ItemDeleting", RoutingStrategy.Bubble, typeof(ItemDeletingRoutedEventHandler), typeof(CollectionControl));
        public event ItemDeletingRoutedEventHandler ItemDeleting
        {
            add
            {
                AddHandler(ItemDeletingEvent, value);
            }
            remove
            {
                RemoveHandler(ItemDeletingEvent, value);
            }
        }

        #endregion //ItemDeleting Event

        #region ItemDeleted Event

        public delegate void ItemDeletedRoutedEventHandler(object sender, ItemEventArgs e);

        public static readonly RoutedEvent ItemDeletedEvent = EventManager.RegisterRoutedEvent("ItemDeleted", RoutingStrategy.Bubble, typeof(ItemDeletedRoutedEventHandler), typeof(CollectionControl));
        public event ItemDeletedRoutedEventHandler ItemDeleted
        {
            add
            {
                AddHandler(ItemDeletedEvent, value);
            }
            remove
            {
                RemoveHandler(ItemDeletedEvent, value);
            }
        }

        #endregion //ItemDeleted Event

        #region ItemAdding Event

        public delegate void ItemAddingRoutedEventHandler(object sender, ItemAddingEventArgs e);

        public static readonly RoutedEvent ItemAddingEvent = EventManager.RegisterRoutedEvent("ItemAdding", RoutingStrategy.Bubble, typeof(ItemAddingRoutedEventHandler), typeof(CollectionControl));
        public event ItemAddingRoutedEventHandler ItemAdding
        {
            add
            {
                AddHandler(ItemAddingEvent, value);
            }
            remove
            {
                RemoveHandler(ItemAddingEvent, value);
            }
        }

        #endregion //ItemAdding Event

        #region ItemAdded Event

        public delegate void ItemAddedRoutedEventHandler(object sender, ItemEventArgs e);

        public static readonly RoutedEvent ItemAddedEvent = EventManager.RegisterRoutedEvent("ItemAdded", RoutingStrategy.Bubble, typeof(ItemAddedRoutedEventHandler), typeof(CollectionControl));
        public event ItemAddedRoutedEventHandler ItemAdded
        {
            add
            {
                AddHandler(ItemAddedEvent, value);
            }
            remove
            {
                RemoveHandler(ItemAddedEvent, value);
            }
        }

        #endregion //ItemAdded Event

        #region ItemMovedDown Event

        public delegate void ItemMovedDownRoutedEventHandler(object sender, ItemEventArgs e);

        public static readonly RoutedEvent ItemMovedDownEvent = EventManager.RegisterRoutedEvent("ItemMovedDown", RoutingStrategy.Bubble, typeof(ItemMovedDownRoutedEventHandler), typeof(CollectionControl));
        public event ItemMovedDownRoutedEventHandler ItemMovedDown
        {
            add
            {
                AddHandler(ItemMovedDownEvent, value);
            }
            remove
            {
                RemoveHandler(ItemMovedDownEvent, value);
            }
        }

        #endregion //ItemMovedDown Event

        #region ItemMovedUp Event

        public delegate void ItemMovedUpRoutedEventHandler(object sender, ItemEventArgs e);

        public static readonly RoutedEvent ItemMovedUpEvent = EventManager.RegisterRoutedEvent("ItemMovedUp", RoutingStrategy.Bubble, typeof(ItemMovedUpRoutedEventHandler), typeof(CollectionControl));
        public event ItemMovedUpRoutedEventHandler ItemMovedUp
        {
            add
            {
                AddHandler(ItemMovedUpEvent, value);
            }
            remove
            {
                RemoveHandler(ItemMovedUpEvent, value);
            }
        }

        #endregion //ItemMovedUp Event

        #endregion

        #region EventHandlers

        private void NewItemTypesComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            if (_newItemTypesComboBox != null)
                _newItemTypesComboBox.SelectedIndex = 0;
        }

        private void PropertyGrid_PropertyValueChanged(object sender, PropertyValueChangedEventArgs e)
        {
            if (_listBox != null)
            {
                _listBox.Items.Refresh();
            }
        }

        #endregion

        #region Commands

        private void AddNew(object sender, ExecutedRoutedEventArgs e)
        {
            object newItem = CreateNewItem((Type)e.Parameter);
            System.Reflection.PropertyInfo[] properties = newItem.GetType().GetProperties();
            foreach (System.Reflection.PropertyInfo property in properties)
            {
                // For Generic Types, add an empty collection/list of T.
                if (property.CanWrite && property.PropertyType.IsGenericType)
                {
                    object genericCollection = Activator.CreateInstance(property.PropertyType);
                    property.SetValue(newItem, genericCollection, null);
                }
            }

            ItemAddingEventArgs eventArgs = new ItemAddingEventArgs(ItemAddingEvent, newItem);
            this.RaiseEvent(eventArgs);
            if (eventArgs.Cancel)
                return;
            newItem = eventArgs.Item;

            this.Items.Add(newItem);

            this.RaiseEvent(new ItemEventArgs(ItemAddedEvent, newItem));

            this.SelectedItem = newItem;
        }

        private void CanAddNew(object sender, CanExecuteRoutedEventArgs e)
        {
            Type t = e.Parameter as Type;
            if ((t != null) && !this.IsReadOnly)
            {
                bool isComplexStruct = t.IsValueType && !t.IsEnum && !t.IsPrimitive;

                if (isComplexStruct || (t.GetConstructor(Type.EmptyTypes) != null))
                {
                    e.CanExecute = true;
                }
            }
        }

        private void Delete(object sender, ExecutedRoutedEventArgs e)
        {
            ItemDeletingEventArgs eventArgs = new ItemDeletingEventArgs(ItemDeletingEvent, e.Parameter);
            this.RaiseEvent(eventArgs);
            if (eventArgs.Cancel)
                return;

            this.Items.Remove(e.Parameter);

            this.RaiseEvent(new ItemEventArgs(ItemDeletedEvent, e.Parameter));
        }

        private void CanDelete(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = e.Parameter != null && !this.IsReadOnly;
        }

        private void MoveDown(object sender, ExecutedRoutedEventArgs e)
        {
            object selectedItem = e.Parameter;
            int index = this.Items.IndexOf(selectedItem);
            this.Items.RemoveAt(index);
            this.Items.Insert(++index, selectedItem);

            this.RaiseEvent(new ItemEventArgs(ItemMovedDownEvent, selectedItem));

            this.SelectedItem = selectedItem;
        }

        private void CanMoveDown(object sender, CanExecuteRoutedEventArgs e)
        {
            if (e.Parameter != null && this.Items.IndexOf(e.Parameter) < (this.Items.Count - 1) && !this.IsReadOnly)
                e.CanExecute = true;
        }

        private void MoveUp(object sender, ExecutedRoutedEventArgs e)
        {
            object selectedItem = e.Parameter;
            int index = this.Items.IndexOf(selectedItem);
            this.Items.RemoveAt(index);
            this.Items.Insert(--index, selectedItem);

            this.RaiseEvent(new ItemEventArgs(ItemMovedUpEvent, selectedItem));

            this.SelectedItem = selectedItem;
        }

        private void CanMoveUp(object sender, CanExecuteRoutedEventArgs e)
        {
            if (e.Parameter != null && this.Items.IndexOf(e.Parameter) > 0 && !this.IsReadOnly)
                e.CanExecute = true;
        }

        #endregion //Commands

        #region Methods

        public void PersistChanges()
        {
            this.PersistChanges(this.Items);
        }

        internal void PersistChanges(IList sourceList)
        {
            IEnumerable collection = ComputeItemsSource();
            if (collection == null)
                return;

            //IDictionary<T> and IDictionary
            if (collection is IDictionary)
            {
                //For a Dictionary, we need to parse the list of EditableKeyValuePair and add KeyValuePair to the Dictionary.
                IDictionary dict = (IDictionary)collection;
                //the easiest way to persist changes to the source is to just clear the source list and then add all items to it.
                dict.Clear();

                foreach (object item in sourceList)
                {
                    System.Reflection.PropertyInfo propInfoKey = item.GetType().GetProperty("Key");
                    System.Reflection.PropertyInfo propInfoValue = item.GetType().GetProperty("Value");
                    if ((propInfoKey != null) && (propInfoValue != null))
                    {
                        dict.Add(propInfoKey.GetValue(item, null), propInfoValue.GetValue(item, null));
                    }
                }
            }
            //IList
            else if (collection is IList)
            {
                IList list = (IList)collection;

                //the easiest way to persist changes to the source is to just clear the source list and then add all items to it.
                list.Clear();

                if (list.IsFixedSize)
                {
                    if (sourceList.Count > list.Count)
                        throw new IndexOutOfRangeException("Exceeding array size.");

                    for (int i = 0; i < sourceList.Count; ++i)
                        list[i] = sourceList[i];
                }
                else
                {
                    foreach (object item in sourceList)
                    {
                        list.Add(item);
                    }
                }
            }
            else
            {
                //ICollection<T> (or IList<T>)
                Type collectionType = collection.GetType();
                Type iCollectionOfTInterface = collectionType.GetInterfaces().FirstOrDefault(x => x.IsGenericType && (x.GetGenericTypeDefinition() == typeof(ICollection<>)));
                if (iCollectionOfTInterface != null)
                {
                    Type argumentType = iCollectionOfTInterface.GetGenericArguments().FirstOrDefault();
                    if (argumentType != null)
                    {
                        Type iCollectionOfTType = typeof(ICollection<>).MakeGenericType(argumentType);

                        //the easiest way to persist changes to the source is to just clear the source list and then add all items to it.
                        iCollectionOfTType.GetMethod("Clear").Invoke(collection, null);

                        foreach (object item in sourceList)
                        {
                            iCollectionOfTType.GetMethod("Add").Invoke(collection, new object[] { item });
                        }
                    }
                }
            }
        }

        private IEnumerable CreateItemsSource()
        {
            IEnumerable collection = null;

            if (this.ItemsSourceType != null)
            {
                System.Reflection.ConstructorInfo constructor = this.ItemsSourceType.GetConstructor(Type.EmptyTypes);
                if (constructor != null)
                {
                    collection = (IEnumerable)constructor.Invoke(null);
                }
                else if (this.ItemsSourceType.IsArray)
                {
                    collection = Array.CreateInstance(this.ItemsSourceType.GetElementType(), this.Items.Count);
                }
            }

            return collection;
        }

        private object CreateNewItem(Type type)
        {
            return Activator.CreateInstance(type);
        }

        private IEnumerable ComputeItemsSource()
        {
            if (this.ItemsSource == null)
                this.ItemsSource = CreateItemsSource();

            return this.ItemsSource;
        }

        #endregion //Methods
    }
}
