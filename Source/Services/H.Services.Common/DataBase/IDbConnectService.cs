// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Services.Common
{
    public interface IDbConnectService : ISplashLoad
    {
        bool TryConnect(out string message);
    }
}