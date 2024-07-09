// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Services.Common
{
    public interface ILoginService
    {
        IUser User { get; }
        bool Login(string name, string password, out string message);
        bool Logout(out string message);
    }
}