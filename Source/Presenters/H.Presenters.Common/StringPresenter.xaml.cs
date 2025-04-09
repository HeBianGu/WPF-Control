namespace H.Presenters.Common;

[Icon("\xEDE3")]
public class StringPresenter : DisplayBindableBase, IStringPresenter
{
    private string _value;
    public string Value
    {
        get { return _value; }
        set
        {
            _value = value;
            RaisePropertyChanged();
        }
    }
}
