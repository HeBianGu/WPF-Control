namespace H.Extensions.Revertible;

public class RevertibleAction
{
    private readonly Action _redo;
    private readonly Action _undo;
    public Action Redo => _redo;
    public Action Undo => _undo;
    public RevertibleAction(Action redo, Action undo)
    {
        _redo = redo;
        _undo = undo;
    }
    public static void UndoActions(IList<RevertibleAction> actions)
    {
        for (int num = actions.Count - 1; num >= 0; num--)
        {
            actions[num].Undo();
        }
    }
    public static void RedoActions(IList<RevertibleAction> actions)
    {
        for (int i = 0; i < actions.Count; i++)
        {
            actions[i].Redo();
        }
    }
}
