namespace H.Services.Common.Setting;

public interface ISettingDataService
{
    ObservableCollection<ISettable> Settings { get; set; }
    void Add(params ISettable[] settings);
    void Cancel();
    bool Load(Action<ISettable> action, out string message);
    bool LoadLoginedLoad(Action<ISettable> action, out string message);
    void Remove(params ISettable[] settings);
    bool Save(out string message);
    void SetDefault();
}
