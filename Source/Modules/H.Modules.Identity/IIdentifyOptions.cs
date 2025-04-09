
namespace H.Modules.Identity;

public interface IIdentifyOptions
{
    bool UseAdiminCheckOnRegister { get; set; }
    TimeSpan UserLicenseDefaultTryTime { get; set; }
    bool UseUserLicenseDeadTime { get; set; }
}