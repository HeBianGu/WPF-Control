namespace H.Services.Common.Setting;

public interface ISettingOption
{
    string Name { get; set; }
    string GroupName { get; set; }
    string Description { get; set; }
    string Icon { get; set; }
    int Order { get; set; }
}
