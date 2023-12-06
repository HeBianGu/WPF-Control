// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;

namespace H.Modules.License
{
    public interface ILicenseManager
    {
        string Decrypt(string source, string key);
        string Encrypt(string source, string key);
        string GetHostID();
        Tuple<string, string> GetPK();
    }
}