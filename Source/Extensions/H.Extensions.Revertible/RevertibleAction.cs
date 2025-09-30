// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

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
