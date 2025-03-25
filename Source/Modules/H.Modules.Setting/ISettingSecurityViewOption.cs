namespace H.Services.Common.Setting;

public interface ISettingSecurityViewOption
{
    string Password { get; set; }
    int PasswordCount { get; set; }
    bool RememberPassword { get; set; }
    bool UsePassword { get; set; }
}
