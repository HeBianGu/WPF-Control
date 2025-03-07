using H.Mvvm.ViewModels.Base;

namespace H.Mvvm
{
    public interface IDisplayBindable : IIconable, INameable, IOrderable
    {
        string Description { get; set; }
        string GroupName { get; set; }
        string ID { get; set; }
        string ShortName { get; set; }
    }

    public interface IOrderable
    {
        int Order { get; set; }
    }

}
