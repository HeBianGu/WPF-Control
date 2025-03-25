using H.Common.Interfaces;

namespace H.Services.Common.Setting;

public interface ISettingViewPresenter : ITitleable
{
    void SwitchTo(Type type);
}
