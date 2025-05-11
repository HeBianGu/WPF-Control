// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Extensions.Command;

/// <summary>
/// 带有绑定百分比的命令
/// </summary>
public class PercentCommand : BuzyCommand<PercentCommand>
{
    private bool AutoMessage { get; set; } = true;

    /// <summary> 执行命令 </summary>
    public PercentCommand(Action<PercentCommand, object> execute, bool autoMesaage = true) : base(execute)
    {
        this.AutoMessage = autoMesaage;
    }

    /// <summary> 执行命令 </summary>
    public PercentCommand(Action<PercentCommand, object> execute, Predicate<object> canExecute, bool autoMesaage = true) : base(execute, canExecute)
    {
        this.AutoMessage = autoMesaage;
    }

    private double _value;
    /// <summary> 当前值  </summary>
    public double Value
    {
        get { return _value; }
        set
        {
            _value = value;
            RaisePropertyChanged();

            if (!this.AutoMessage) return;

            this.Message = value + "%";
        }
    }

    public override async void Execute(object parameter)
    {
        this.Value = 0.0;

        this.IsBuzy = true;

        this.Message = "正在执行";

        this.Exception = null;

        await Task.Run(() =>
        {
            try
            {
                this.Action?.Invoke(this, parameter);

                this.Message = "执行成功";
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
                this.Exception = ex;
            }
            finally
            {
                this.IsBuzy = false;
            }
        });
    }
}
