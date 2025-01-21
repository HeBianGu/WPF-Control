namespace H.Extensions.TypeLicense.LicenseProviders;

public interface IValidLicense
{
    bool IsValid<T>(out string message);
}
public class ValidTypeLicense : TypeLicense, IValidLicense
{
    public ValidTypeLicense()
    {

    }
    public ValidTypeLicense(string key) : base(key)
    {

    }

    public string TypeName { get; set; }

    public virtual bool IsValid<T>(out string message)
    {
        message = null;
        if (typeof(T).Name != this.TypeName)
        {
            message = "许可验证失败，许可类型不匹配";
            return false;
        }
        return true;
    }
}
