// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace H.Controls.FilterBox
{
    public class StringPropertyFilter : PropertyFilterBase<string>
    {
        public StringPropertyFilter()
        {

        }
        public StringPropertyFilter(PropertyInfo property) : base(property)
        {
            this.Operate = FilterOperate.Equals;

        }

        public StringPropertyFilter(PropertyInfo property, IEnumerable source) : base(property, source)
        {
            this.Operate = FilterOperate.Equals;
        }

        private bool _ordinalIgnoreCase;
        public bool OrdinalIgnoreCase
        {
            get { return _ordinalIgnoreCase; }
            set
            {
                _ordinalIgnoreCase = value;
                RaisePropertyChanged();
            }
        }

        public override IFilterable Copy()
        {
            StringPropertyFilter result = new StringPropertyFilter(this.PropertyInfo)
            {
                Operate = this.Operate,
                Value = this.Value,
                Source = this.Source,
                SelectedSource = new ObservableCollection<string>(this.SelectedSource)
            };

            if (this.Operate == FilterOperate.SelectSource)
            {
                result.Value = $"<{string.Join(",", this.SelectedSource)}>";
            }

            return result;
        }

        public override bool IsMatch(object obj)
        {
            PropertyInfo p = obj.GetType().GetProperty(this.PropertyName);
            if (p == null || !p.CanRead)
                return false;

            string v = p.GetValue(obj)?.ToString();
            if (this.Operate == FilterOperate.Contain)
            {
                if (string.IsNullOrEmpty(this.Value))
                    return true;
                return this.OrdinalIgnoreCase ? v.IndexOf(this.Value, StringComparison.OrdinalIgnoreCase) >= 0 : v.Contains(this.Value);
            }
            else if (this.Operate == FilterOperate.UnContain)
            {
                if (string.IsNullOrEmpty(this.Value))
                    return true;
                return this.OrdinalIgnoreCase ? !(v.IndexOf(this.Value, StringComparison.OrdinalIgnoreCase) >= 0) : !v.Contains(this.Value);
            }
            else if (this.Operate == FilterOperate.Equals)
            {
                if (string.IsNullOrEmpty(this.Value))
                    return string.IsNullOrEmpty(v);
                return string.Compare(v, this.Value, this.OrdinalIgnoreCase) == 0;
            }
            else if (this.Operate == FilterOperate.UnEquals)
            {
                if (string.IsNullOrEmpty(this.Value))
                    return !string.IsNullOrEmpty(v);
                return string.Compare(v, this.Value, this.OrdinalIgnoreCase) != 0;
            }
            else if (this.Operate == FilterOperate.SelectSource)
            {
                //  Do ：没有勾选默认表示全选
                if (this.SelectedSource.Count == 0)
                    return true;

                return this.SelectedSource.Any(l => l == v);
            }
            else if (this.Operate == FilterOperate.Setted)
            {
                return v != null;
            }
            else if (this.Operate == FilterOperate.StartWith)
            {
                return this.OrdinalIgnoreCase ? v.StartsWith(this.Value, StringComparison.OrdinalIgnoreCase) : v.StartsWith(this.Value);
            }
            else if (this.Operate == FilterOperate.EndWith)
            {
                return this.OrdinalIgnoreCase ? v.EndsWith(this.Value, StringComparison.OrdinalIgnoreCase) : v.EndsWith(this.Value);
            }
            else if (this.Operate == FilterOperate.Unset)
            {
                return v == null;
            }
            else
            {
                return false;
            }
        }
    }
}
