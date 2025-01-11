using System.IO;

namespace H.Services.Common
{
    public abstract class JsonMetaSettingServiceBase : IMetaSettingService
    {
        public abstract T Deserilize<T>(string id);
        public abstract void Serilize(object setting, string id);
        protected string GetFilePath(string typeName, string id) => Path.Combine(AppPaths.Instance.Cache, id + ".json");
    }
}
