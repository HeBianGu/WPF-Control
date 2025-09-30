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
    /// <summary> 集合类型 </summary>
    public class PrimitiveArrayPropertyItem : PrimitivesPropertyItemBase
    {
        public PrimitiveArrayPropertyItem(PropertyInfo property, object obj) : base(property, obj)
        {

        }

        protected override Type GetElementType()
        {
            return this.PropertyInfo.PropertyType.GetElementType();
        }

        protected override void SetValue(ObservableCollection<StringHost> value)
        {
            object to = this.ChangeType(value);

            this.PropertyInfo.SetValue(this.Obj, to);
        }

        private object ChangeType(ObservableCollection<StringHost> value)
        {
            Type elementType = this.PropertyInfo.PropertyType.GetElementType();

            Array array = Array.CreateInstance(elementType, value.Count);

            for (int i = 0; i < value.Count; i++)
            {
                string item = value[i].Value;

                object v = Convert.ChangeType(item, elementType);

                array.SetValue(v, i);
            }

            return array;
        }
    }
}
