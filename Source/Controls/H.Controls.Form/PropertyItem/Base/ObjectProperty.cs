// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using H.Providers.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace H.Controls.Form
{
    public class ObjectPropertyItem<T> : ObjectPropertyItem, IDataErrorInfo
    {
        public ObjectPropertyItem(PropertyInfo property, object obj) : base(property, obj)
        {
            List<RequiredAttribute> required = property.GetCustomAttributes<RequiredAttribute>()?.ToList();
            Validations = property.GetCustomAttributes<ValidationAttribute>()?.ToList();
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
                var descriptor = DependencyPropertyDescriptor.FromName(this.PropertyInfo.Name, this.PropertyInfo.DeclaringType, this.PropertyInfo.DeclaringType);
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
                if (Validations != null)
                {
                    foreach (ValidationAttribute item in Validations)
                    {
                        if (!item.IsValid(value))
                        {
                            if (item is RequiredAttribute required)
                            {
                                this.Message = item.ErrorMessage ?? this.Name + "数据不能为空";
                            }
                            else
                            {
                                this.Message = item.ErrorMessage ?? this.Name + "数据校验失败";
                            }
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
            if (value is IConvertible convertible)
            {
                return Convert.ChangeType(value, this.PropertyInfo.PropertyType);
            }
            else
            {
                return value;
            }
        }

        protected virtual void SetValue(T value)
        {
            object to = this.ConverToObject(value);
            object from = this.PropertyInfo.GetValue(Obj);
            if (to == from)
                return;
            if (to?.Equals(from) == true)
                return;
            this.PropertyInfo.SetValue(Obj, to);
        }

        protected virtual T GetValue()
        {
            return (T)this.PropertyInfo.GetValue(Obj);
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

    public interface IPropertyValueChanged
    {
        void OnPropertyValueChanged(PropertyInfo propertyInfo, object o, object n);
    }

    public interface IPropertyValueChanged<PropertyType>
    {
        void OnPropertyValueChanged(PropertyInfo propertyInfo, PropertyType o, PropertyType n);
    }

    /// <summary> 类型基类 </summary>
    public abstract class ObjectPropertyItem : DisplayViewModelBase, IPropertyItem
    {
        public string TabGroup { get; set; }
        public PropertyInfo PropertyInfo { get; set; }
        public object Obj { get; set; }
        public bool ReadOnly { get; set; }
        public Visibility Visibility { get; set; }
        public Action<object> ValueChanged { get; set; }
        public string Unit { get; set; }
        public int Vip { get; set; }
        //~ObjectPropertyItem()
        //{
        //    ValueChanged = null; 
        //}

        public ObjectPropertyItem(PropertyInfo property, object obj)
        {
            PropertyInfo = property;
            Obj = obj;
            DisplayAttribute display = property.GetCustomAttribute<DisplayAttribute>();
            this.Name = property.Name;
            if (display != null)
            {
                this.Name = display == null ? property.Name : display.Name;
                this.TabGroup = display?.Prompt;
                this.GroupName = display?.GroupName;
                this.Description = display?.Description;
                this.Order = display == null ? 999 : display.GetOrder().HasValue ? display.GetOrder().Value : 999;
            }
            //DisplayAttribute displayer = property.GetCustomAttribute<DisplayAttribute>();
            //if (displayer != null)
            //{
            //    this.Name = displayer == null ? displayer.Name : displayer.Name;
            //    this.GroupName = displayer?.GroupName;
            //    this.Description = displayer?.Description;
            //    this.Order = displayer == null ? 999 : displayer.Order;
            //    this.Icon = displayer?.Icon;
            //}

            ReadOnlyAttribute readyOnly = property.GetCustomAttribute<ReadOnlyAttribute>();
            this.ReadOnly = readyOnly?.IsReadOnly == true;
            if (!this.PropertyInfo.CanWrite)
            {
                this.ReadOnly = true;
            }
            //  Do ：用于控制显示和隐藏
            BrowsableAttribute browsable = property.GetCustomAttribute<BrowsableAttribute>();
            this.Visibility = browsable == null || browsable.Browsable ? Visibility.Visible : Visibility.Collapsed;

            UnitAttribute unit = property.GetCustomAttribute<UnitAttribute>();
            if (unit != null)
                this.Unit = unit.Unit;

            //var vip = property.GetCustomAttribute<VipAttribute>();
            //if (vip != null)
            //    this.Vip = vip.Level;
        }

        /// <summary>
        /// 从实体中加载数据到页面
        /// </summary>
        public abstract void LoadValue();
    }













    ///// <summary> Double属性类型 </summary>
    //public class DoubleArrayPropertyItem : ObjectPropertyItem<double[]>
    //{
    //    public DoubleArrayPropertyItem(PropertyInfo property, object obj) : base(property, obj)
    //    {
    //    }
    //}
}
