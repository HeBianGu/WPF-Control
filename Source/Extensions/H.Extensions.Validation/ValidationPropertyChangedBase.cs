// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Extensions.Mvvm.ViewModels.Base;
using H.Mvvm.ViewModels.Base;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace H.Extensions.Validation;

public abstract class ValidationPropertyChangedBase : BindableBase, IDataErrorInfo
{
    [Browsable(false)]
    public string Error { get; private set; }

    [Browsable(false)]
    public string this[string columnName]
    {
        get
        {
            List<string> results = new List<string>();
            System.Reflection.PropertyInfo property = this.GetType().GetProperty(columnName);
            IEnumerable<ValidationAttribute> attrs = property.GetCustomAttributes(true)?.OfType<ValidationAttribute>();
            if (attrs == null || attrs.Count() == 0)
                return string.Empty;
            DisplayAttribute display = property.GetCustomAttributes(true)?.OfType<DisplayAttribute>()?.FirstOrDefault();
            object value = property.GetValue(this);
            foreach (ValidationAttribute r in attrs)
            {
                if (!r.IsValid(value))
                {
                    results.Add(r.ErrorMessage ?? r.FormatErrorMessage(display == null ? columnName : display.Name));
                }
            }
            return this.Error = results.FirstOrDefault();
        }
    }

    public List<string> GetErrorMessage()
    {
        List<string> results = new List<string>();

        System.Reflection.PropertyInfo[] propertys = this.GetType().GetProperties();

        foreach (System.Reflection.PropertyInfo item in propertys)
        {
            IEnumerable<ValidationAttribute> collection = item.GetCustomAttributes(false)?.OfType<ValidationAttribute>();

            //  Do：检验数据有效性
            if (collection == null || collection.Count() == 0)
                continue;

            object value = item.GetValue(this);

            foreach (ValidationAttribute r in collection)
            {
                if (!r.IsValid(value))
                {
                    results.Add(r.ErrorMessage ?? r.FormatErrorMessage(item.Name));
                }
            }
        }

        return results;
    }

    public bool IsValid()
    {
        List<string> message = this.GetErrorMessage();
        this.Error = message.FirstOrDefault();
        return message.Count == 0;
    }
}
