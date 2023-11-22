// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

namespace H.Providers.Ioc
{
    public interface IDbConnectService : ISplashLoad
    {
        /// <summary>
        /// 初始化启动尝试连接
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        bool TryConnect(out string message);
    }
}