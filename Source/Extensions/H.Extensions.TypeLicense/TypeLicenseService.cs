using H.Extensions.TypeLicense.LicenseProviders;

namespace H.Extensions.TypeLicense;

public class TypeLicenseService : ITypeLicenseService
{
    public bool IsValid<T>(out string message)
    {
        message = null;
        bool result = LicenseManager.IsValid(typeof(T), null, out License license);
        if (!result || license == null)
        {
            message = $"检查许可错误<{typeof(T).Name}>";
            return false;
        }
        if (license is IValidLicense validLicense)
            result = validLicense.IsValid<T>(out message);
        license.Dispose();
        return result;
    }
}
