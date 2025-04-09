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
