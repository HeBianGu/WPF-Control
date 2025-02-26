using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Windows.Input;
using System.Xml.Serialization;

namespace H.Mvvm
{
    public class IconAttribute : Attribute
    {
        public IconAttribute(string icon)
        {
            this.Icon = icon;
        }
        public string Icon { get; set; }
    }

    public interface IIconable
    {
        public string Icon { get; set; }
    }
    public interface IDisplayBindable : IIconable
    {
        string Description { get; set; }
        string GroupName { get; set; }
        string ID { get; set; }
        string Name { get; set; }
        int Order { get; set; }
        string ShortName { get; set; }
    }

    public abstract class DisplayBindableBase : BindableBase, IDable, IDisplayBindable
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
            this.Commands = new ObservableCollection<ICommand>(this.CreateCommands());
            IconAttribute icon = type.GetCustomAttribute<IconAttribute>(true);
            this.Icon = icon?.Icon;
            LoadDefault();
        }
        [Browsable(false)]
        [System.Text.Json.Serialization.JsonIgnore]

        [System.Xml.Serialization.XmlIgnore]
        public ObservableCollection<ICommand> Commands { get; } = new ObservableCollection<ICommand>();

        protected virtual IEnumerable<ICommand> CreateCommands()
        {
            var type = this.GetType();
            System.Collections.Generic.IEnumerable<PropertyInfo> cmdps = type.GetProperties().Where(x => typeof(ICommand).IsAssignableFrom(x.PropertyType));
            foreach (PropertyInfo cmdp in cmdps)
            {
                if (cmdp.CanRead == false)
                    continue;
                if (cmdp.GetCustomAttribute<BrowsableAttribute>()?.Browsable == false)
                    continue;
                ICommand command = cmdp.GetValue(this) as ICommand;
                //this.Commands.Add(command);
                yield return command;
            }
        }

        [Browsable(false)]
        [System.Text.Json.Serialization.JsonIgnore]

        [System.Xml.Serialization.XmlIgnore]
        public RelayCommand LoadedCommand => new RelayCommand(Loaded);

        [Browsable(false)]
        [System.Text.Json.Serialization.JsonIgnore]

        [System.Xml.Serialization.XmlIgnore]
        public bool IsLoaded { get; set; }
        protected virtual void Loaded(object obj)
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
        public virtual RelayCommand LoadDefaultCommand => new RelayCommand((s, e) =>
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
