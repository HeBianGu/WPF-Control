// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;

namespace H.Providers.Ioc
{
    public interface ICryptService
    {
        string Encrypt(string value);
        string Decrypt(string value);
    }

    public class CryptProxy : Ioc<ICryptService>
    {

    }
}