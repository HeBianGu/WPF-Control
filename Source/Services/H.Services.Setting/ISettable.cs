using H.Common.Interfaces;

namespace H.Services.Setting;

public interface ISettable : INameable, IOrderable, IGroupable
{
    bool IsVisibleInSetting { get; set; }
}
