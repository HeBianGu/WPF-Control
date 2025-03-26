namespace H.Common.Commands;

public abstract class AsyncMarkupCommandBase : MarkupCommandBase
{
    private bool _isExecuting;
    public override async void Execute(object parameter)
    {
        this._isExecuting = true;
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
            this._isExecuting = false;
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
        return this._isExecuting == false;
    }

    public virtual Task ExecuteAsync(object parameter)
    {
        return Task.CompletedTask;
    }
}
