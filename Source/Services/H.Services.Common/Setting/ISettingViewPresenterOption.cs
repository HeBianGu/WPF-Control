namespace H.Services.Common
{
    public interface ISettingViewPresenterOption : ISettingDataManagerOption
    {
        double TitleWidth { get; set; }
        bool UsePassword { get; set; }
    }
}
