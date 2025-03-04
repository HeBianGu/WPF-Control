using H.Mvvm.Attributes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Windows.Input;

namespace H.Mvvm.ViewModels.Base
{
    public abstract class DisplayBindableBase : CommandsBindableBase, IDable, IDisplayBindable
    {
        public DisplayBindableBase()
        {
            var type = this.GetType();
            this.Name = type.Name;
            DisplayAttribute display = type.GetCustomAttribute<DisplayAttribute>(true);
            if (display != null)
            {
                this.Name = display.Name ?? this.Name;
                this.GroupName = display.GroupName;
                this.Description = display.Description;
                int? od = display.GetOrder();
                if (od.HasValue)
                    this.Order = od.Value;
                this.ShortName = display.ShortName;
            }
            IDAttribute id = type.GetCustomAttribute<IDAttribute>(true);
            this.ID = id?.ID ?? type.Name;
            IconAttribute icon = type.GetCustomAttribute<IconAttribute>(true);
            this.Icon = icon?.Icon;
            LoadDefault();
        }

        [Browsable(false)]
        [System.Text.Json.Serialization.JsonIgnore]
        [System.Xml.Serialization.XmlIgnore]
        public bool IsLoaded { get; set; }
        protected override void Loaded(object obj)
        {
            this.IsLoaded = true;
        }

        private string _id;
        [Browsable(false)]
        public virtual string ID
        {
            get { return _id; }
            set
            {
                _id = value;
                RaisePropertyChanged();
            }
        }

        private string _name;
        [System.Text.Json.Serialization.JsonIgnore]
        [System.Xml.Serialization.XmlIgnore]
        [Browsable(false)]
        public virtual string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged();
            }
        }


        private string _icon;
        [System.Text.Json.Serialization.JsonIgnore]
        [System.Xml.Serialization.XmlIgnore]
        [Browsable(false)]
        public virtual string Icon
        {
            get { return _icon; }
            set
            {
                _icon = value;
                RaisePropertyChanged();
            }
        }

        private string _shortName;
        [System.Text.Json.Serialization.JsonIgnore]
        [System.Xml.Serialization.XmlIgnore]
        [Browsable(false)]
        public virtual string ShortName
        {
            get { return _shortName; }
            set
            {
                _shortName = value;
                RaisePropertyChanged();
            }
        }

        private string _groupName;
        [System.Text.Json.Serialization.JsonIgnore]
        [System.Xml.Serialization.XmlIgnore]
        [Browsable(false)]
        public virtual string GroupName
        {
            get { return _groupName; }
            set
            {
                _groupName = value;
                RaisePropertyChanged();
            }
        }

        private string _description;
        [System.Text.Json.Serialization.JsonIgnore]
        [System.Xml.Serialization.XmlIgnore]
        [Browsable(false)]
        public virtual string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                RaisePropertyChanged();
            }
        }


        private int _order;
        [System.Text.Json.Serialization.JsonIgnore]
        [System.Xml.Serialization.XmlIgnore]
        [Browsable(false)]
        public virtual int Order
        {
            get { return _order; }
            set
            {
                _order = value;
                RaisePropertyChanged();
            }
        }

        [System.Text.Json.Serialization.JsonIgnore]
        [System.Xml.Serialization.XmlIgnore]
        [Display(Name = "恢复默认")]
        [Browsable(false)]
        public virtual RelayCommand LoadDefaultCommand => new RelayCommand(x =>
        {
            LoadDefault();
        });

        public virtual void LoadDefault()
        {
            PropertyInfo[] ps = GetType().GetProperties();
            foreach (PropertyInfo p in ps)
            {
                DefaultValueAttribute d = p.GetCustomAttribute<DefaultValueAttribute>();
                if (d == null) continue;
                if (typeof(IConvertible).IsAssignableFrom(p.PropertyType))
                {
                    object value = Convert.ChangeType(d.Value, p.PropertyType);
                    p.SetValue(this, value);
                }
                else
                {
                    p.SetValue(this, d.Value);
                }
            }
        }
    }

}
