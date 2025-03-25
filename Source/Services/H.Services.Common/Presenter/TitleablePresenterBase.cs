using H.Common.Interfaces;

namespace H.Services.Common.Presenter;

public class TitleablePresenterBase<T, Interface> : Ioc<T, Interface>, ITitleable where T : class, Interface, new()
{
    public TitleablePresenterBase()
    {
        TypeDescriptor.GetAttributes(this).OfType<DisplayAttribute>();
        this.Title = TypeDescriptor.GetClassName(this);
    }
    public string Title { get; set; }
}
