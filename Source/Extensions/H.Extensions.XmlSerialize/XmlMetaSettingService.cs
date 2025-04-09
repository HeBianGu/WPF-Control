// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control
using H.Services.AppPath;
using H.Services.Common;
using H.Services.Common.Serialize.Meta;
using System;
using System.IO;
using System.Windows;

namespace H.Extensions.XmlSerialize
{
    public class XmlMetaSettingService : IMetaSettingService
    {
        private ISerializerService XmlSerializer => new XmlSerializerService();

        public T Deserilize<T>(string id)
        {
            string path = Path.Combine(AppPaths.Instance.Cache, typeof(T).Name, id + ".xml");

            if (!File.Exists(path)) return default(T);

            return this.XmlSerializer.Load<T>(path);
        }

        public void Serilize(object setting, string id)
        {
            Application.Current.Dispatcher.BeginInvoke(MetaSetting.Instance.DispatcherPriority, new Action(() =>
                       {
                           string path = Path.Combine(AppPaths.Instance.Cache, setting.GetType().Name, id + ".xml");

                           this.XmlSerializer.Save(path, setting);
                       }));

        }
    }

}
