// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using H.Extensions.Common;
using H.Mvvm.ViewModels.Base;
using System.Windows.Threading;

namespace H.Extensions.ObservableSource;

public class ObservableSource<T> : Bindable, IObservableSource<T>
{
    public int Count => this.Cache.Count;
    private ObservableCollection<T> _cache = new ObservableCollection<T>();
    public ObservableCollection<T> Cache
    {
        get { return _cache; }
        set
        {
            _cache = value;
            RaisePropertyChanged();
        }
    }
    private ObservableCollection<T> _filterSource = new ObservableCollection<T>();
    public ObservableCollection<T> FilterSource
    {
        get { return _filterSource; }
        set
        {
            _filterSource = value;
            RaisePropertyChanged();
        }
    }

    private ObservableCollection<T> _source = new ObservableCollection<T>();
    public ObservableCollection<T> Source
    {
        get { return _source; }
        set
        {
            _source = value;
            RaisePropertyChanged("Source");
        }
    }

    private T _selectedItem;
    public T SelectedItem
    {
        get { return _selectedItem; }
        set
        {
            if (_selectedItem?.Equals(value) == true)
                return;
            _selectedItem = value;
            RaisePropertyChanged();
            this.SelectedIndex = this.FilterSource.IndexOf(value);
        }
    }

    private int _selectedIndex;
    public int SelectedIndex
    {
        get { return _selectedIndex; }
        set
        {
            if (_selectedIndex == value)
                return;
            _selectedIndex = value;
            RaisePropertyChanged();
            this.SelectedItem = this.FilterSource.ElementAtOrDefault(value);
        }
    }

    private int _total;
    public int Total
    {
        get { return _total; }
        set
        {
            _total = value;
            RaisePropertyChanged("Total");
        }
    }

    private int _totalPage;
    public int TotalPage
    {
        get { return _totalPage; }
        set
        {
            _totalPage = value;
            RaisePropertyChanged("TotalPage");
        }
    }

    private int _pageIndex = 1;
    public int PageIndex
    {
        get { return _pageIndex; }
        set
        {
            _pageIndex = value;
            RaisePropertyChanged("PageIndex");
            this.RefreshPage();
        }
    }

    private int _pageCount = 20;
    public int PageCount
    {
        get { return _pageCount; }
        set
        {
            _pageCount = value;
            RaisePropertyChanged("PageCount");
            this.RefreshPage();
        }
    }

    private Predicate<T> _fileter = l => true;
    public Predicate<T> Fileter
    {
        get { return _fileter; }
        set
        {
            _fileter = value;
            RaisePropertyChanged("Fileter");
            this.RefreshPage();
        }
    }

    private int _minValue;
    public int MinValue
    {
        get { return _minValue; }
        set
        {
            _minValue = value;
            RaisePropertyChanged("MinValue");
        }
    }

    private int _maxValue;
    public int MaxValue
    {
        get { return _maxValue; }
        set
        {
            _maxValue = value;
            RaisePropertyChanged("MaxValue");
        }
    }


    private IFilterable _filter1;
    public IFilterable Filter1
    {
        get { return _filter1; }
        set
        {
            _filter1 = value;
            RaisePropertyChanged();
            this.RefreshPage();
        }
    }

    private IFilterable _filter2;
    public IFilterable Filter2
    {
        get { return _filter2; }
        set
        {
            _filter2 = value;
            RaisePropertyChanged();
            this.RefreshPage();
        }
    }

    private IFilterable _filter3;
    public IFilterable Filter3
    {
        get { return _filter3; }
        set
        {
            _filter3 = value;
            RaisePropertyChanged();
            this.RefreshPage();
        }
    }

    private IFilterable _filter4;
    public IFilterable Filter4
    {
        get { return _filter4; }
        set
        {
            _filter4 = value;
            RaisePropertyChanged();
            this.RefreshPage();
        }
    }

    private IFilterable _filter5;
    public IFilterable Filter5
    {
        get { return _filter5; }
        set
        {
            _filter5 = value;
            RaisePropertyChanged();
            this.RefreshPage();
        }
    }

    private IFilterable _filter6;
    /// <summary> 说明  </summary>
    public IFilterable Filter6
    {
        get { return _filter6; }
        set
        {
            _filter6 = value;
            RaisePropertyChanged();
            this.RefreshPage();
        }
    }

    private IFilterable _filter7;
    /// <summary> 说明  </summary>
    public IFilterable Filter7
    {
        get { return _filter7; }
        set
        {
            _filter7 = value;
            RaisePropertyChanged();
            this.RefreshPage();
        }
    }
    private IFilterable _filter8;
    /// <summary> 说明  </summary>
    public IFilterable Filter8
    {
        get { return _filter8; }
        set
        {
            _filter8 = value;
            RaisePropertyChanged();
            this.RefreshPage();
        }
    }
    private IFilterable _filter9;
    /// <summary> 说明  </summary>
    public IFilterable Filter9
    {
        get { return _filter9; }
        set
        {
            _filter9 = value;
            RaisePropertyChanged();
            this.RefreshPage();
        }
    }

    private IFilterable _filter10;
    /// <summary> 说明  </summary>
    public IFilterable Filter10
    {
        get { return _filter10; }
        set
        {
            _filter10 = value;
            RaisePropertyChanged();
            this.RefreshPage();
        }
    }

    private IFilterable _filter11;
    /// <summary> 说明  </summary>
    public IFilterable Filter11
    {
        get { return _filter11; }
        set
        {
            _filter11 = value;
            RaisePropertyChanged();
            this.RefreshPage();
        }
    }

    private IFilterable _filter12;
    /// <summary> 说明  </summary>
    public IFilterable Filter12
    {
        get { return _filter12; }
        set
        {
            _filter12 = value;
            RaisePropertyChanged();
            this.RefreshPage();
        }
    }

    private IFilterable _filter13;
    /// <summary> 说明  </summary>
    public IFilterable Filter13
    {
        get { return _filter13; }
        set
        {
            _filter13 = value;
            RaisePropertyChanged();
            this.RefreshPage();
        }
    }

    private IFilterable _filter14;
    /// <summary> 说明  </summary>
    public IFilterable Filter14
    {
        get { return _filter14; }
        set
        {
            _filter14 = value;
            RaisePropertyChanged();
            this.RefreshPage();
        }
    }

    private IFilterable _filter15;
    /// <summary> 说明  </summary>
    public IFilterable Filter15
    {
        get { return _filter15; }
        set
        {
            _filter15 = value;
            RaisePropertyChanged();
            this.RefreshPage();
        }
    }
    private IOrderWhereable _order1;
    public IOrderWhereable Order1
    {
        get { return _order1; }
        set
        {
            _order1 = value;
            RaisePropertyChanged();
            this.RefreshPage();
        }
    }

    private IOrderWhereable _order2;
    public IOrderWhereable Order2
    {
        get { return _order2; }
        set
        {
            _order2 = value;
            RaisePropertyChanged();
            this.RefreshPage();
        }
    }

    private IOrderWhereable _order3;
    public IOrderWhereable Order3
    {
        get { return _order3; }
        set
        {
            _order3 = value;
            RaisePropertyChanged();
            this.RefreshPage();
        }
    }

    public void RefreshPage(Action after = null)
    {
        Func<List<T>> func = () =>
        {
            IEnumerable<T> where = this.Cache.Where(l => this.Fileter?.Invoke(l) != false).
     Where(x => this.Filter1?.IsMatch(x) != false).
     Where(x => this.Filter2?.IsMatch(x) != false).
     Where(x => this.Filter3?.IsMatch(x) != false).
     Where(x => this.Filter4?.IsMatch(x) != false).
     Where(x => this.Filter5?.IsMatch(x) != false).
     Where(x => this.Filter6?.IsMatch(x) != false).
     Where(x => this.Filter7?.IsMatch(x) != false).
     Where(x => this.Filter8?.IsMatch(x) != false).
     Where(x => this.Filter9?.IsMatch(x) != false).
     Where(x => this.Filter10?.IsMatch(x) != false).
     Where(x => this.Filter11?.IsMatch(x) != false).
     Where(x => this.Filter12?.IsMatch(x) != false).
     Where(x => this.Filter13?.IsMatch(x) != false).
     Where(x => this.Filter14?.IsMatch(x) != false).
     Where(x => this.Filter15?.IsMatch(x) != false);

            if (this.Order1 != null)
                where = this.Order1.Where(where).Cast<T>();
            if (this.Order2 != null)
                where = this.Order2.Where(where).Cast<T>();
            if (this.Order3 != null)
                where = this.Order3.Where(where).Cast<T>();

            this.FilterSource = where.ToObservable();
            this.Total = this.FilterSource.Count;
            int min = (this.PageIndex - 1) * this.PageCount;
            int max = min + this.PageCount;
            this.MinValue = this.Total == 0 ? 0 : (min + 1);
            this.MaxValue = max < this.Total ? max : this.Total;
            this.TotalPage = this.Total % this.PageCount == 0 ? this.Total / this.PageCount : (this.Total / this.PageCount) + 1;
            return where.Skip(this.MinValue - 1).Take(this.PageCount).ToList();
        };
        //this.Source = collection.ToObservable();
        this.DelayInvoke(this.Source, func, () =>
        {

            if (after == null)
                this.SelectedItem = func.Invoke().FirstOrDefault();
            else
                after?.Invoke();
        });


    }

    public void Load(IEnumerable<T> source)
    {
        this.Cache = source?.ToObservable();
        this.RefreshPage();
    }

    public void Add(params T[] value)
    {
        this.Cache.AddRange(value);
        this.RefreshPage();
    }

    public void Remove(params T[] value)
    {
        foreach (T item in value)
        {
            this.Cache.Remove(item);
        }
        this.RefreshPage();
    }

    public void RemoveAll(Func<T, bool> predicate)
    {
        this.Cache.RemoveAll(predicate);
        this.RefreshPage();
    }

    public IEnumerable<T> Where(Func<T, bool> predicate)
    {
        return this.Cache.Where(predicate);
    }

    public void Foreach(Action<T> predicate)
    {
        this.Cache.Foreach(predicate);
    }

    public void Clear()
    {
        this.Cache.Clear();
        this.RefreshPage();
    }

    public void Sort<TKey>(Func<T, TKey> keySelector, bool isdesc)
    {
        if (isdesc)
        {
            this.Cache.OrderBy(keySelector);
        }
        else
        {
            this.Cache.OrderByDesc(keySelector);
        }
        this.RefreshPage();
    }

    public T FirstOrDefault(Func<T, bool> predicate)
    {
        return this.Cache.FirstOrDefault(predicate);
    }

    public bool Any(Func<T, bool> predicate)
    {
        return this.Cache.Any(predicate);
    }

    public IEnumerable<TResult> Select<TResult>(Func<T, TResult> predicate)
    {
        return this.Cache.Select(predicate);
    }

    public void Next()
    {
        if (this.SelectedItem == null)
            return;
        int index = this.FilterSource.IndexOf(this.SelectedItem);
        if (index >= this.Total - 1 || index == -1)
            index = 0;
        else
            index++;
        int cPage = (index / this.PageCount) + 1;
        if (cPage != this._pageIndex)
        {
            this._pageIndex = cPage;
            this.RefreshPage(() => this.SelectedItem = this.FilterSource[index]);
        }
        else
        {
            this._pageIndex = cPage;
            this.SelectedItem = this.FilterSource[index];
        }
    }

    public void Previous()
    {
        if (this.SelectedItem == null)
            return;
        int index = this.FilterSource.IndexOf(this.SelectedItem);
        if (index == 0 || index == -1)
            index = this.FilterSource.Count - 1;
        else
            index--;
        int cPage = (index / this.PageCount) + 1;
        if (cPage != this._pageIndex)
        {
            this._pageIndex = cPage;
            this.RefreshPage(() => this.SelectedItem = this.FilterSource[index]);
        }
        else
        {
            this._pageIndex = cPage;
            this.SelectedItem = this.FilterSource[index];
        }
    }
}
