// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control
using H.Controls.Form.PropertyItem.Base;
using System.Collections.Generic;
using System.Reflection;

namespace H.Controls.Form.PropertyItem.ComboBoxPropertyItems
{
    public class EnumComboBoxPropertyItem : SelectSourcePropertyItem<Enum>
    {
        public EnumComboBoxPropertyItem(PropertyInfo property, object obj) : base(property, obj)
        {

        }

        protected override IEnumerable<Enum> CreateSource()
        {
            if (!this.PropertyInfo.PropertyType.IsEnum)
                yield break;
            Array result = this.PropertyInfo.PropertyType.GetEnumValues();
            foreach (object item in result)
            {
                if (item is Enum @enum)
                    yield return @enum;
            }
        }
    }
}
