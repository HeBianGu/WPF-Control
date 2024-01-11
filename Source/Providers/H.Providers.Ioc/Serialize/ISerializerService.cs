﻿// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;

namespace H.Providers.Ioc
{
    public interface ISerializerService
    {
        object Load(string filePath, Type type);
        T Load<T>(string filePath);
        void Save(string filePath, object sourceObj, string xmlRootName = null);
    }
}