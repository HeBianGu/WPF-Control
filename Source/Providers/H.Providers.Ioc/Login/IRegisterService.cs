// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

namespace H.Providers.Ioc
{
    public interface IRegisterService
    {
        bool Register(string phone, string password, out string message);
        bool ResetPassword(string phone, string password, out string message);
    }
}