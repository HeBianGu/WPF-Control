namespace System
{
    public interface IMetaSettingSerilize
    {
        string ID { get; set; }
        void Save();
        void Load();
    }

    public interface IMetaSettingService
    {
        void Serilize(object setting, string id);
        T Deserilize<T>(string id) where T : IMetaSetting;
    }

    public interface IMetaSetting
    {

    }
}
