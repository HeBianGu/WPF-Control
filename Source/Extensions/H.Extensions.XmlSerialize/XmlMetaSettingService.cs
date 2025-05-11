// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Services.AppPath;
using H.Services.Common.Serialize.Meta;
using System.IO;

namespace H.Extensions.XmlSerialize
{
    public class XmlMetaSettingService : IMetaSettingService
    {
        [Obsolete]
        private ISerializerService XmlSerializer => new XmlSerializerService();

        [Obsolete]
        public T Deserilize<T>(string id)
        {
            string path = Path.Combine(AppPaths.Instance.Cache, typeof(T).Name, id + ".xml");

            if (!File.Exists(path)) return default(T);

            return this.XmlSerializer.Load<T>(path);
        }

        [Obsolete]
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
