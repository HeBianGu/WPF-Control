// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.FilterBox
{
    public class DateTimePropertyFilter : ComparablePropertyFilterBase<DateTime>
    {
        public DateTimePropertyFilter()
        {

        }
        public DateTimePropertyFilter(PropertyInfo property) : base(property)
        {
            this.Value = DateTime.Now;
        }

        private bool _onlyDate = true;
        /// <summary> 仅比较日期 </summary>
        public bool OnlyDate
        {
            get { return _onlyDate; }
            set
            {
                _onlyDate = value;
                RaisePropertyChanged("OnlyDate");
            }
        }

        public override IFilterable Copy()
        {
            return new DateTimePropertyFilter(this.PropertyInfo) { Operate = this.Operate, Value = this.Value };
        }

        public override DateTime ConvertValue()
        {
            return this.OnlyDate ? this.Value.Date : this.Value;
        }

    }
}
