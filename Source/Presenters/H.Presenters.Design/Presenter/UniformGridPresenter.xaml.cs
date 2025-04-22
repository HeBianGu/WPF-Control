namespace H.Presenters.Design.Presenter;

[Display(Name = "Uniform")]
public class UniformGridPresenter : PanelPresenterBase
{
    private int _columns = 6;
    [DefaultValue(6)]
    [Display(Name = "列数", GroupName = "常用,布局")]
    public int Columns
    {
        get { return _columns; }
        set
        {
            _columns = value;
            RaisePropertyChanged();
        }
    }

    private int _rows = 6;
    [DefaultValue(6)]
    [Display(Name = "行数", GroupName = "常用,布局")]
    public int Rows
    {
        get { return _rows; }
        set
        {
            _rows = value;
            RaisePropertyChanged();
        }
    }
}
