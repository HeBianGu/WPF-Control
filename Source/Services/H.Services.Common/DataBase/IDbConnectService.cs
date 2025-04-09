// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using H.Common.Interfaces;

namespace H.Services.Common.DataBase;

public interface IDbConnectService : ISplashLoad
{
    bool TryConnect(out string message);
}