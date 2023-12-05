using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace H.Providers.Ioc
{
    public interface ISettingDataManagerOption
    {
        void Add(params ISetting[] settings);
    }

    public class SettingDataManager : LazyInstance<SettingDataManager>, ISettingDataManagerOption
    {
        private ObservableCollection<ISetting> _settings = new ObservableCollection<ISetting>();
        public ObservableCollection<ISetting> Settings
        {
            get { return _settings; }
            set { _settings = value; }
        }

        public virtual bool Load(out string message)
        {
            message = null;
            List<string> list = new List<string>();
            list.Add(SystemSetting.GroupApp);
            list.Add(SystemSetting.GroupBase);
            list.Add(SystemSetting.GroupSystem);
            list.Add(SystemSetting.GroupData);
            list.Add(SystemSetting.GroupStyle);
            list.Add(SystemSetting.GroupControl);
            list.Add(SystemSetting.GroupMessage);
            list.Add(SystemSetting.GroupSecurity);
            list.Add(SystemSetting.GroupAuthority);
            list.Add(SystemSetting.GroupOther);
            Comparison<ISetting> comparison = (x, y) =>
            {
                if (x == null) return -1;
                if (y == null) return 1;
                if (x.GroupName == y.GroupName)
                    return x.Order.CompareTo(y.Order);
                return list.IndexOf(x.GroupName).CompareTo(list.IndexOf(y.GroupName));
            };
            List<ISetting> settings = this.Settings.ToList();
            settings.RemoveAll(x => x == null);
            settings.Sort(comparison);
            this.Settings = new ObservableCollection<ISetting>(settings);
            foreach (ISetting item in this.Settings)
            {
                item.Load();
            }

            return true;
        }

        public virtual bool Save(out string message)
        {
            StringBuilder sb = new StringBuilder();
            foreach (ISetting item in this.Settings)
            {
                try
                {
                    if (!item.Save(out string error))
                    {
                        sb.AppendLine(error);
                    }
                }
                catch (Exception ex)
                {
                    sb.AppendLine(ex.Message);
                }
            }
            message = sb.ToString();
            return string.IsNullOrEmpty(message);
        }

        public void SetDefault()
        {
            foreach (ISetting item in this.Settings)
            {
                item.LoadDefault();
            }
        }

        public void Cancel()
        {
            foreach (ISetting item in this.Settings)
            {
                item.Load();
            }
        }

        public void Add(params ISetting[] settings)
        {
            foreach (ISetting setting in settings)
            {
                if (this.Settings.Contains(setting))
                    continue;
                this.Settings.Add(setting);
            }
        }
    }
}
