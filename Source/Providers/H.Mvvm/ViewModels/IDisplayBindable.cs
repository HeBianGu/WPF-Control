using H.Mvvm.ViewModels.Base;

namespace H.Mvvm
{
    public interface IDisplayBindable : IIconable, INameable
    {
        string Description { get; set; }
        string GroupName { get; set; }
        string ID { get; set; }
        int Order { get; set; }
        string ShortName { get; set; }
    }

}
