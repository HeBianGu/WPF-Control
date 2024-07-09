namespace H.Services.Common
{
    public interface ISettingOption
    {
        string Name { get; set; }
        string GroupName { get; set; }
        string Description { get; set; }
        string Icon { get; set; }
        int Order { get; set; }
    }
}
