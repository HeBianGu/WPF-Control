// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using H.Extensions.Mvvm.Commands;
global using H.Services.Message;
global using System.ComponentModel.DataAnnotations;
using H.Extensions.FontIcon;
using H.Mvvm.Commands;
using H.Mvvm.ViewModels.Base;

namespace H.Presenters.Common;

public class ParameterInfosPresenter : DisplayBindableBase
{
    private readonly IParameters _parameters;
    public ParameterInfosPresenter(IParameters parameters)
    {
        this._parameters = parameters;
        this.UpdateParameterInfos();
    }

    private void UpdateParameterInfos()
    {
        this.ParameterInfoPresenters = this._parameters.GetParameterInfos().Select(x => new ParameterInfoPresenter(this._parameters, x)).OfType<IParameterInfoPresenter>().ToObservable();
    }
    private ObservableCollection<IParameterInfoPresenter> _ParameterInfoPresenters = new ObservableCollection<IParameterInfoPresenter>();

    public ObservableCollection<IParameterInfoPresenter> ParameterInfoPresenters
    {
        get { return _ParameterInfoPresenters; }
        set
        {
            _ParameterInfoPresenters = value;
            RaisePropertyChanged();
        }
    }

    //[Icon(FontIcons.Setting)]
    //[Display(Name = "设置设备参数", GroupName = "参数设置", Description = "设置字符串参数")]
    //public DisplayCommand SetParamterCommand => new DisplayCommand(async x =>
    //{
    //    var r = await IocMessage.Form.ShowEdit(_setparameter, x => x.Title = "设置设备参数");
    //    if (r != true)
    //        return;
    //    var cr = await _setparameter.SetCameraParameterAsync(this._camera);
    //    if (!cr.Succeeded)
    //    {
    //        await IocMessage.ShowDialogMessage(cr.Message);
    //        this.Message = cr.Message;
    //        return;
    //    }
    //    await this.UpdateCameraParameterInfos();
    //}, x => this.IsOpen && !this.IsAcquiring);


    //[Icon(FontIcons.Add)]
    //[Display(Name = "添加设备参数", GroupName = "参数设置", Description = "设置字符串参数")]
    //public DisplayCommand GetParamterCommand => new DisplayCommand(async x =>
    //{
    //    var r = await IocMessage.Form.ShowEdit(_getparameter, x => x.Title = "添加设备参数");
    //    if (r != true)
    //        return;
    //    var info = _getparameter.GetCameraParameterInfo(this._camera);
    //    if (info == null)
    //    {
    //        await IocMessage.ShowDialogMessage("添加设备参数错误");
    //        this.Message = "添加设备参数错误";
    //        return;
    //    }
    //    this._customCameraParameterInfo.Add(info);
    //    await this.UpdateCameraParameterInfos();
    //}, x => this.IsOpen && !this.IsAcquiring);

}

public interface IParameterInfoPresenter
{
    IParameterInfo ParameterInfo { get; }
}

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
            var message = ValidateParameterValue(this.ParameterInfo, value);
            if (message != null)
            {
                this.Message = message;
                IocMessage.Snack.ShowError(message);
                return;
            }
            var t = this._SetParameter.SetParameter(this.ParameterInfo.Name, value);
            if (!t.successed)
            {
                this.Message = t.message;
                IocMessage.Snack.ShowError(t.message);
                return;
            }
            IocMessage.Snack.ShowSuccess(t.message ?? $"设置{this.ParameterInfo.DisplayName}成功");
            this.ParameterInfo.CurrentValue = value;
            RaisePropertyChanged();
        }
    }


    private static string ValidateParameterValue(IParameterInfo parameterInfo, object value)
    {
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

                return ValidateRange(displayName, integerValue, parameterInfo.Minimum, parameterInfo.Maximum);

            case ParameterType.Float:
                if (!TryConvertToDecimal(value, out decimal floatValue))
                    return $"{displayName} 必须是数字";

                return ValidateRange(displayName, floatValue, parameterInfo.Minimum, parameterInfo.Maximum);

            case ParameterType.Boolean:
                if (value is bool)
                    return null;

                return bool.TryParse(value.ToString(), out _) ? null : $"{displayName} 必须是布尔值";

            case ParameterType.Enum:
                if (parameterInfo.Options == null || parameterInfo.Options.Count == 0)
                    return null;

                return parameterInfo.Options.Contains(value.ToString()) ? null : $"{displayName} 必须是有效选项";

            case ParameterType.String:
            case ParameterType.Command:
            default:
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

public class ParameterInfoDataTemplateSelector : DataTemplateSelector
{
    public DataTemplate ComboBoxDataTemplate { get; set; }
    public DataTemplate TextDataTemplate { get; set; }
    public DataTemplate CheckBoxDataTemplate { get; set; }
    public DataTemplate CommandDataTemplate { get; set; }
    public override DataTemplate SelectTemplate(object item, DependencyObject container)
    {
        if (item is ParameterInfoPresenter parameterInfoPresenter && parameterInfoPresenter.ParameterInfo is IParameterInfo parameterInfo)
        {
            if (parameterInfo.Options != null && parameterInfo.Options.Count > 0)
                return this.ComboBoxDataTemplate;
            if (parameterInfo.Type == ParameterType.Boolean)
                return this.CheckBoxDataTemplate;
            if (parameterInfo.Type == ParameterType.Integer || parameterInfo.Type == ParameterType.Float)
                return this.TextDataTemplate;
            if (parameterInfo.Type == ParameterType.String)
                return this.TextDataTemplate;
            if (parameterInfo.Type == ParameterType.Command)
                return this.CommandDataTemplate;
        }
        return this.TextDataTemplate;
    }
}

public interface IParameters : ISetParameter
{
    IReadOnlyList<IParameterInfo> GetParameterInfos();
}

public interface ISetParameter
{
    (bool successed, string message) SetParameter(string name, object value);
    (bool successed, string message) ExecuteCommand(string name);
}

public interface IParameterInfo
{
    string Name { get; }
    string DisplayName { get; }
    ParameterType Type { get; }
    ParameterAccess Access { get; }
    object Minimum { get; }
    object Maximum { get; }
    object Increment { get; }
    object DefaultValue { get; }
    object CurrentValue { get; set; }
    IReadOnlyList<string> Options { get; }
    string Unit { get; }
    string Category { get; }
    string Description { get; }
}

/// <summary>
/// 参数描述。
/// </summary>
public class ParameterInfo : BindableBase, IParameterInfo
{
    private string _Name;
    public string Name
    {
        get { return _Name; }
        set
        {
            _Name = value;
            RaisePropertyChanged();
        }
    }

    private string _DisplayName;

    public string DisplayName
    {
        get { return _DisplayName; }
        set
        {
            _DisplayName = value;
            RaisePropertyChanged();
        }
    }


    private ParameterType _Type;

    public ParameterType Type
    {
        get { return _Type; }
        set
        {
            _Type = value;
            RaisePropertyChanged();
        }
    }


    private ParameterAccess _Access;

    public ParameterAccess Access
    {
        get { return _Access; }
        set
        {
            _Access = value;
            RaisePropertyChanged();
        }
    }


    private object _Minimum;
    public object Minimum
    {
        get { return _Minimum; }
        set
        {
            _Minimum = value;
            RaisePropertyChanged();
        }
    }


    private object _Maximum;

    public object Maximum
    {
        get { return _Maximum; }
        set
        {
            _Maximum = value;
            RaisePropertyChanged();
        }
    }


    private object _Increment;

    public object Increment
    {
        get { return _Increment; }
        set
        {
            _Increment = value;
            RaisePropertyChanged();
        }
    }


    private object _DefaultValue;

    public object DefaultValue
    {
        get { return _DefaultValue; }
        set
        {
            _DefaultValue = value;
            RaisePropertyChanged();
        }
    }


    private object _CurrentValue;

    public object CurrentValue
    {
        get { return _CurrentValue; }
        set
        {
            _CurrentValue = value;
            RaisePropertyChanged();
        }
    }


    private IReadOnlyList<string> _Options;

    public IReadOnlyList<string> Options
    {
        get { return _Options; }
        set
        {
            _Options = value;
            RaisePropertyChanged();
        }
    }


    private string _Unit;

    public string Unit
    {
        get { return _Unit; }
        set
        {
            _Unit = value;
            RaisePropertyChanged();
        }
    }


    private string _Category;

    public string Category
    {
        get { return _Category; }
        set
        {
            _Category = value;
            RaisePropertyChanged();
        }
    }


    private string _Description;
    public string Description
    {
        get { return _Description; }
        set
        {
            _Description = value;
            RaisePropertyChanged();
        }
    }
}

/// <summary>
/// 参数类型。
/// </summary>
public enum ParameterType
{
    Boolean,
    Integer,
    Float,
    String,
    Enum,
    Command
}

/// <summary>
/// 参数访问权限。
/// </summary>
[Flags]
public enum ParameterAccess
{
    None = 0,
    Read = 1,
    Write = 2,
    ReadWrite = Read | Write
}


