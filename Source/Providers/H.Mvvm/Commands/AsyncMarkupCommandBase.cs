using System;
using System.Threading.Tasks;

namespace H.Mvvm
{
    public abstract class AsyncMarkupCommandBase : MarkupCommandBase
    {
        private bool _isExecuting;
        public bool IsExecuting
        {
            get { return _isExecuting; }
            set
            {
                _isExecuting = value;
                RaisePropertyChanged();
            }
        }
        public override async void Execute(object parameter)
        {
            this.IsExecuting = true;
            await this.ExecuteAsync(parameter);
            this.IsExecuting = false;
            //try
            //{
            //    await this.SingletonExecute(parameter);
            //}
            //catch (Exception)
            //{
            //    throw;
            //}
            //finally
            //{
            //    this._canExcute = true;
            //}
        }
        public override bool CanExecute(object parameter)
        {
            return this.IsExecuting == false;
        }

        public abstract Task ExecuteAsync(object parameter);
    }
}
