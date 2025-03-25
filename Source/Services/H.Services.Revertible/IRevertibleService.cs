namespace H.Services.Revertible;

public interface IRevertibleService
{
    IReadOnlyCollection<IRevertible> Revertibles { get; }
    int Position { get; }
    void Cancel();
    void Commit();
    RevertibleDisposer Begin(string name = null, object data = null);
    IRevertible Current { get; }
    void Redo();
    void Undo();
    bool CanUndo { get; }
    bool CanRedo { get; }
}
