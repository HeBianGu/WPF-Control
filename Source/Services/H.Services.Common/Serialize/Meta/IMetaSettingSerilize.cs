namespace H.Services.Common
{
    public interface IMetaSetting
    {
        string ID { get; set; }
        void Load();
        bool Save(out string message);
    }
}
