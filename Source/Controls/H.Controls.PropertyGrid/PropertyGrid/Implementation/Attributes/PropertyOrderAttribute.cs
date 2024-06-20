// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;

namespace H.Controls.PropertyGrid
{
    public enum UsageContextEnum
    {
        Alphabetical,
        Categorized,
        Both
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public class PropertyOrderAttribute : Attribute
    {
        #region Properties

        public int Order
        {
            get;
            set;
        }

        public UsageContextEnum UsageContext
        {
            get;
            set;
        }

        public override object TypeId
        {
            get
            {
                return this;
            }
        }

        #endregion

        #region Initialization

        public PropertyOrderAttribute(int order)
          : this(order, UsageContextEnum.Both)
        {
        }

        public PropertyOrderAttribute(int order, UsageContextEnum usageContext)
        {
            this.Order = order;
            this.UsageContext = usageContext;
        }

        #endregion
    }
}
