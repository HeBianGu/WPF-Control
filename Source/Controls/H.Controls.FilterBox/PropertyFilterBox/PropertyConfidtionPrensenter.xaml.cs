// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using H.Extensions.XmlSerialize;
using H.Providers.Ioc;
using H.Providers.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace H.Controls.FilterBox
{

    [Display(Name = "设置条件")]
    public class PropertyConfidtionPrensenter : DisplayerViewModelBase, IConditionable, IMetaSetting
    {
        public PropertyConfidtionPrensenter()
        {

        }

        public PropertyConfidtionPrensenter(Type modelTyle, Func<PropertyInfo, bool> predicate = null)
        {
            ObservableCollection<PropertyInfo> ps = modelTyle.GetProperties().Where(x => x.PropertyType.IsPrimitive || x.PropertyType == typeof(DateTime) || x.PropertyType == typeof(string)).ToObservable();
            if (predicate != null)
                Properties = ps.Where(predicate).ToObservable();
            else
                Properties = ps.ToObservable();
        }

        private ObservableCollection<IPropertyConfidtion> _conditions = new ObservableCollection<IPropertyConfidtion>();
        /// <summary> 说明  </summary>
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
        public ConditionOperate ConditionOperate
        {
            get { return _conditionOperate; }
            set
            {
                _conditionOperate = value;
                RaisePropertyChanged();
            }
        }


        [JsonIgnore]
        [XmlIgnore]
        public RelayCommand AddConditionCommand => new RelayCommand(l =>
        {
            PropertyInfo first = Properties.FirstOrDefault();
            PropertyConfidtion confidtion = new PropertyConfidtion(first);
            confidtion.Filter.IsSelected = true;
            Conditions.Add(confidtion);
        });

        [JsonIgnore]
        [XmlIgnore]
        public RelayCommand ClearConditionCommand => new RelayCommand(l =>
        {
            Conditions.Clear();
        }, l => Conditions.Count > 0);

        [JsonIgnore]
        [XmlIgnore]
        public RelayCommand SaveCommand => new RelayCommand(l =>
        {
            this.Save(out string message);
        });

        private ObservableCollection<PropertyInfo> _properties = new ObservableCollection<PropertyInfo>();
        [JsonIgnore]
        [XmlIgnore]
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
            if (ConditionOperate == ConditionOperate.All)
                return Conditions.All(x => x.IsMatch(obj));
            if (ConditionOperate == ConditionOperate.Any)
                return Conditions.Any(x => x.IsMatch(obj));
            if (ConditionOperate == ConditionOperate.AnyNot)
                return Conditions.Any(x => !x.IsMatch(obj));
            return !Conditions.All(x => x.IsMatch(obj));
        }

        [JsonIgnore]
        [XmlIgnore]
        public IMetaSettingService MetaSettingService => new XmlMetaSettingService();

        public bool Save(out string message)
        {
            message = null;
            if (string.IsNullOrEmpty(ID))
            {
                message = "ID为空";
                return false;
            }
            MetaSettingService?.Serilize(this, ID);
            return true;
        }

        private bool _isLoaded = false;
        public void Load()
        {
            if (_isLoaded)
                return;
            if (string.IsNullOrEmpty(ID))
                return;
            PropertyConfidtionPrensenter find = MetaSettingService?.Deserilize<PropertyConfidtionPrensenter>(ID);
            if (find == null)
                return;

            foreach (IPropertyConfidtion item in find.Conditions)
            {
                //var propertyInfo = this.Properties.FirstOrDefault(x => x.Name == item.Filter.Name);
                PropertyConfidtion pc = new PropertyConfidtion();
                //item.Filter.PropertyInfo = propertyInfo;
                pc.Filter = item.Filter;
                Conditions.Add(pc);
            }
            ConditionOperate = find.ConditionOperate;
            _isLoaded = true;
        }
    }
}
