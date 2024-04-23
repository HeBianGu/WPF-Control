// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;
using System.Collections.Generic;
using System.Linq;


namespace H.Controls.PropertyGrid
{
    internal class ListUtilities
    {
        internal static Type GetListItemType(Type listType)
        {
            Type iListOfT = listType.GetInterfaces().FirstOrDefault(
              (i) => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IList<>));

            return (iListOfT != null)
              ? iListOfT.GetGenericArguments()[0]
              : null;
        }

        internal static Type GetCollectionItemType(Type colType)
        {
            Type iCollectionOfT = null;
            bool isCollectionOfT = colType.IsGenericType && (colType.GetGenericTypeDefinition() == typeof(ICollection<>));
            if (isCollectionOfT)
            {
                iCollectionOfT = colType;
            }
            else
            {
                iCollectionOfT = colType.GetInterfaces().FirstOrDefault((i) => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ICollection<>));
            }

            return (iCollectionOfT != null)
              ? iCollectionOfT.GetGenericArguments()[0]
              : null;
        }

        internal static Type[] GetDictionaryItemsType(Type dictType)
        {
            bool isDict = dictType.IsGenericType
              && ((dictType.GetGenericTypeDefinition() == typeof(Dictionary<,>)) || (dictType.GetGenericTypeDefinition() == typeof(IDictionary<,>)));

            return isDict
              ? new Type[] { dictType.GetGenericArguments()[0], dictType.GetGenericArguments()[1] }
              : null;
        }

        internal static object CreateEditableKeyValuePair(object key, Type keyType, object value, Type valueType)
        {
            Type itemType = ListUtilities.CreateEditableKeyValuePairType(keyType, valueType);
            return Activator.CreateInstance(itemType, key, value);
        }

        internal static Type CreateEditableKeyValuePairType(Type keyType, Type valueType)
        {
            //return an EditableKeyValuePair< TKey, TValue> Type from keyType and valueType
            Type itemGenType = typeof(EditableKeyValuePair<,>);
            Type[] itemGenTypeArgs = { keyType, valueType };
            return itemGenType.MakeGenericType(itemGenTypeArgs);
        }
    }
}
