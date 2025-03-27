namespace H.Extensions.TypeLicense.LicenseProviders;

public class DefaultTypeFileLicenseProvider : TypeFileLicenseProviderBase
{
    const string Format = "{0} is a licensed component.";
    protected override License CreateLicense(string key, Type type)
    {
        if (key.StartsWith(string.Format(Format, type.Name)))
            return new TypeLicense(key);
        return null;
    }
}
