using H.Common;
using H.Common.Interfaces;
using H.Services.Identity;
using System.Collections.ObjectModel;
using System.Reflection.Metadata;
using System.Text;

namespace H.Modules.Setting;

public class SettingDataService : LazyInstance<SettingDataService>, ISettingDataService
{
    private ObservableCollection<ISettable> _settings = new ObservableCollection<ISettable>();
    public ObservableCollection<ISettable> Settings
    {
        get { return _settings; }
        set { _settings = value; }
    }

    public virtual bool Load(Action<ISettable> action, out string message)
    {
        message = null;
        List<string> list = new List<string>();
        list.Add(SettingGroupNames.GroupApp);
        list.Add(SettingGroupNames.GroupBase);
        list.Add(SettingGroupNames.GroupSystem);
        list.Add(SettingGroupNames.GroupData);
        list.Add(SettingGroupNames.GroupStyle);
        list.Add(SettingGroupNames.GroupControl);
        list.Add(SettingGroupNames.GroupMessage);
        list.Add(SettingGroupNames.GroupSecurity);
        list.Add(SettingGroupNames.GroupAuthority);
        list.Add(SettingGroupNames.GroupOther);
        Comparison<ISettable> comparison = (x, y) =>
        {
            return x == null
                ? -1
                : y == null
                ? 1
                : x.GroupName == y.GroupName ? x.Order.CompareTo(y.Order) : list.IndexOf(x.GroupName).CompareTo(list.IndexOf(y.GroupName));
        };
        List<ISettable> settings = this.Settings.ToList();
        settings.RemoveAll(x => x == null);
        settings.Sort(comparison);
        this.Settings = new ObservableCollection<ISettable>(settings);
        foreach (ILoadable item in this.Settings.OfType<ILoadable>())
        {
            action?.Invoke(item as ISettable);
            try
            {
                var r = item.Load(out message);
                if (r == false)
                    IocLog.Instance?.Error(message);
            }
            catch (Exception ex)
            {
                message = ex.Message;
                IocLog.Instance?.Error(ex);
            }
        }

        return true;
    }

    public bool LoadLoginedLoad(Action<ISettable> action, out string message)
    {
        message = null;
        foreach (ILoginedSplashLoad item in this.Settings.OfType<ILoginedSplashLoad>())
        {
            action?.Invoke(item as ISettable);
            item.Load(out message);
        }
        return true;
    }

    public virtual bool Save(out string message)
    {
        StringBuilder sb = new StringBuilder();
        foreach (ISaveable item in this.Settings.OfType<ISaveable>())
        {
            try
            {
                if (!item.Save(out string error))
                    sb.AppendLine(error);
            }
            catch (Exception ex)
            {
                IocLog.Instance?.Error(ex);
                sb.AppendLine(ex.Message);
            }
        }
        message = sb.ToString();
        return string.IsNullOrEmpty(message);
    }

    public void SetDefault()
    {
        foreach (IDefaultable item in this.Settings.OfType<IDefaultable>())
        {
            item.LoadDefault();
        }
    }


    public void Clear()
    {
        foreach (var item in this.Settings.OfType<IClearable>())
        {
            item.Clear();
        }
        this.Settings.Clear();
    }

    public void Cancel()
    {
        foreach (ILoadable item in this.Settings.OfType<ILoadable>())
        {
            item.Load(out string message);
        }
    }

    public void Add(params ISettable[] settings)
    {
        foreach (ISettable setting in settings)
        {
            if (this.Settings.Contains(setting))
                continue;
            this.Settings.Add(setting);
        }
    }

    public void Remove(params ISettable[] settings)
    {
        foreach (ISettable setting in settings)
        {
            if (!this.Settings.Contains(setting))
                continue;
            this.Settings.Remove(setting);
        }
    }
}
