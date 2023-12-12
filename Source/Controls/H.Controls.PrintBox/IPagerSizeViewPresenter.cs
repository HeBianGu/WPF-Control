using System.Collections.ObjectModel;

namespace H.Controls.PrintBox
{
    public interface IPagerSizeViewPresenter
    {
        ObservableCollection<PageSize> Collection { get; }
        PageSize SelectedPagerSizeData { get; }
    }
}
