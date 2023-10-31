using H.Providers.Ioc;
using H.Providers.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace H.Extensions.Setting
{
    public abstract class SettingBase : DisplayerViewModelBase, ISetting
    {
        [Browsable(false)]
        public bool IsVisibleInSetting { get; set; } = true;

        public override void LoadDefault()
        {
            base.LoadDefault();
        }

        protected virtual string GetDefaultPath()
        {
            return System.IO.Path.Combine(this.GetDefaultFolder(), this.ID + SystemPathSetting.Instance.ConfigExtention);
        }

        protected virtual string GetDefaultFolder()
        {
            return SystemPathSetting.Instance.Setting;
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
            return XmlSerialize.Instance;
        }

        public virtual void Load()
        {
            this.Load(this.GetDefaultPath());
        }

        protected virtual void Load(string path)
        {
            object load = this.GetSerializerService()?.Load(path, this.GetType());
            if (load == null) return;
            PropertyInfo[] ps = this.GetType().GetProperties();
            foreach (PropertyInfo p in ps)
            {
                if (p.GetCustomAttribute<XmlIgnoreAttribute>() != null)
                    continue;
                if (p.CanRead && p.CanWrite)
                {
                    var v = p.GetValue(load);
                    p.SetValue(this, v);
                }
            }
        }

        public virtual bool IsInit()
        {
            return !File.Exists(this.GetDefaultPath());
        }
    }
}
