using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Windows.Input;
using System.Xml.Serialization;

namespace H.Providers.Mvvm
{


    public interface IDisplayer
    {
        void LoadDefault();
    }

    public class DisplayerAttribute : Attribute
    {
        public int Order { get; set; }
        public string GroupName { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string ID { get; set; }
        public string Icon { get; set; }
        public string TabName { get; set; }
    }
    public class DisplayerViewModelBase : NotifyPropertyChangedBase, IDisplayer
    {
        public DisplayerViewModelBase()
        {
            ID = GetType().Name;
            Name = GetType().Name;
            {

                {
                    var display = GetType().GetCustomAttribute<DisplayerAttribute>(true);
                    if (display != null)
                    {
                        ID = display.ID ?? ID;
                        Name = display.Name ?? Name;
                        GroupName = display.GroupName;
                        Description = display.Description;
                        Order = display.Order;
                        Icon = display.Icon;
                        TabName = display.TabName;
                    }
                }
                {
                    var display = GetType().GetCustomAttribute<DisplayAttribute>(true);
                    if (display != null)
                    {
                        Name = display.Name ?? Name;
                        GroupName = display.GroupName;
                        Description = display.Description;
                        var od = display.GetOrder();
                        if (od.HasValue)
                            Order = od.Value;
                    }
                }
            }

            var cmdps = GetType().GetProperties().Where(x => typeof(ICommand).IsAssignableFrom(x.PropertyType));
            foreach (var cmdp in cmdps)
            {
                if (cmdp.CanRead == false)
                    continue;
                if (cmdp.GetCustomAttribute<BrowsableAttribute>()?.Browsable == false)
                    continue;
                ICommand command = cmdp.GetValue(this) as ICommand;
                Commands.Add(command);
            }

            LoadDefault();
        }
        [Browsable(false)]
        [JsonIgnore]
        [XmlIgnore]
        public ObservableCollection<ICommand> Commands { get; } = new ObservableCollection<ICommand>();

        [Browsable(false)]
        [JsonIgnore]
        [XmlIgnore]
        public RelayCommand LoadedCommand => new RelayCommand(Loaded);

        [Browsable(false)]
        [JsonIgnore]
        [XmlIgnore]
        public bool IsLoaded { get; set; }
        protected virtual void Loaded(object obj)
        {
            IsLoaded = true;
        }
        //[XmlIgnore]
        //[Browsable(false)]
        //public virtual string ID { get; set; }

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
        [JsonIgnore]
        [XmlIgnore]
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
        [JsonIgnore]
        [XmlIgnore]
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
        [JsonIgnore]
        [XmlIgnore]
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
        [JsonIgnore]
        [XmlIgnore]
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
        [JsonIgnore]
        [XmlIgnore]
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
        [JsonIgnore]
        [XmlIgnore]
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


        private string _tabName;
        [JsonIgnore]
        [XmlIgnore]
        [Browsable(false)]
        public virtual string TabName
        {
            get { return _tabName; }
            set
            {
                _tabName = value;
                RaisePropertyChanged();
            }
        }
        [JsonIgnore]
        [XmlIgnore]
        [Display(Name = "恢复默认")]
        [Browsable(false)]
        public virtual RelayCommand LoadDefaultCommand => new RelayCommand((s, e) =>
        {
            LoadDefault();
        });

        public virtual void LoadDefault()
        {

            System.Diagnostics.Debug.WriteLine("DisplayerViewModelBase.LoadDefault");

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
