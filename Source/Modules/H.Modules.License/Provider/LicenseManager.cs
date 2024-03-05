// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;

namespace H.Modules.License
{
    public class LicenseManager : ILicenseManager
    {
        public string GetHostID()
        {
            return SystemInfo.Instance.HostID;
        }

        public Tuple<string, string> GetPK()
        {
            RSAHelper.RSAKey keyPair = RSAHelper.GetRASKey();


            return Tuple.Create(keyPair.PublicKey, keyPair.PrivateKey);

        }

        public string Encrypt(string source, string key)
        {
            return RSAHelper.EncryptString(source, key);
        }

        public string Decrypt(string source, string key)
        {
            return RSAHelper.DecryptString(source, key);
        }
    }
}
