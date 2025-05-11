// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.Form.PropertyItem.ObservableCollectionPropertyItems.Base;

namespace H.Controls.Form.PropertyItem.ObservableCollectionPropertyItems
{
    public class PrimitiveListPropertyItem : PrimitivesPropertyItemBase
    {
        public PrimitiveListPropertyItem(PropertyInfo property, object obj) : base(property, obj)
        {

        }

        protected override Type GetElementType()
        {
            return this.PropertyInfo.PropertyType.GetGenericArguments().FirstOrDefault();
        }

        protected override void SetValue(ObservableCollection<StringHost> value)
        {
            object to = this.ChangeType(value);

            this.PropertyInfo.SetValue(this.Obj, to);
        }

        private object ChangeType(ObservableCollection<StringHost> value)
        {
            Type type = this.PropertyInfo.PropertyType.GetGenericTypeDefinition();

            Type[] agrs = this.PropertyInfo.PropertyType.GetGenericArguments();

            Type elementType = agrs.FirstOrDefault();

            Type sfs = type.MakeGenericType(elementType);

            IList list = (IList)Activator.CreateInstance(sfs);

            foreach (StringHost item in value)
            {
                object v = Convert.ChangeType(item.Value, elementType);

                list.Add(v);
            }

            return list;
        }

    }
}
