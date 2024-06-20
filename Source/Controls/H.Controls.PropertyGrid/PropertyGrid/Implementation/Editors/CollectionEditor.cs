// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;
using System.Collections.Generic;
using System.Windows;


namespace H.Controls.PropertyGrid
{
    public class CollectionEditor : TypeEditor<CollectionControlButton>
    {
        protected override void SetValueDependencyProperty()
        {
            this.ValueProperty = CollectionControlButton.ItemsSourceProperty;
        }

        protected override CollectionControlButton CreateEditor()
        {
            return new PropertyGridEditorCollectionControl();
        }


        protected override void ResolveValueBinding(PropertyItem propertyItem)
        {
            Type type = propertyItem.PropertyType;

            this.Editor.ItemsSourceType = type;

            if (type.BaseType == typeof(System.Array))
            {
                this.Editor.NewItemTypes = new List<Type>() { type.GetElementType() };
            }
            else
            {
                if ((propertyItem.DescriptorDefinition != null)
                    && (propertyItem.DescriptorDefinition.NewItemTypes != null)
                    && (propertyItem.DescriptorDefinition.NewItemTypes.Count > 0))
                {
                    this.Editor.NewItemTypes = propertyItem.DescriptorDefinition.NewItemTypes;
                }
                else
                {
                    //Check if we have a Dictionary
                    Type[] dictionaryTypes = ListUtilities.GetDictionaryItemsType(type);
                    if ((dictionaryTypes != null) && (dictionaryTypes.Length == 2))
                    {
                        // A Dictionary contains KeyValuePair that can't be edited.
                        // We need to create EditableKeyValuePairs.
                        // Create a EditableKeyValuePair< TKey, TValue> type from dictionary generic arguments type
                        Type editableKeyValuePairType = ListUtilities.CreateEditableKeyValuePairType(dictionaryTypes[0], dictionaryTypes[1]);
                        this.Editor.NewItemTypes = new List<Type>() { editableKeyValuePairType };
                    }
                    else
                    {
                        //Check if we have a list
                        Type listType = ListUtilities.GetListItemType(type);
                        if (listType != null)
                        {
                            this.Editor.NewItemTypes = new List<Type>() { listType };
                        }
                        else
                        {
                            //Check if we have a Collection of T
                            Type colType = ListUtilities.GetCollectionItemType(type);
                            if (colType != null)
                            {
                                this.Editor.NewItemTypes = new List<Type>() { colType };
                            }
                        }
                    }
                }
            }

            base.ResolveValueBinding(propertyItem);
        }

    }

    public class PropertyGridEditorCollectionControl : CollectionControlButton
    {
        static PropertyGridEditorCollectionControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PropertyGridEditorCollectionControl), new FrameworkPropertyMetadata(typeof(PropertyGridEditorCollectionControl)));
        }
    }
}
