using H.Mvvm.ViewModels.Base;

namespace H.Mvvm
{
    public interface IDisplayBindable : IIconable, INameable, IOrderable, IGroupable, IDable, IDescriptionable
    {
        
        string ShortName { get; set; }
    }

}
