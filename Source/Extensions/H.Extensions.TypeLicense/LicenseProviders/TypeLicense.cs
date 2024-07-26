using System.Text.Json.Serialization;

namespace H.Extensions.TypeLicense.LicenseProviders;

public interface ITypeLicense
{
    string LicenseKey { get; }
}

public class TypeLicense : License, ITypeLicense
{
    public TypeLicense()
    {

    }
    public TypeLicense(string key)
    {
        this.LicenseKey = key;
    }

    [JsonIgnore]
    public override string LicenseKey { get; }

    public override void Dispose()
    {

    }
}
