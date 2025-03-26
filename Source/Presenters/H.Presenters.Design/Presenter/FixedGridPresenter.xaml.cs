namespace H.Presenters.Design.Presenter;

[Display(Name = "FixedGrid")]
public class FixedGridPresenter : GridPresenterBase
{
    private int _rows;
    [Display(Name = "行数", GroupName = "常用,样式")]
    public int Rows
    {
        get { return _rows; }
        set
        {
            _rows = value;
            RaisePropertyChanged();
        }
    }


    private int _columns;
    [Display(Name = "列数", GroupName = "常用,样式")]
    public int Columns
    {
        get { return _columns; }
        set
        {
            _columns = value;
            RaisePropertyChanged();
        }
    }


    private GridLength _rowGridLength = GridLength.Auto;
    [Display(Name = "行高", GroupName = "常用,样式")]
    public GridLength RowGridLength
    {
        get { return _rowGridLength; }
        set
        {
            _rowGridLength = value;
            RaisePropertyChanged();
        }
    }


    private GridLength _columnGridLength = GridLength.Auto;
    [Display(Name = "列宽", GroupName = "常用,样式")]
    public GridLength ColumnGridLength
    {
        get { return _columnGridLength; }
        set
        {
            _columnGridLength = value;
            RaisePropertyChanged();
        }
    }

}
