
using H.Extensions.DataBase.Repository;

namespace H.Presenters.Repository;

public class RepositoryPresenter
{
    private readonly IRepositoryBindable _viewModel;
    public RepositoryPresenter(Type type)
    {
        Type gType = typeof(IRepositoryBindable<>).MakeGenericType(type);
        this._viewModel = Ioc.GetService<IRepositoryBindable>(gType, true);
    }
    public IRepositoryBindable ViewModel => _viewModel;
}
