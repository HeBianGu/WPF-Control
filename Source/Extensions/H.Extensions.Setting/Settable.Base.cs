global using H.Common.Interfaces;
global using H.Mvvm.ViewModels.Base;
global using H.Services.Serializable;
global using System.IO;
global using System.Reflection;
global using System.Xml.Serialization;
using H.Services.AppPath;
using H.Services.Setting;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace H.Extensions.Setting;

public abstract class SettableBase : DisplayBindableBase, ISettable, ILoadable, ISaveable, IDefaultable, IClearable
{
    [XmlIgnore]
    [JsonIgnore]
    [Browsable(false)]
    public bool IsVisibleInSetting { get; set; } = true;

    public override void LoadDefault()
    {
        base.LoadDefault();
    }

    protected virtual string GetDefaultPath()
    {
        return System.IO.Path.Combine(this.GetDefaultFolder(), this.GetType().Name + ".json");
    }

    protected virtual string GetDefaultFolder()
    {
        if (this is ILoginedSplashLoad)
            return AppPaths.Instance.UserSetting;
        return AppPaths.Instance.Setting;
    }

    public virtual bool Save(out string message)
    {
        message = null;
        string path = this.GetDefaultPath();
        string folder = Path.GetDirectoryName(path);
        if (!Directory.Exists(folder))
            Directory.CreateDirectory(folder);
        this.GetSerializerService()?.Save(path, this);
        return true;
    }

    protected virtual ISerializerService GetSerializerService()
    {
        return new TextJsonSerializerService();
    }

    public virtual bool Load(out string message)
    {
        var path = this.GetDefaultPath();
        if (!File.Exists(path))
        {
            message = "文件不存在";
            return false;
        }
        message = null;
        this.Load(path);
        return true;
    }

    protected virtual void Load(string path)
    {
        if (!File.Exists(path))
            return;
        object load = this.GetSerializerService()?.Load(path, this.GetType());
        if (load == null)
            return;
        PropertyInfo[] ps = this.GetType().GetProperties();
        foreach (PropertyInfo p in ps)
        {
            if (p.GetCustomAttribute<XmlIgnoreAttribute>() != null)
                continue;
            if (p.CanRead && p.CanWrite)
            {
                object v = p.GetValue(load);
                p.SetValue(this, v);
            }
        }
    }

    public virtual bool IsInit()
    {
        return !File.Exists(this.GetDefaultPath());
    }

    public virtual void Clear()
    {
        var path = this.GetDefaultPath();
        if (File.Exists(path))
            File.Delete(path);
    }
}
