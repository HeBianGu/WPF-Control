using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace H.Extensions.Unit
{
    public abstract class UnitableBase<T> : IUnitable<T>, IUnitable, IDigits where T : IComparable<T>
    {
        protected readonly Dictionary<T, List<string>> _map = new Dictionary<T, List<string>>();
        public UnitableBase()
        {
            _map = CreateMap();
        }

        protected abstract Dictionary<T, List<string>> CreateMap();

        //public abstract T ToValue(string str);
        //public abstract string ToString(T value);

        public virtual int Digits { get; set; } = 15;

        protected virtual string ToStringFormat(string symbol, string value, string unit)
        {
            return string.Format("{0}{1} {2}", symbol, value, unit);
        }

        protected virtual bool IsValid(string str)
        {
            if (string.IsNullOrEmpty(str))
                return false;
            return true;
        }

        public virtual T Parse(string str)
        {
            if (IsValid(str) == false)
                return default;

            str = str.Trim();

            // 定义正则表达式模式，将非数字部分作为分隔符进行拆分
            string pattern = @"([\u4e00-\u9fa5]|[a-zA-Z]+)";

            // 调用 Regex.Split() 函数进行拆分
            string[] result = Regex.Split(str, pattern, RegexOptions.IgnorePatternWhitespace).Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();

            if (result.Length == 0)
                return default;

            object t;

            try
            {
                t = Convert.ChangeType(result[0], typeof(double));
            }
            catch (Exception)
            {
                return default;
            }
            if (t == null)
                return default;

            if (result.Length > 1)
            {
                KeyValuePair<T, List<string>>? unit = _map.FirstOrDefault(x => x.Value.Contains(result[1], StringComparer.OrdinalIgnoreCase));
                if (unit != null)
                    return Parse((double)t, unit.Value.Key);
            }
            return (T)Convert.ChangeType(t, typeof(T));
        }

        protected abstract T Parse(double value, T unit);

        public virtual string ToString(T value)
        {
            bool isMinus = value.CompareTo(default) < 0;
            value = ToAbs(value);
            var order = _map.OrderByDescending(x => x.Key);
            foreach (var item in order)
            {
                if (value.CompareTo(item.Key) >= 0)
                    return ToStringFormat(isMinus ? "-" : "", ToRound(value, item.Key).ToString(), item.Value?.FirstOrDefault());
            }
            return ToStringFormat(isMinus ? "-" : "", value.ToString(), _map.FirstOrDefault().Value?.FirstOrDefault());
        }

        protected abstract T ToAbs(T value);

        protected abstract double ToRound(T value, T unit);

        string IUnitable.ToString(object value) => ToString((T)value);

        object IUnitable.Parse(string str) => Parse(str);
    }
}

