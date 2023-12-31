// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control
using H.Providers.Ioc;
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

            return XmlSerializer.Load<T>(path);
        }

        public void Serilize(object setting, string id)
        {
            Application.Current.Dispatcher.BeginInvoke(MetaSetting.Instance.DispatcherPriority, new Action(() =>
                       {
                           string path = Path.Combine(AppPaths.Instance.Cache, setting.GetType().Name, id + ".xml");

                           XmlSerializer.Save(path, setting);
                       }));

        }
    }

}
