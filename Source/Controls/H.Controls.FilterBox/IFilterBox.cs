using H.Providers.Ioc;

namespace H.Controls.FilterBox
{
    public interface IFilterBox
    {
        IFilter Filter { get; }
    }

    public interface IDisplayFilterBox : IFilterBox
    {
        string DisplayName { get; }
    }
}