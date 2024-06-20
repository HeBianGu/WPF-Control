// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

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
            Type valueSourceInterface = type.GetInterface(typeof(IItemsSource).FullName);
            if (valueSourceInterface == null)
                throw new ArgumentException("Type must implement the IItemsSource interface.", "type");

            this.Type = type;
        }
    }
}
