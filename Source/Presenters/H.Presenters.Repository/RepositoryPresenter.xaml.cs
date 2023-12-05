using H.Extensions.ViewModel;
using System;

namespace H.Presenters.Repository
{
    public class RepositoryPresenter
    {
        private readonly IRepositoryViewModel _viewModel;
        public RepositoryPresenter(Type type)
        {
            Type gType = typeof(IRepositoryViewModel<>).MakeGenericType(type);
            this._viewModel = Ioc.GetService<IRepositoryViewModel>(gType, true);
        }
        public IRepositoryViewModel ViewModel => _viewModel;
    }
}
