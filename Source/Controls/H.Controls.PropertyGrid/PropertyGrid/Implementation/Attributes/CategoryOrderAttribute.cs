﻿// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;

namespace H.Controls.PropertyGrid
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class CategoryOrderAttribute : Attribute
    {
        #region Properties

        #region Order

        public int Order
        {
            get;
            set;
        }

        #endregion

        #region Category

        public virtual string Category
        {
            get
            {
                return CategoryValue;
            }
        }

        #endregion

        #region CategoryValue

        public string CategoryValue
        {
            get;
            private set;
        }

        #endregion

        #endregion

        #region constructor

        public CategoryOrderAttribute()
        {
        }

        public CategoryOrderAttribute(string categoryName, int order)
          : this()
        {
            CategoryValue = categoryName;
            Order = order;
        }

        #endregion
    }
}

