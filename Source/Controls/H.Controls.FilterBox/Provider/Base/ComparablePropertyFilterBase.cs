// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;
using System.Reflection;
using System.Reflection.Metadata;

namespace H.Controls.FilterBox
{

    public abstract class ComparablePropertyFilterBase<T> : PropertyFilterBase<T> where T : IComparable
    {
        public ComparablePropertyFilterBase()
        {

        }
        public ComparablePropertyFilterBase(PropertyInfo property) : base(property)
        {
            this.Operate = FilterOperate.Equals;
        }

        public override bool IsMatch(object obj)
        {
            PropertyInfo p = obj.GetType().GetProperty(this.PropertyName);

            if (p == null || !p.CanRead) return false;

            T v = (T)p.GetValue(obj);

            if (this.Operate == FilterOperate.Equals)
            {
                return v.CompareTo(this.ConvertValue()) == 0;
            }
            else if (this.Operate == FilterOperate.UnEquals)
            {
                return v.CompareTo(this.ConvertValue()) != 0;
            }
            else if (this.Operate == FilterOperate.Greater)
            {
                return v.CompareTo(this.ConvertValue()) > 0;
            }
            else if (this.Operate == FilterOperate.Less)
            {
                return v.CompareTo(this.ConvertValue()) < 0;
            }
            else if (this.Operate == FilterOperate.GreaterAndEqual)
            {
                return v.CompareTo(this.ConvertValue()) >= 0;
            }
            else if (this.Operate == FilterOperate.LessAndEqual)
            {
                return v.CompareTo(this.ConvertValue()) <= 0;
            }
            else
            {
                return false;
            }
        }

        public virtual T ConvertValue()
        {
            return this.Value;
        }
    }
}
