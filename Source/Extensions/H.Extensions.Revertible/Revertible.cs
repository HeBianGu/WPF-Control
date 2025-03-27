namespace H.Extensions.Revertible;


public class Revertible : IRevertible
{
    public Revertible()
    {

    }
    public Revertible(string name, object data)
    {
        this.Name = name;
        this.Data = data;
    }
    public string Name { get; }
    public object Data { get; }
    private List<RevertibleAction> _actions = new List<RevertibleAction>();
    public int Count => _actions.Count;
    public void AddAction(Action redo, Action undo)
    {
        _actions.Add(new RevertibleAction(redo, undo));
    }
    public void Undo()
    {
        RevertibleAction.UndoActions(_actions);
    }
    public void Redo()
    {
        RevertibleAction.RedoActions(_actions);
    }
}
