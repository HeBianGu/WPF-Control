using System.Threading.Tasks;

namespace H.Iocable
{
    public abstract class IocAsyncMarkupCommandBase : IocMarkupCommandBase
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

        private string _message;
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                RaisePropertyChanged();
            }
        }

        public override async void Execute(object parameter)
        {
            this.IsExecuting = true;
            try
            {
                await this.ExecuteAsync(parameter);
                
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                this.IsExecuting = false;
            }
     
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

        public virtual Task ExecuteAsync(object parameter)
        {
            return Task.CompletedTask;
        }
    }

}
