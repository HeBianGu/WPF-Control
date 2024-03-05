//// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

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
