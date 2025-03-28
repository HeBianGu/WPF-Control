using H.Services.AppPath;
using H.Services.Common.Crypt;
using System.IO;

namespace H.Extensions.TypeLicense.LicenseProviders;

public abstract class TypeFileLicenseProviderBase : LicenseProvider
{
    public override License GetLicense(LicenseContext context, Type type, object instance, bool allowExceptions)
    {
        string licenseFile = this.GetLicenseFile(type);
        if (!File.Exists(licenseFile))
            return null;
        using Stream licStream = new FileStream(licenseFile, FileMode.Open, FileAccess.Read, FileShare.Read);
        using StreamReader sr = new StreamReader(licStream);
        string s = sr.ReadLine();
        s = this.GetDecrypt(s);
        return this.CreateLicense(s, type);
        //if (result is IValidLicense validLicense)
        //{
        //    if (!validLicense.IsValid(out string message))
        //        throw new LicenseException(type, instance, message);
        //}
        //return result;
    }

    protected virtual string GetDecrypt(string licStr)
    {
        return IocCrypt.Instance?.Decrypt(licStr) ?? licStr;
    }

    protected virtual string GetLicenseFile(Type type)
    {
        return Path.Combine(AppPaths.Instance.UserLicense, type.Name + ".lic");
    }

    protected abstract License CreateLicense(string key, Type type);
}
