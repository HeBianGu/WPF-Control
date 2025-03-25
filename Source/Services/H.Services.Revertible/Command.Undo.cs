using H.Common.Attributes;
using H.Common.Commands;
using System.ComponentModel.DataAnnotations;

namespace H.Services.Revertible;

[Icon("\xE7A7")]
[Display(Name = "撤销", Description = "撤销当前操作")]
public class UndoCommand : DisplayMarkupCommandBase
{
    public override void Execute(object parameter)
    {
        Ioc<IRevertibleService>.Instance?.Undo();
    }
    public override bool CanExecute(object parameter)
    {
        return Ioc<IRevertibleService>.Instance?.CanUndo == true;
    }
}
