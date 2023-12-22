// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase



using H.Extensions.XmlSerialize;
using H.Modules.Login;
using H.Providers.Ioc;
using System;
using System.Linq;

namespace H.Modules.Project
{
    [Obsolete]
    public class ProjectLoadService : IProjectLoadService
    {
        public string Name => "工程列表";

        public virtual bool Load(out string message)
        {
            //message = null;
            //var data = this.GetSerializer().Load<ProjectHistroyData>(ProjectOptions.Instance.HistoryPath);
            //if (data == null)
            //    return true;
            //foreach (var item in data.ProjectItems)
            //{
            //    Ioc<IProjectService>.Instance.Add(item);
            //}
            //Ioc<IProjectService>.Instance.Current = data.ProjectItems.FirstOrDefault();
           return Ioc<IProjectService>.Instance.Load(out message);
            //return true;
        }

        protected ISerializerService GetSerializer() => new XmlSerializerService();
    }
}
