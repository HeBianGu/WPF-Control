// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Mvvm.Commands;
using H.Mvvm.ViewModels.Base;
using H.Presenters.ParameterInfo.Base;
using H.Services.Message;

namespace H.Presenters.ParameterInfo.Presenters;

public class ParameterInfoPresenter : BindableBase, IParameterInfoPresenter
{
    private readonly ISetParameter _SetParameter;
    public ParameterInfoPresenter(ISetParameter setParameter, IParameterInfo parameterInfo)
    {
        this.ParameterInfo = parameterInfo;
        this._SetParameter = setParameter;
        this.GroupName = parameterInfo.Category;
        this.UseSetDefault = parameterInfo.DefaultValue != null && parameterInfo.Access.HasFlag(ParameterAccess.Write);
        this.IsReadOnly = !parameterInfo.Access.HasFlag(ParameterAccess.Write);
    }

    private IParameterInfo _ParameterInfo;
    public IParameterInfo ParameterInfo
    {
        get { return _ParameterInfo; }
        set
        {
            _ParameterInfo = value;
            RaisePropertyChanged();
        }
    }

    private string _GroupName;
    public string GroupName
    {
        get { return _GroupName; }
        set
        {
            _GroupName = value;
            RaisePropertyChanged();
        }
    }


    private bool _UseSetDefault;
    public bool UseSetDefault
    {
        get { return _UseSetDefault; }
        set
        {
            _UseSetDefault = value;
            RaisePropertyChanged();
        }
    }


    private bool _IsReadOnly;
    public bool IsReadOnly
    {
        get { return _IsReadOnly; }
        set
        {
            _IsReadOnly = value;
            RaisePropertyChanged();
        }
    }



    public RelayCommand SetDefaultCommand => new RelayCommand(x =>
    {
        this.CurrentValue = this.ParameterInfo.DefaultValue;
    }, x => this.ParameterInfo != null && this.ParameterInfo.Access.HasFlag(ParameterAccess.Write) && this.UseSetDefault);

    public RelayCommand ExecuteCommandCommand => new RelayCommand(x =>
    {
        var t = this._SetParameter.ExecuteCommand(this.ParameterInfo.Name);
        if (!t.successed)
        {
            this.Message = t.message;
            IocMessage.Snack.ShowError(t.message);
            return;
        }
        IocMessage.Snack.ShowSuccess(t.message ?? $"执行{this.ParameterInfo.DisplayName}成功");
    }, x => this.ParameterInfo != null && this.ParameterInfo.Access.HasFlag(ParameterAccess.Write));


    private string _Message;
    public string Message
    {
        get { return _Message; }
        set
        {
            _Message = value;
            RaisePropertyChanged();
        }
    }

    public object CurrentValue
    {
        get { return this.ParameterInfo.CurrentValue; }
        set
        {
            var message = ValidateParameterValue(this.ParameterInfo, value, out object convertValue);
            if (message != null)
            {
                this.Message = message;
                IocMessage.Snack.ShowError(message);
                return;
            }
            var t = this._SetParameter.SetParameter(this.ParameterInfo.Name, convertValue);
            if (!t.successed)
            {
                this.Message = t.message;
                IocMessage.Snack.ShowError(t.message);
                return;
            }
            IocMessage.Snack.ShowSuccess(t.message ?? $"设置{this.ParameterInfo.DisplayName}成功");
            this.ParameterInfo.CurrentValue = convertValue;
            RaisePropertyChanged();
        }
    }


    private static string ValidateParameterValue(IParameterInfo parameterInfo, object value, out object convertValue)
    {
        convertValue = null;
        if (parameterInfo == null)
            return null;

        string displayName = string.IsNullOrWhiteSpace(parameterInfo.DisplayName) ? parameterInfo.Name : parameterInfo.DisplayName;

        switch (parameterInfo.Type)
        {
            case ParameterType.Integer:
                if (!TryConvertToDecimal(value, out decimal integerValue))
                    return $"{displayName} 必须是整数";

                if (integerValue != decimal.Truncate(integerValue))
                    return $"{displayName} 必须是整数";

                convertValue = integerValue;
                return ValidateRange(displayName, integerValue, parameterInfo.Minimum, parameterInfo.Maximum);

            case ParameterType.Float:
                if (!TryConvertToDecimal(value, out decimal floatValue))
                    return $"{displayName} 必须是数字";
                convertValue = floatValue;
                return ValidateRange(displayName, floatValue, parameterInfo.Minimum, parameterInfo.Maximum);

            case ParameterType.Boolean:
                if (value is bool)
                    return null;
                if (bool.TryParse(value.ToString(), out _))
                {
                    convertValue = value;
                    return null;
                }
                return $"{displayName} 必须是布尔值";

            case ParameterType.Enum:
                if (parameterInfo.Options == null || parameterInfo.Options.Count == 0)
                    return null;
                if (parameterInfo.Options.Contains(value.ToString()))
                {
                    convertValue = value.ToString();
                    return null;
                }
                return $"{displayName} 必须是有效选项";

            case ParameterType.String:
            case ParameterType.Command:
            default:
                convertValue = value.ToString();
                return null;
        }
    }

    private static string ValidateRange(string displayName, decimal value, object minimum, object maximum)
    {
        if (minimum != null && TryConvertToDecimal(minimum, out decimal min) && value < min)
            return $"{displayName} 不能小于最小值 {min}";

        if (maximum != null && TryConvertToDecimal(maximum, out decimal max) && value > max)
            return $"{displayName} 不能大于最大值 {max}";

        return null;
    }

    private static bool TryConvertToDecimal(object value, out decimal result)
    {
        result = default;

        if (value == null)
            return false;

        try
        {
            if (value is decimal decimalValue)
            {
                result = decimalValue;
                return true;
            }

            if (value is IConvertible convertible)
            {
                result = convertible.ToDecimal(System.Globalization.CultureInfo.InvariantCulture);
                return true;
            }

            return decimal.TryParse(value.ToString(), System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.InvariantCulture, out result) ||
                   decimal.TryParse(value.ToString(), out result);
        }
        catch
        {
            return decimal.TryParse(value.ToString(), System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.InvariantCulture, out result) ||
                   decimal.TryParse(value.ToString(), out result);
        }
    }
}


