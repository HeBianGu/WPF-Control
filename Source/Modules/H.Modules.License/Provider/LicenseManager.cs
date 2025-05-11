// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

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
