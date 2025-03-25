namespace H.Mvvm.ViewModels.Base;

public abstract class GroupDisplayBindableBase<T> : DisplayBindableBase
{
    public GroupDisplayBindableBase()
    {
        this.NodeDatas = new ObservableCollection<T>(this.CreateNodeDatas());
    }
    private ObservableCollection<T> _nodeDatas = new ObservableCollection<T>();
    public ObservableCollection<T> NodeDatas
    {
        get { return _nodeDatas; }
        set
        {
            _nodeDatas = value;
            RaisePropertyChanged();
        }
    }

    protected abstract IEnumerable<T> CreateNodeDatas();
}
