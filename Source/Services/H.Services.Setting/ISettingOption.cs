namespace H.Services.Setting;

public interface ISettingDataOption
{
    void Add(params ISettable[] settings);
}
