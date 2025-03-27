// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Diagnostics;
using System.IO;
using System.Xml.Serialization;

namespace H.Extensions.XmlSerialize
{
    [Obsolete("优先使用Json保存")]
    public class XmlSerializerService : ISerializerService
    {
        [Obsolete]
        public void Save(string filePath, object sourceObj, string xmlRootName = null)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                return;
            string folder = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
            XmlableSerializor.Instance.Save(filePath, sourceObj);
        }
    

        [Obsolete]
        public object Load(string filePath, Type type)
        {
            if (!File.Exists(filePath)) return null;

            if (XmlableSerializor.Instance.IsValid(filePath, type))
            {
                object result = Activator.CreateInstance(type);
                return XmlableSerializor.Instance.Load(filePath, result);
            }
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
                Trace.Assert(false);
                File.Delete(filePath);
            }

            return null;
        }

        public string SerializeObject<T>(T t)
        {
            var doc = XmlableSerializor.Instance.SaveAs(t);
            return doc.OuterXml;
        }

        public object DeserializeObject(string txt, Type type)
        {
            return XmlableSerializor.Instance.Load(txt, type);
        }
    }
}
