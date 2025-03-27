// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

global using System.ComponentModel.DataAnnotations;
global using H.Extensions.NewtonsoftJson;
global using System.Windows.Markup;
global using System.ComponentModel;
global using H.Mvvm.ViewModels.Base;
global using H.Mvvm.Commands;
global using H.Extensions.Common;
global using H.Services.Common.Serialize.Meta;
using EnumConverter = System.ComponentModel.EnumConverter;

namespace H.Controls.FilterBox
{
    [ContentProperty(nameof(Conditions))]
    [Display(Name = "设置条件")]
    public class PropertyConditionPrensenter : DisplayBindableBase, IConditionable, IMetaSetting
    {
        public PropertyConditionPrensenter()
        {

        }

        public PropertyConditionPrensenter(Type modelTyle, Func<PropertyInfo, bool> predicate = null)
        {
            ObservableCollection<PropertyInfo> ps = modelTyle.GetProperties().Where(x => x.PropertyType.IsPrimitive || x.PropertyType == typeof(DateTime) || x.PropertyType == typeof(string)).ToObservable();
            if (predicate != null)
                this.Properties = ps.Where(predicate).ToObservable();
            else
                this.Properties = ps.ToObservable();
        }

        private ObservableCollection<IPropertyConfidtion> _conditions = new ObservableCollection<IPropertyConfidtion>();
        public ObservableCollection<IPropertyConfidtion> Conditions
        {
            get { return _conditions; }
            set
            {
                _conditions = value;
                RaisePropertyChanged();
            }
        }

        private ConditionOperate _conditionOperate = ConditionOperate.All;
        [TypeConverter(typeof(EnumConverter))]
        public ConditionOperate ConditionOperate
        {
            get { return _conditionOperate; }
            set
            {
                _conditionOperate = value;
                RaisePropertyChanged();
            }
        }


        [System.Text.Json.Serialization.JsonIgnore]
        [System.Xml.Serialization.XmlIgnore]
        public RelayCommand AddConditionCommand => new RelayCommand(l =>
        {
            PropertyInfo first = this.Properties.FirstOrDefault();
            PropertyConfidtion confidtion = new PropertyConfidtion(first);
            confidtion.Filter.IsSelected = true;
            this.Conditions.Add(confidtion);
        });

        [System.Text.Json.Serialization.JsonIgnore]
        [System.Xml.Serialization.XmlIgnore]
        public RelayCommand ClearConditionCommand => new RelayCommand(l =>
        {
            this.Conditions.Clear();
        }, l => this.Conditions.Count > 0);

        [System.Text.Json.Serialization.JsonIgnore]
        [System.Xml.Serialization.XmlIgnore]
        public RelayCommand SaveCommand => new RelayCommand(l =>
        {
            this.Save(out string message);
        });

        private ObservableCollection<PropertyInfo> _properties = new ObservableCollection<PropertyInfo>();
        [System.Text.Json.Serialization.JsonIgnore]
        [System.Xml.Serialization.XmlIgnore]
        public ObservableCollection<PropertyInfo> Properties
        {
            get { return _properties; }
            set
            {
                _properties = value;
                RaisePropertyChanged();
            }
        }

        public bool IsMatch(object obj)
        {
            if (this.ConditionOperate == ConditionOperate.All)
                return this.Conditions.All(x => x.IsMatch(obj));
            if (this.ConditionOperate == ConditionOperate.Any)
                return this.Conditions.Any(x => x.IsMatch(obj));
            if (this.ConditionOperate == ConditionOperate.AnyNot)
                return this.Conditions.Any(x => !x.IsMatch(obj));
            return !this.Conditions.All(x => x.IsMatch(obj));
        }

        [System.Text.Json.Serialization.JsonIgnore]
        
        [System.Xml.Serialization.XmlIgnore]
        public IMetaSettingService MetaSettingService => new NewtonsoftJsonMetaSettingService();

        public bool Save(out string message)
        {
            message = null;
            if (string.IsNullOrEmpty(this.ID))
            {
                message = "ID为空";
                return false;
            }
            this.MetaSettingService?.Serilize(this, this.ID);
            return true;
        }

        private bool _isLoaded = false;
        public void Load()
        {
            if (_isLoaded)
                return;
            if (string.IsNullOrEmpty(this.ID))
                return;
            PropertyConditionPrensenter find = this.MetaSettingService?.Deserilize<PropertyConditionPrensenter>(this.ID);
            if (find == null)
                return;

            foreach (IPropertyConfidtion item in find.Conditions)
            {
                //var propertyInfo = this.Properties.FirstOrDefault(x => x.Name == item.Filter.Name);
                PropertyConfidtion pc = new PropertyConfidtion();
                //item.Filter.PropertyInfo = propertyInfo;
                pc.Filter = item.Filter;
                this.Conditions.Add(pc);
            }
            this.ConditionOperate = find.ConditionOperate;
            _isLoaded = true;
        }
    }
}
