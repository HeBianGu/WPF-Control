// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;
using System.Collections.Generic;

namespace H.Controls.PropertyGrid
{
    /// <summary>
    /// This attribute can decorate the collection properties (i.e., IList) 
    /// of your selected object in order to control the types that will be allowed
    /// to be instantiated in the CollectionControl.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class NewItemTypesAttribute : Attribute
    {
        public IList<Type> Types
        {
            get;
            set;
        }

        public NewItemTypesAttribute(params Type[] types)
        {
            this.Types = new List<Type>(types);
        }

        public NewItemTypesAttribute()
        {
        }
    }
}
