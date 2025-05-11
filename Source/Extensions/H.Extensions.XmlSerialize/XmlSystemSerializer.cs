// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Diagnostics;
using System.IO;
using System.Xml.Serialization;

namespace H.Extensions.XmlSerialize
{
    [Obsolete]
    public class XmlSystemSerializer : ISerializerService
    {
        [Obsolete]
        public void Save(string filePath, object sourceObj, string xmlRootName = null)
        {
            if (string.IsNullOrWhiteSpace(filePath)) return;

            string folder = Path.GetDirectoryName(filePath);

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            try
            {
                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    using (StreamWriter writer = new StreamWriter(stream))
                    {
                        XmlSerializer xmlSerializer = string.IsNullOrWhiteSpace(xmlRootName) ?
                            new XmlSerializer(sourceObj.GetType()) :
                            new XmlSerializer(sourceObj.GetType(), new XmlRootAttribute(xmlRootName));
                        xmlSerializer.Serialize(writer, sourceObj);
                    }
                }
            }
            catch (Exception ex)
            {
                IocLog.Instance?.Error(ex);
                Trace.Assert(false);
            }
        }
        [Obsolete]
        public T Load<T>(string filePath)
        {
            return (T)this.Load(filePath, typeof(T));
        }
        [Obsolete]
        public object Load(string filePath, Type type)
        {
            if (!File.Exists(filePath)) return null;

            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(type);
                    return xmlSerializer.Deserialize(reader);
                }
            }
            catch (Exception ex)
            {
                IocLog.Instance?.Error(ex);
                //Trace.Assert(false);
                File.Delete(filePath);
            }

            return null;
        }

        public string SerializeObject<T>(T t)
        {
            throw new NotImplementedException();
        }

        public object DeserializeObject(string txt, Type type)
        {
            throw new NotImplementedException();
        }
    }
}
