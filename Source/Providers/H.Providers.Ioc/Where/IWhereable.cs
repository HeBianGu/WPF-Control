namespace H.Services.Common
{
    public interface IWhereable
    {
        IEnumerable Where(IEnumerable from);
    }
}
