// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

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
