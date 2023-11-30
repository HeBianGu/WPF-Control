namespace H.Providers.Ioc
{
    public interface ISetting
    {
        bool IsVisibleInSetting { get; set; }
        int Order { get; }
        string Name { get; }
        string GroupName { get; }
        void Load();
        bool Save(out string message);
        void LoadDefault();
    }
}
