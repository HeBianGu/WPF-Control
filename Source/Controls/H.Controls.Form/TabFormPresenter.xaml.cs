
namespace H.Controls.Form;

public class TabFormPresenter : FormPresenter
{
    public TabFormPresenter()
    {

    }
    public TabFormPresenter(object value) : base(value)
    {

    }

    public ObservableCollection<string> TabNames { get; set; } = new ObservableCollection<string>();
}
