global using H.Services.Message.Dialog;

namespace H.Presenters.Common;

public class PercentPresenter : DisplayBindableBase, IPercentPresenter
{
    private int _value;
    public int Value
    {
        get { return _value; }
        set
        {
            _value = value;
            RaisePropertyChanged();
        }
    }
}
