// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Services.Serializable.Web;

public interface IWebXmlSerializerService
{
    T Load<T>(string uri, out string message);
}