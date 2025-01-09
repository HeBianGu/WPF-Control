// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace H.Controls.Form
{
    public class ObjectPropertyItem<T> : BindingVisiblablePropertyItemBase, IDataErrorInfo
    {
        public ObjectPropertyItem(PropertyInfo property, object obj) : base(property, obj)
        {
            List<RequiredAttribute> required = property.GetCustomAttributes<RequiredAttribute>()?.ToList();
            this.Validations = property.GetCustomAttributes<ValidationAttribute>()?.ToList();
            ////  Do ：这两个特性用在通知，本控件默认不可用于验证属性定义
            //Validations.RemoveAll(l => l is CustomValidationAttribute);
            //Validations.RemoveAll(l => l is CompareAttribute);
            if (required != null && required.Count > 0)
            {
                this.Flag = "*";
            }

            if (obj is INotifyPropertyChanged notify)
            {
                notify.PropertyChanged += Notify_PropertyChanged;
            }

            if (obj is DependencyObject dependencyObject)
            {
                DependencyPropertyDescriptor descriptor = DependencyPropertyDescriptor.FromName(this.PropertyInfo.Name, this.PropertyInfo.DeclaringType, this.PropertyInfo.DeclaringType);
                if (descriptor != null)
                {
                    descriptor.AddValueChanged(obj, new EventHandler((s, e) =>
                    {
                        this.LoadValue();
                    }));
                }

            }
            this.LoadValue();
        }

        private void Notify_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == this.PropertyInfo.Name)
                this.LoadValue();
        }

        private T _value;
        /// <summary> 说明  </summary>
        [Browsable(false)]
        public virtual T Value
        {
            get { return _value; }
            set
            {
                if (_value == null && value == null)
                    return;
                if (_value != null && _value.Equals(value))
                    return;
                T o = _value;
                this.Message = null;
                _value = value;
                if (!this.CheckType(value, out string error))
                {
                    this.Message = error;
                    return;
                }

                this.Error = null;
                //  Do：检验数据有效性
                if (this.Validations != null)
                {
                    foreach (ValidationAttribute item in this.Validations)
                    {
                        if (!item.IsValid(value))
                        {
                            this.Message = item is RequiredAttribute required ? item.ErrorMessage ?? this.Name + "数据不能为空" : item.ErrorMessage ?? this.Name + "数据校验失败";
                        }
                    }
                }

                RaisePropertyChanged("Value");
                if (!this.PropertyInfo.CanWrite)
                {
                    this.ReadOnly = true;
                    return;
                }

                if (this.PropertyInfo.GetCustomAttribute<ReadOnlyAttribute>()?.IsReadOnly == true)
                    return;
                try
                {
                    this.SetValue(value);
                }
                catch (Exception ex)
                {
                    this.Message = ex.Message + $"[{this.PropertyInfo.PropertyType.Name}]";
                    return;
                }

                //  Do ：触发值更改
                this.ValueChanged?.Invoke(value);
                this.OnValueChanged(o, value);
            }
        }

        protected virtual void OnValueChanged(T o, T n)
        {
            string methodName = this.PropertyInfo.Name;
            Type[] types = new Type[3];
            types[0] = typeof(PropertyInfo);
            types[1] = this.PropertyInfo.PropertyType;
            types[2] = this.PropertyInfo.PropertyType;
            object[] parameters = new object[3];
            parameters[0] = this.PropertyInfo;
            parameters[1] = o;
            parameters[2] = n;
            {
                MethodInfo method = this.Obj.GetType().GetMethod($"On{methodName}ValueChanged", types);
                method?.Invoke(this.Obj, parameters);
            }

            if (this.Obj is IPropertyValueChanged changed)
                changed.OnPropertyValueChanged(this.PropertyInfo, o, n);
            else if (this.Obj is IPropertyValueChanged<T> typechanged)
                typechanged.OnPropertyValueChanged(this.PropertyInfo, o, n);
            else
            {
                MethodInfo method = this.Obj.GetType().GetMethod($"OnPropertyValueChanged", types);
                method?.Invoke(this.Obj, parameters);
            }
        }

        private List<ValidationAttribute> Validations { get; }

        /// <summary> 验证数据类型是否合法 </summary>
        protected virtual bool CheckType(T value, out string error)
        {
            error = null;

            try
            {
                object to = this.ConverToObject(value);
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message + $"[{this.PropertyInfo.PropertyType.Name}]";
                return false;
            }
        }

        private object ConverToObject(T value)
        {
            if (value == null)
                return null;
            TypeConverterAttribute propertyConvert = this.PropertyInfo.GetCustomAttribute<TypeConverterAttribute>();
            if (propertyConvert != null)
            {
                Type type = Type.GetType(propertyConvert.ConverterTypeName);
                TypeConverter ddd = Activator.CreateInstance(type) as TypeConverter;
                return ddd.ConvertFrom(null, System.Globalization.CultureInfo.CurrentUICulture, value);
            }

            TypeConverterAttribute typeConvert = this.PropertyInfo.PropertyType.GetCustomAttribute<TypeConverterAttribute>();
            if (typeConvert != null)
            {
                Type type = Type.GetType(typeConvert.ConverterTypeName);
                ConstructorInfo constructor = type.GetConstructors().FirstOrDefault(l => l.GetParameters().Count() == 0);
                if (constructor != null)
                {
                    TypeConverter instance = Activator.CreateInstance(type) as TypeConverter;

                    if (value != null)
                    {
                        return instance.ConvertFrom(null, System.Globalization.CultureInfo.CurrentUICulture, value?.ToString());
                    }
                }
            }
            return value is IConvertible convertible ? Convert.ChangeType(value, this.PropertyInfo.PropertyType) : value;
        }

        protected virtual void SetValue(T value)
        {
            object to = this.ConverToObject(value);
            object from = this.PropertyInfo.GetValue(this.Obj);
            if (to == from)
                return;
            if (to?.Equals(from) == true)
                return;
            this.PropertyInfo.SetValue(this.Obj, to);
        }

        protected virtual T GetValue()
        {
            return (T)this.PropertyInfo.GetValue(this.Obj);
        }

        protected R GetValue<R>()
        {
            return (R)this.PropertyInfo.GetValue(this.Obj);
        }

        public override void LoadValue()
        {
            this.Value = this.GetValue();
        }

        private string _message;
        /// <summary> 说明  </summary>
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                RaisePropertyChanged("Message");
            }
        }

        public string Flag { get; set; }

        public string Error { get; private set; }

        // 验证
        public string this[string columnName]
        {
            get
            {
                return this.Error = this.Message;
            }
        }

    }
}
