namespace H.Services.Setting;

public interface ISettingDataService : ISettingDataOption
{
    ObservableCollection<ISettable> Settings { get; set; }
    void Cancel();
    bool Load(Action<ISettable> action, out string message);
    bool LoadLoginedLoad(Action<ISettable> action, out string message);
    void Remove(params ISettable[] settings);
    bool Save(out string message);
    void SetDefault();

    void Clear();
}
