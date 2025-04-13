// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Modules.Login
{
    public interface ILoginOption
    {
        string Company { get; set; }
        string Copyright { get; set; }
        string LastPassword { get; set; }
        string LastUserName { get; set; }
        string Logo { get; set; }
        string Password { get; set; }
        string PasswordRegular { get; set; }
        string Product { get; set; }
        int ProductFontSize { get; set; }
        bool Remember { get; set; }
        string Title { get; set; }
        string UserName { get; set; }
        string Version { get; set; }
    }
}
