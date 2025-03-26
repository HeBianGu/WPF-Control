namespace H.Presenters.Design.Presenter;

[Display(Name = "DataGrid")]
public class DataGridDesignPresenter : DataGridPresenterBase, IDesignPresenter
{
    public DataGridDesignPresenter()
    {
        this.ColumnSpan = 12;
    }
}
