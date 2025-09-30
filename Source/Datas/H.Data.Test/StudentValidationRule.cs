// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Globalization;
using System.Windows.Controls;

namespace H.Data.Test
{
    public class StudentValidationRule : ValidationRule
    {
        public override System.Windows.Controls.ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value is Student student)
                return student.Age > 30 ? System.Windows.Controls.ValidationResult.ValidResult : new System.Windows.Controls.ValidationResult(false, "Age不应该大于50");
            return System.Windows.Controls.ValidationResult.ValidResult;
        }
    }
}
