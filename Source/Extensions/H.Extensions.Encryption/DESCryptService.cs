// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Extensions.Encryption.String;
using H.Services.Common.Crypt;

namespace H.Extensions.Encryption;

public class DESCryptService : ICryptService
{
    public string Decrypt(string value)
    {
        return DESExtension.DecryptDES(value);
    }

    public string Encrypt(string value)
    {
        return DESExtension.EncryptDES(value);
    }
}
