// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control



using H.Mvvm;
using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace H.Controls.Form
{
    public class TextPropertyItem : ObjectPropertyItem<string>
    {
        public TextPropertyItem(PropertyInfo property, object obj) : base(property, obj)
        {
            TextBoxAttribute ta = property.GetCustomAttribute<TextBoxAttribute>();
            if (ta != null)
            {
                this.TextWrapping = ta.TextWrapping;
                this.UseClear = ta.UseClear;
            }
        }

        private TextWrapping _textWrapping = TextWrapping.Wrap;
        /// <summary> 说明  </summary>
        public TextWrapping TextWrapping
        {
            get { return _textWrapping; }
            set
            {
                _textWrapping = value;
                RaisePropertyChanged();
            }
        }


        private bool _useClear;
        /// <summary> 说明  </summary>
        public bool UseClear
        {
            get { return _useClear; }
            set
            {
                _useClear = value;
                RaisePropertyChanged();
            }
        }


        protected override string GetValue()
        {
            object value = this.PropertyInfo.GetValue(this.Obj);
            if (value == null)
                return null;
            if (IsTypeConverter(this.PropertyInfo))
            {
                return TypeConverterToString(value);
            }
            return value?.ToString();
        }


        public static bool IsIConvertible(PropertyInfo info)
        {
            return typeof(IConvertible).IsAssignableFrom(info.PropertyType);
        }

        public static bool IsTypeConverter(PropertyInfo info)
        {
            TypeConverterAttribute propertyConvert = info.GetCustomAttribute<TypeConverterAttribute>();
            if (propertyConvert != null)
                return true;
            TypeConverterAttribute typeConvert = info.PropertyType.GetCustomAttribute<TypeConverterAttribute>();
            if (typeConvert != null)
                return true;
            return false;
        }

        private string TypeConverterToString(object value)
        {
            TypeConverterAttribute propertyConvert = this.PropertyInfo.GetCustomAttribute<TypeConverterAttribute>();
            if (propertyConvert != null)
            {
                Type type = Type.GetType(propertyConvert.ConverterTypeName);
                TypeConverter ddd = Activator.CreateInstance(type) as TypeConverter;
                return ddd.ConvertToString(null, System.Globalization.CultureInfo.CurrentUICulture, value);
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
                        return instance.ConvertToString(null, System.Globalization.CultureInfo.CurrentUICulture, value);
                    }
                }
            }
            return value?.ToString();
        }

        //[Browsable(true)]
        //[Display(Name = "恢复默认", Icon = "\xe8dc", Order = 1)]
        //public override RelayCommand LoadDefaultCommand => new RelayCommand(l =>
        //{
        //    this.LoadDefault();
        //    this.LoadValue();
        //});


        //[Browsable(true)]
        //[Display(Name = "删除", Icon = "\xe857", Order = 0)]
        //public RelayCommand ClearCommand => new RelayCommand(l =>
        //{
        //    this.Value = null;
        //});
    }

    public class OpenClearPathTextPropertyItem : TextPropertyItem
    {
        public OpenClearPathTextPropertyItem(PropertyInfo property, object obj) : base(property, obj)
        {

        }

        public RelayCommand OpenCommand => new RelayCommand(l =>
        {
            Process.Start(new ProcessStartInfo(this.Value) { UseShellExecute = true });

            //Process.Start(this.Value);
        }, l =>
        {
            return File.Exists(this.Value) | Directory.Exists(this.Value);
        });


        public RelayCommand ClearCommand => new RelayCommand(l =>
        {
            //try
            //{
            if (File.Exists(this.Value))
            {
                File.Delete(this.Value);
            }

            if (Directory.Exists(this.Value))
            {
                Directory.Delete(this.Value, true);
            }

            //MessageProxy.Snacker?.ShowTime("删除成功");
            //}
            //catch (Exception ex)
            //{
            //    MessageProxy.Snacker?.ShowTime("删除失败,详情请查看日志");
            //    Logger.Instance?.Error(ex);
            //}
        }, l =>
        {
            return File.Exists(this.Value) | Directory.Exists(this.Value);
        });

    }


    public class OpenFileDialogPropertyItem : TextPropertyItem
    {
        public OpenFileDialogPropertyItem(PropertyInfo property, object obj) : base(property, obj)
        {

        }

        [Display(Name = "浏览", Order = 2)]
        public RelayCommand OpenCommand => new RelayCommand(l =>
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (File.Exists(this.Value))
                openFileDialog.InitialDirectory = System.IO.Path.GetDirectoryName(this.Value); //设置初始路径
            openFileDialog.Filter = "所有文件(*.*)|*.*"; //设置“另存为文件类型”或“文件类型”框中出现的选择内容
            openFileDialog.FilterIndex = 2; //设置默认显示文件类型为Csv文件(*.csv)|*.csv
            openFileDialog.Title = "打开文件"; //获取或设置文件对话框标题
            openFileDialog.RestoreDirectory = true; //设置对话框是否记忆上次打开的目录
            openFileDialog.Multiselect = false;//设置多选
            if (openFileDialog.ShowDialog() != true)
                return;
            this.Value = openFileDialog.FileName;
        });
    }

    public class OpenPathTextPropertyItem : OpenClearPathTextPropertyItem
    {
        public OpenPathTextPropertyItem(PropertyInfo property, object obj) : base(property, obj)
        {

        }

    }

}
