using H.Common.Interfaces;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace H.Extensions.IocPresenter;

public class IocTitlePresenterBase<T, Interface> : IocDisplayBindableBase<T, Interface>, ITitleable where T : class, Interface, new()
{
    public IocTitlePresenterBase()
    {
        TypeDescriptor.GetAttributes(this).OfType<DisplayAttribute>();
        this.Title = TypeDescriptor.GetClassName(this);
    }
    public string Title { get; set; }
}
