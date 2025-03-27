// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Mvvm.ViewModels.Tree;

public partial class TreeNodeBase<T> : SelectBindable<T>, ITreeNode, ISearchable
{
    public TreeNodeBase(T t) : base(t)
    {

    }

    public TreeNodeBase<T> TreeNodeEntity { get; set; }

    private bool? _isChecked = false;
    public bool? IsChecked
    {
        get { return _isChecked; }
        set
        {
            _isChecked = value;
            RaisePropertyChanged();
            RefreshParentCheckState();
            RefreshChildrenCheckState();
        }
    }

    private bool _isCheckable = false;
    public bool IsCheckable
    {
        get { return _isCheckable; }
        set
        {
            _isCheckable = value;
            RaisePropertyChanged();
        }
    }

    private void RefreshParentCheckState()
    {
        if (this.Parent == null)
            return;

        bool allChecked = this.Parent.Nodes.All(l => l.IsChecked == true);
        if (allChecked)
        {
            this.Parent.CheckOnlyCurrent(true);
            this.Parent.RefreshParentCheckState();
            return;
        }

        bool allUnChecked = this.Parent.Nodes.All(l => l.IsChecked == false);
        if (allUnChecked)
        {
            this.Parent.CheckOnlyCurrent(false);
            this.Parent.RefreshParentCheckState();
            return;
        }

        this.Parent.CheckOnlyCurrent(null);
        this.Parent.RefreshParentCheckState();

    }

    private void RefreshChildrenCheckState()
    {
        foreach (TreeNodeBase<T> item in this.Nodes)
        {
            item.CheckOnlyCurrent(this.IsChecked);
            item.RefreshChildrenCheckState();
        }
    }

    private void CheckOnlyCurrent(bool? value)
    {
        _isChecked = value;
        RaisePropertyChanged("IsChecked");
    }

    private void SetIsVisible(bool value)
    {
        _visibility = value ? Visibility.Visible : Visibility.Collapsed;
        RaisePropertyChanged("Visibility");
    }

    private void SetParentVisible(bool value)
    {
        if (this.Parent == null) return;

        if (this.Visibility == Visibility.Visible)
        {
            //  Do ：递归设置父节点选中
            this.Parent.SetIsVisible(true);
            this.Parent.SetParentVisible(true);
        }
        else
        {

            bool isAllFalse = !this.Parent.Nodes.All(l => l.Visibility != Visibility.Visible);
            this.Parent.SetIsVisible(isAllFalse);
            this.Parent.SetParentVisible(isAllFalse);
        }
    }

    private void SetChildVisible(bool value)
    {
        foreach (TreeNodeBase<T> item in this.Nodes)
        {
            item.SetIsVisible(value);
            item.SetChildVisible(value);
        }
    }
    private Visibility _visibility;
    public Visibility Visibility
    {
        get { return _visibility; }
        set
        {
            _visibility = value;
            RaisePropertyChanged("Visibility");
            //this.SetParentVisible(value == Visibility.Visible);
            //this.SetChildVisible(value == Visibility.Visible);
        }
    }

    private bool _isExpanded;
    public bool IsExpanded
    {
        get { return _isExpanded; }
        set
        {
            _isExpanded = value;
            RaisePropertyChanged("IsExpanded");
        }
    }

    public bool IsLoaded { get; set; }

    public TreeNodeBase<T> Parent { get; set; }

    private ObservableCollection<TreeNodeBase<T>> _nodes = new ObservableCollection<TreeNodeBase<T>>();

    public ObservableCollection<TreeNodeBase<T>> Nodes
    {
        get { return _nodes; }
        set
        {
            _nodes = value;
            RaisePropertyChanged();
        }
    }

    public void AddNode(TreeNodeBase<T> node)
    {
        node.Parent = this;
        this.Nodes.Add(node);
    }

    public void Foreach(Action<TreeNodeBase<T>> action)
    {
        foreach (TreeNodeBase<T> node in this.Nodes)
        {
            action?.Invoke(node);
            node.Foreach(action);
        }
    }

    public IEnumerable<TreeNodeBase<T>> FindAll(Predicate<TreeNodeBase<T>> action = null)
    {
        foreach (TreeNodeBase<T> node in this.Nodes)
        {
            if (action?.Invoke(node) != false)
                yield return node;
            IEnumerable<TreeNodeBase<T>> finds = node.FindAll(action);

            foreach (TreeNodeBase<T> item in finds)
            {
                yield return item;
            }
        }
    }

    public IEnumerable<TreeNodeBase<T>> FindAllParent(Predicate<TreeNodeBase<T>> action = null)
    {
        if (this.Parent != null)
        {
            if (action?.Invoke(this.Parent) != false)
                yield return this.Parent;
            this.Parent.FindAllParent(action);
        }
    }

    public override bool Filter(string txt)
    {
        foreach (TreeNodeBase<T> item in this.Nodes)
        {
            item.Filter(txt);
        }
        bool r = this.Nodes.Any(x => x.Visibility == Visibility.Visible) || base.Filter(txt);
        this.Visibility = r ? Visibility.Visible : Visibility.Collapsed;
        return r;
    }
}
