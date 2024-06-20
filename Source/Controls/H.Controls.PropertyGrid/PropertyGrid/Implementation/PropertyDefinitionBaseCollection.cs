// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace H.Controls.PropertyGrid
{
    public class PropertyDefinitionCollection : PropertyDefinitionBaseCollection<PropertyDefinition>
    {
    }
    public class EditorDefinitionCollection : PropertyDefinitionBaseCollection<EditorDefinitionBase>
    {
    }


    public abstract class PropertyDefinitionBaseCollection<T> : DefinitionCollectionBase<T> where T : PropertyDefinitionBase
    {
        protected PropertyDefinitionBaseCollection()
        {
        }

        public virtual T this[object propertyId]
        {
            get
            {
                foreach (T item in this.Items)
                {
                    if (item.TargetProperties.Contains(propertyId))
                        return item;

                    // Using the special character "*" in a string of TargetProperties will
                    // return all the items containing the string (before or after) the "*".
                    // ex : Prop* will return properties named Prop1, Prop2, Prop3...
                    List<string> stringTargetProperties = item.TargetProperties.OfType<string>().ToList();
                    if ((stringTargetProperties != null) && (stringTargetProperties.Count > 0))
                    {
                        if (propertyId is string)
                        {
                            string stringPropertyID = (string)propertyId;
                            foreach (string targetPropertyString in stringTargetProperties)
                            {
                                if (targetPropertyString.Contains("*"))
                                {
                                    string searchString = targetPropertyString.Replace("*", "");
                                    if (stringPropertyID.StartsWith(searchString) || stringPropertyID.EndsWith(searchString))
                                        return item;
                                }
                            }
                        }
                    }
                    else
                    {
                        //Manage Interfaces
                        Type type = propertyId as Type;
                        if (type != null)
                        {
                            foreach (Type targetProperty in item.TargetProperties)
                            {
                                if (targetProperty.IsAssignableFrom(type))
                                    return item;
                            }
                        }
                    }
                }

                return null;
            }
        }

        internal T GetRecursiveBaseTypes(Type type)
        {
            // If no definition for the current type, fall back on base type editor recursively.
            T ret = null;
            while (ret == null && type != null)
            {
                ret = this[type];
                type = type.BaseType;
            }
            return ret;
        }
    }

    public abstract class DefinitionCollectionBase<T> : ObservableCollection<T> where T : DefinitionBase
    {
        internal DefinitionCollectionBase()
        {
        }

        protected override void InsertItem(int index, T item)
        {
            if (item == null)
                throw new InvalidOperationException(@"Cannot insert null items in the collection.");

            item.Lock();
            base.InsertItem(index, item);
        }

        protected override void SetItem(int index, T item)
        {
            if (item == null)
                throw new InvalidOperationException(@"Cannot insert null items in the collection.");

            item.Lock();
            base.SetItem(index, item);
        }
    }
}
