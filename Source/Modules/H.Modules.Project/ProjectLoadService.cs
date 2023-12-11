// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase



using H.Extensions.XmlSerialize;
using H.Modules.Login;
using H.Providers.Ioc;
using System;

namespace H.Modules.Project
{
    public class ProjectLoadService : IProjectLoadService
    {
        public string Name => "工程列表";

        public virtual bool Load(out string message)
        {
            message = null;
            var data = this.GetSerializer().Load<ProjectHistroyData>(ProjectOptions.Instance.HistoryPath);
            if (data == null)
                return true;
            foreach (var item in data.ProjectItems)
            {
                Ioc<IProjectService>.Instance.Add(item);
            }
            return true;
        }

        protected ISerializerService GetSerializer() => new XmlSerializerService();
    }
}
