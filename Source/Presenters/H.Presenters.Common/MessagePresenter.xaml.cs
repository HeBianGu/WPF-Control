namespace H.Presenters.Common;

public class MessagePresenter : DisplayBindableBase
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
