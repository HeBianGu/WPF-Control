using System;
using System.Windows.Input;
using System.Windows.Markup;

namespace H.Windows.Dialog
{
    public abstract class DialogCommandBase : MarkupExtension, ICommand
    {
        public int Width { get; set; } = 500;
        public int Height { get; set; } = 300;

        public event EventHandler? CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }
        public virtual bool CanExecute(object? parameter)
        {
            return parameter is Type;
        }

        public abstract void Execute(object? parameter);

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }


    public class ShowDialogIocCommand : DialogCommandBase
    {
        public Type Type { get; set; }

        public override void Execute(object? parameter)
        {
            DialogWindow.ShowIoc(this.Type, x =>
            {
                x.Width = this.Width;
                x.Height = this.Height;
            });
        }

        public override bool CanExecute(object? parameter)
        {
            return this.Type != null;
        }
    }
}
