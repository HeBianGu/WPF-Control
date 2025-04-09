namespace H.Mvvm.ViewModels;

public interface IDisplayBindable : IIconable, INameable, IOrderable, IGroupable, IDable, IDescriptionable
{

    string ShortName { get; set; }
}
