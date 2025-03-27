// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using H.Mvvm.ViewModels.Base;

namespace H.Extensions.Validation;

/// <summary> 验证Model属性的基类 </summary>
public class ValidationPropertyBase : Bindable
{
    #region - 属性 -


    private string _name;
    /// <summary> 实体字段名称  </summary>
    public string Name
    {
        get { return _name; }
        set
        {
            _name = value;
            RaisePropertyChanged("Name");
        }
    }


    private string _displayName;
    /// <summary> 说明  </summary>
    public string DisplayName
    {
        get { return _displayName; }
        set
        {
            _displayName = value;
            RaisePropertyChanged("DisplayName");
        }
    }

    private string _flag;
    /// <summary> 必须字段标识  </summary>
    public string Flag
    {
        get { return _flag; }
        set
        {
            _flag = value;
            RaisePropertyChanged("Flag");
        }
    }


    private string _message;
    /// <summary> 验证消息  </summary>
    public string Message
    {
        get { return _message; }
        set
        {
            _message = value;
            RaisePropertyChanged("Message");
        }
    }

    #endregion

}
