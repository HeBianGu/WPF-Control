// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using H.Mvvm.ViewModels;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace H.Extensions.Validation;


public class ValidationSelectedViewModel<T> : SelectBindable<T>, IDataErrorInfo
{
    public ValidationSelectedViewModel(T t) : base(t)
    {

    }

    public string Error { get; private set; }

    // 验证
    public string this[string columnName]
    {
        get
        {
            if (this.Validation(columnName, out string error))
            {
                return this.Error = null;
            }
            return this.Error = error;
        }
    }

    protected virtual bool Validation(string columnName, out string error)
    {
        error = null;
        List<string> results = new List<string>();
        System.Reflection.PropertyInfo property = this.GetType().GetProperty(columnName);
        IEnumerable<ValidationAttribute> attrs = property.GetCustomAttributes(true)?.OfType<ValidationAttribute>();
        if (attrs == null || attrs.Count() == 0)
            return true;
        DisplayAttribute display = property.GetCustomAttributes(true)?.OfType<DisplayAttribute>()?.FirstOrDefault();
        object value = property.GetValue(this);
        foreach (ValidationAttribute r in attrs)
        {
            if (!r.IsValid(value))
            {
                results.Add(r.ErrorMessage ?? r.FormatErrorMessage(display == null ? columnName : display.Name));
            }
        }
        error = results.FirstOrDefault();
        return string.IsNullOrEmpty(error);
    }
}
