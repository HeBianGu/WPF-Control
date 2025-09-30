// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

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
