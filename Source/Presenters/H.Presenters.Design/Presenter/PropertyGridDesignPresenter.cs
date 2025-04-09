namespace H.Presenters.Design.Presenter;

[Display(Name = "属性列表")]
public class PropertyGridDesignPresenter : CommandsDesignPresenterBase
{
    private object _data;
    [Display(Name = "数据源", GroupName = "常用,数据")]
    public object Data
    {
        get { return _data; }
        set
        {
            _data = value;
            RaisePropertyChanged();
        }
    }
}
