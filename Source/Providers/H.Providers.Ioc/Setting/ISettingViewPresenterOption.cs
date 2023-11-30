namespace H.Providers.Ioc
{
    public interface ISettingViewPresenterOption : ISettingDataManagerOption
    {
        double TitleWidth { get; set; }
        bool UsePassword { get; set; }
    }
}
