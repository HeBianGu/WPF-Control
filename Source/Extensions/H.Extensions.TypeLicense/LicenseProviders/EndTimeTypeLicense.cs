namespace H.Extensions.TypeLicense.LicenseProviders;

public class EndTimeTypeLicense : ValidTypeLicense
{
    public EndTimeTypeLicense()
    {

    }

    public DateTime EndTime { get; set; }

    public override bool IsValid<T>(out string message)
    {
        if (this.EndTime < DateTime.Now)
        {
            message = $"该许可已于{this.EndTime}到期";
            return false;
        }
        return base.IsValid<T>(out message);
    }
}
