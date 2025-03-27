using System.Text.Json;

namespace H.Extensions.TypeLicense.LicenseProviders;

public class JsonTypeFileLicenseProvider<T> : TypeFileLicenseProviderBase where T : License
{
    protected override License CreateLicense(string key, Type type)
    {
		try
		{
            return JsonSerializer.Deserialize<T>(key);
        }
		catch (Exception ex)
		{
			return null;
		}
    }
}

public class JsonTypeFileLicenseProvider : JsonTypeFileLicenseProvider<TypeLicense>
{

}
