namespace H.Providers.Ioc
{
    public interface IMetaSettingSerilize
    {
        string ID { get; set; }
        void Save();
        void Load();
    }
}
