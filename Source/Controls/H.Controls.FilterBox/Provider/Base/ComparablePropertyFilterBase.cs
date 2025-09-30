// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

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
