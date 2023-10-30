// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;

namespace H.Controls.PropertyGrid
{
    public class ItemsSourceAttribute : Attribute
    {
        public Type Type
        {
            get;
            set;
        }

        public ItemsSourceAttribute(Type type)
        {
            var valueSourceInterface = type.GetInterface(typeof(IItemsSource).FullName);
            if (valueSourceInterface == null)
                throw new ArgumentException("Type must implement the IItemsSource interface.", "type");

            Type = type;
        }
    }
}
