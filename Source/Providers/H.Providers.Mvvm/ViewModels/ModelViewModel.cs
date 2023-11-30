// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace H.Providers.Mvvm
{


    public partial class ModelViewModel<T> : NotifyPropertyChanged, IModelViewModel<T>, ISearchable
    {
        public ModelViewModel(T t)
        {
            Model = t;

        }

        private T _model;
        [Browsable(false)]
        public T Model
        {
            get { return _model; }
            set
            {
                _model = value;
                RaisePropertyChanged("Model");
            }
        }

        public object GetModel()
        {
            return this.Model;
        }

        private bool _visible = true;
        [Browsable(false)]
        public bool Visible
        {
            get { return _visible; }
            set
            {
                _visible = value;
                RaisePropertyChanged("Visible");
            }
        }

        private bool _isEnbled;
        [Browsable(false)]
        public bool IsEnbled
        {
            get { return _isEnbled; }
            set
            {
                _isEnbled = value;
                RaisePropertyChanged("IsEnbled");
            }
        }


        private bool _isBuzy;
        [Browsable(false)]
        public bool IsBuzy
        {
            get { return _isBuzy; }
            set
            {
                _isBuzy = value;
                RaisePropertyChanged();
            }
        }


        private double _value;
        [Browsable(false)]
        public double Value
        {
            get { return _value; }
            set
            {
                _value = value;
                RaisePropertyChanged();
            }
        }


        private string _message;
        [Browsable(false)]
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                RaisePropertyChanged();
            }
        }

        protected virtual bool LoadValue(out string message)
        {
            message = string.Empty;
            PropertyInfo[] ps = GetType().GetProperties();
            if (ps == null)
                return true;
            foreach (PropertyInfo property in ps)
            {
                PropertyInfo find = typeof(T).GetProperty(property.Name);
                if (find == null)
                    continue;
                property.SetValue(this, find.GetValue(Model));
            }
            return true;
        }

        protected virtual bool SaveValue(out string message)
        {
            message = string.Empty;
            PropertyInfo[] ps = GetType().GetProperties();
            if (ps == null)
                return true;
            foreach (PropertyInfo property in ps)
            {
                PropertyInfo find = typeof(T).GetProperty(property.Name);
                if (find == null)
                    continue;
                find.SetValue(this, property.GetValue(Model));
            }
            return true;
        }

        public virtual bool Filter(string txt)
        {
            if (string.IsNullOrEmpty(txt))
                return true;

            string[] ands = txt.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            {
                IEnumerable<PropertyInfo> ps = Model.GetType().GetProperties().Where(x => x.CanRead).Where(l => l.PropertyType == typeof(string) || l.PropertyType.IsPrimitive || l.PropertyType == typeof(DateTime));
                ps = ps.Where(x => x.Name != "Item" && x.GetCustomAttribute<BrowsableAttribute>()?.Browsable != false);
                IEnumerable<string> list = ps.Select(x => x.GetValue(Model)?.ToString());
                if (ands.All(x => list.Any(l => l?.Contains(x) == true)))
                {
                    return true;
                }
            }
            {
                IEnumerable<PropertyInfo> ps = GetType().GetProperties().Where(x => x.CanRead).Where(l => l.PropertyType == typeof(string) || l.PropertyType.IsPrimitive || l.PropertyType == typeof(DateTime));
                ps = ps.Where(x => x.Name != "Item" && x.GetCustomAttribute<BrowsableAttribute>()?.Browsable != false);
                IEnumerable<string> list = ps.Select(x => x.GetValue(this)?.ToString());
                if (ands.All(x => list.Any(l => l?.Contains(x) == true)))
                {
                    return true;
                }
            }
            Visible = false;
            return false;
        }
    }
}
