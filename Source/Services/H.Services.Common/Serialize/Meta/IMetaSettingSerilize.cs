namespace H.Services.Common.Serialize.Meta;

public interface IMetaSetting
{
    string ID { get; set; }
    void Load();
    bool Save(out string message);
}
