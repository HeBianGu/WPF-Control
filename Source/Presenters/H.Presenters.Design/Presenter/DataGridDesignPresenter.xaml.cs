
using H.Providers.Mvvm;

namespace H.Presenters.Design
{
    [Displayer(Name = "DataGrid", Icon = "\xe890")]
    public class DataGridDesignPresenter : DataGridPresenterBase, IDesignPresenter
    {
        public DataGridDesignPresenter()
        {
            ColumnSpan = 12;
        }
    }
}
