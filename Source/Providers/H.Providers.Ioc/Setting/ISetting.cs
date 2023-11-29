using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace H.Providers.Ioc
{
    public interface ISetting
    {
        bool IsVisibleInSetting { get; set; }
        int Order { get; }
        string Name { get; }
        string GroupName { get; }
        void Load();
        bool Save(out string message);
        void LoadDefault();
    }
}
