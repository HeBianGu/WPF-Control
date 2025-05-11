// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

//using HeBianGu.Common.Expression;
//using System;
//using System.ComponentModel;
//using System.Globalization;

//namespace H.Extensions.TypeConverter
//{
//    public class DelegateConverter<TDelegate> : System.ComponentModel.TypeConverter where TDelegate : Delegate
//    {
//        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
//        {
//            if (destinationType == typeof(string))
//            {
//                if (value == null) return null;

//                return ExpressionService.ParseDelegate<TDelegate>(value.ToString());
//            }
//            return base.ConvertTo(context, culture, value, destinationType);
//        }

//        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
//        {
//            return ExpressionService.ParseDelegate<TDelegate>(value.ToString());
//        }
//    }
//}
