using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace H.Iocable
{
    [MarkupExtensionReturnType(typeof(ICommand))]
    public abstract class IocMarkupCommandBase : MarkupExtension, ICommand, INotifyPropertyChanged
    {
        protected IocMarkupCommandBase()
        {
            var d = this.GetType().GetCustomAttribute<DisplayAttribute>();
            if (d != null)
            {
                this.Name = d.Name;
                this.Description = d.Description;
            }
        }

        public string Name { get; set; }
        public string Icon { get; set; }
        public string Description { get; set; }

        #region - ICommand -
        public event EventHandler CanExecuteChanged
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
        public virtual bool CanExecute(object parameter)
        {
            return true;
        }
        public abstract void Execute(object parameter);
        #endregion

        #region - MarkupExtension -
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }

        #endregion

        #region - INotifyPropertyChanged -
        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void RaisePropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));

        }
        #endregion
    }

}
