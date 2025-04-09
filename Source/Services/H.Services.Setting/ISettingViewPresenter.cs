using H.Common.Interfaces;

namespace H.Services.Setting;

public interface ISettingViewPresenter : ITitleable
{
    void SwitchTo(Type type);
    void RefreshSettingData();
    Task<bool> Show(Type switchType = null);
}
