using System.IO;
using System.Text.Json;

namespace H.Services.Common
{
    public class JsonMetaSettingService : IMetaSettingService
    {
        private string GetFilePath(string typeName, string id) => Path.Combine(AppPaths.Instance.Cache, id + ".json");
        public T Deserilize<T>(string id)
        {
            if (!File.Exists(this.GetFilePath(typeof(T).Name, id)))
                return default;
            string txt = File.ReadAllText(this.GetFilePath(typeof(T).Name, id));
            return (T)JsonSerializer.Deserialize(txt, typeof(T));
        }

        public void Serilize(object setting, string id)
        {
            string txt = JsonSerializer.Serialize(setting);
            File.WriteAllText(this.GetFilePath(setting.GetType().Name, id), txt);
        }
    }
}
