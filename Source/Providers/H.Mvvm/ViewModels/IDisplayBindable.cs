using H.Mvvm.ViewModels.Base;

namespace H.Mvvm
{
    public interface IDisplayBindable : IIconable, INameable, IOrderable,IGroupable,IDable
    {
        string Description { get; set; }
        string ShortName { get; set; }
    }

}
