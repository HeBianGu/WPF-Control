using System.Collections.ObjectModel;

namespace H.Extensions.TextJsonable;

public class MyJsonBindable : JsonBindableBase,IDisposable
{
    public int Age { get; set; }

    //public RelayCommand MyCommand => new RelayCommand(s =>
    //{
      
    //});
    public IDisposable Notify { get; set; }

    public void Dispose()
    {
        throw new NotImplementedException();
    }

    //private ObservableCollection<string> _collection = new ObservableCollection<string>();
    //public ObservableCollection<string> Collection
    //{
    //    get { return _collection; }
    //    set
    //    {
    //        _collection = value;
    //        RaisePropertyChanged();
    //    }
    //}

    //private ObservableCollection<MyJsonBindable> _MyJsonBindables = new ObservableCollection<MyJsonBindable>();
    //public ObservableCollection<MyJsonBindable> MyJsonBindables
    //{
    //    get { return _MyJsonBindables; }
    //    set
    //    {
    //        _MyJsonBindables = value;
    //        RaisePropertyChanged("MyJsonBindables");
    //    }
    //}

    private ObservableCollection<IDisposable> _notifies = new ObservableCollection<IDisposable>();
    public ObservableCollection<IDisposable> Nofities
    {
        get { return _notifies; }
        set
        {
            _notifies = value;
        }
    }

    //public IEnumerable<int> Values { get; set; }

}

public class MyDisposable : IDisposable
{
    public int MyProperty { get; set; }
    public void Dispose()
    {
        throw new NotImplementedException();
    }
}
