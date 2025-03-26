namespace H.Modules.Setting;

public interface ISettingSecurityViewOption
{
    string Password { get; set; }
    int PasswordCount { get; set; }
    bool RememberPassword { get; set; }
    bool UsePassword { get; set; }
}
