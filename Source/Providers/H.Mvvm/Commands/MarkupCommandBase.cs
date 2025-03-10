using H.Mvvm.Attributes;
using H.Mvvm.ViewModels.Base;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Windows.Input;
using System.Windows.Markup;

namespace H.Mvvm
{
    [MarkupExtensionReturnType(typeof(ICommand))]
    public abstract class MarkupCommandBase : MarkupExtension, ICommand, INotifyPropertyChanged, IIconable, INameable, IDescriptionable
    {
        protected MarkupCommandBase()
        {
            var d = this.GetType().GetCustomAttribute<DisplayAttribute>();
            if (d != null)
            {
                this.Name = d.Name;
                this.Description = d.Description;
            }

            var icon = this.GetType().GetCustomAttribute<IconAttribute>();
            this.Icon = icon?.Icon;
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
