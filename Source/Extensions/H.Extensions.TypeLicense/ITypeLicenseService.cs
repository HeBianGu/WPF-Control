namespace H.Extensions.TypeLicense;

public interface ITypeLicenseService
{
    bool IsValid<T>(out string message);
}
