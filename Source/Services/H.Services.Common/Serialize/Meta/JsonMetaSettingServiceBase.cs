using System.IO;

namespace H.Services.Common
{
    public abstract class JsonMetaSettingServiceBase : IMetaSettingService
    {
        public abstract T Deserilize<T>(string id);
        public abstract void Serilize(object setting, string id);
        protected string GetFilePath(string typeName, string id)
        {
            string path = Path.Combine(AppPaths.Instance.Cache, typeName, id + ".json");
            if (!Directory.Exists(Path.GetDirectoryName(path)))
                Directory.CreateDirectory(Path.GetDirectoryName(path));
            return path;
        }
    }
}
