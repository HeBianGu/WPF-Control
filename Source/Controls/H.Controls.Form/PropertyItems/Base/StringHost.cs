// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using H.Mvvm.ViewModels.Base;

namespace H.Controls.Form.PropertyItems.Base;

public class StringHost : Bindable, IDataErrorInfo
{
    public StringHost(string value, Type type)
    {
        this.Value = value;
        this.Type = type;
    }

    private Type _type;
    /// <summary> 数据类型  </summary>
    public Type Type
    {
        get { return _type; }
        set
        {
            _type = value;
            RaisePropertyChanged("Type");
        }
    }

    private string _value;
    /// <summary> 说明  </summary>
    public string Value
    {
        get { return _value; }
        set
        {
            _value = value;
            RaisePropertyChanged("Value");
        }
    }

    private string _message;
    /// <summary> 说明  </summary>
    public string Message
    {
        get { return _message; }
        set
        {
            _message = value;
            RaisePropertyChanged("Message");
        }
    }

    public string Error { get; set; }

    // 验证
    public string this[string columnName]
    {
        get
        {
            //  Do ：检查参数类型
            try
            {
                this.Message = null;
                Convert.ChangeType(this.Value, this.Type);
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
            }

            return this.Error = this.Message;
        }
    }
}
