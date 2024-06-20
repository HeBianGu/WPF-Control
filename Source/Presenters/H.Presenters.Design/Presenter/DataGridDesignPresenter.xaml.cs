using System.ComponentModel.DataAnnotations;

namespace H.Presenters.Design
{
    [Display(Name = "DataGrid")]
    public class DataGridDesignPresenter : DataGridPresenterBase, IDesignPresenter
    {
        public DataGridDesignPresenter()
        {
            this.ColumnSpan = 12;
        }
    }
}
