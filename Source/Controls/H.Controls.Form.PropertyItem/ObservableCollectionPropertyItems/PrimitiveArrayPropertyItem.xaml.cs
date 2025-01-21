// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using H.Controls.Form.PropertyItem.ObservableCollectionPropertyItems.Base;
using System.Collections.ObjectModel;
using System.Reflection;

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
