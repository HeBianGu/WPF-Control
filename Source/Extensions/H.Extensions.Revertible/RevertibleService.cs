using H.Mvvm.ViewModels.Base;
using Microsoft.Extensions.Options;
using System.Collections.ObjectModel;

namespace H.Extensions.Revertible;

public class RevertibleService : BindableBase, IRevertibleService
{
    private readonly IOptions<RevertibleOptions> _options;
    public RevertibleService(IOptions<RevertibleOptions> options)
    {
        this._options = options;
        if (options.Value.AutoInitializedOnCreate)
            this.Initialize();
    }

    public bool CanUndo => this._position >= 0 && this.IsInitialized;

    public bool CanRedo => this._position < this._revertibles.Count - 1 && this.IsInitialized;

    public bool IsInitialized { get; private set; }

    private ObservableCollection<IRevertible> _revertibles = new ObservableCollection<IRevertible>();

    public IReadOnlyCollection<IRevertible> Revertibles
    {
        get { return this._revertibles; }
    }

    private IRevertible _current;
    public IRevertible Current => this._current;

    private int _position = -1;
    public int Position
    {
        get { return this._position; }
        private set
        {
            _position = value;
            RaisePropertyChanged();
        }
    }

    public void Initialize()
    {
        this.IsInitialized = true;
        this._position = -1;
        this._revertibles.Clear();
    }

    public RevertibleDisposer Begin(string name = null, object data = null)
    {
        System.Diagnostics.Trace.Assert(this._current == null, "请先提交或取消上一个操作");
        this._current = new Revertible(name, data);
        return new RevertibleDisposer(this);
    }

    /// <summary>
    /// 提交到历史记录
    /// </summary>
    public void Commit()
    {
        if (this.IsInitialized == false)
        {
            this._current = null;
            return;
        }
        System.Diagnostics.Trace.Assert(this._current != null, "请先开始一个操作");
        int count = this._revertibles.Count - this.Position - 1;
        if (count > 0)
        {
            for (int i = 0; i < count; i++)
            {
                this._revertibles.RemoveAt(this.Position + 1);
            }
        }
        this.Position++;
        this._revertibles.Add(_current);
        this._current = null;
        this.RefreshHistory();
    }

    private void RefreshHistory()
    {
        if (this._options.Value.Capacity > 0 && this._revertibles.Count > this._options.Value.Capacity)
        {
            int num = _revertibles.Count - this._options.Value.Capacity;
            for (int i = 0; i < num; i++)
            {
                this._revertibles.RemoveAt(0);
            }
            this.Position -= num;
        }
    }
    /// <summary>
    /// 不提交历史记录
    /// </summary>
    public void Cancel()
    {
        if (this.IsInitialized == false)
        {
            this._current = null;
            return;
        }
        System.Diagnostics.Trace.Assert(this._current != null, "请先开始一个操作");
        this._current.Undo();
        this._current = null;
    }

    public void Redo()
    {
        if (this.CanRedo)
        {
            IRevertible revertible = this._revertibles[++this.Position];
            revertible.Redo();
        }
    }

    public void Undo()
    {
        if (this.CanUndo)
        {
            IRevertible revertible = this._revertibles[this.Position--];
            revertible.Undo();
        }
    }
}
