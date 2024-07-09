// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Services.Common
{
    public interface ISerializerService
    {
        object Load(string filePath, Type type);
        T Load<T>(string filePath);
        void Save(string filePath, object sourceObj, string xmlRootName = null);
    }
}