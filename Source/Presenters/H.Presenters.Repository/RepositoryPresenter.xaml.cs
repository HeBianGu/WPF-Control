using H.Extensions.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Presenters.Repository
{
    public class RepositoryPresenter
    {
        private readonly IRepositoryViewModel _viewModel;

        public RepositoryPresenter(Type type)
        {
            var gType = typeof(IRepositoryViewModel<>).MakeGenericType(type);
            this._viewModel = Ioc.GetService<IRepositoryViewModel>(gType, true);
        }
        public IRepositoryViewModel ViewModel => _viewModel;
    }
}
