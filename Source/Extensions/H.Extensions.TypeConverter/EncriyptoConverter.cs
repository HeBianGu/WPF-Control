//// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


//using System;
//using System.ComponentModel;
//using System.Globalization;
//using System.Reflection;
//using System.Windows.Markup;

//namespace H.Extensions.TypeConverter
//{
//    public class DateTimeEncriyptoConverter : System.ComponentModel.TypeConverter
//    {
//        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
//        {
//            if (destinationType == typeof(string))
//            {
//                if (value == null)
//                    return string.Empty;

//                if (value is DateTime time)
//                {
//                    return IocCrypt.Instance?.Encrypt(time.Ticks.ToString());
//                }
//            }
//            return base.ConvertTo(context, culture, value, destinationType);
//        }

//        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
//        {
//            if (value == null)
//                return DateTime.MinValue;
//            if (value is string str)
//            {
//                string v = IocCrypt.Instance?.Decrypt(str);
//                if (long.TryParse(v, out long result) == false)
//                    return DateTime.MinValue;
//                return new DateTime(result);
//            }

//            return base.ConvertFrom(context, culture, value);
//        }
//    }

//    public class DateTimeEncriyptoValueSerializer : ValueSerializer
//    {
//        public override string ConvertToString(object value, IValueSerializerContext context)
//        {
//            if (value is DateTime time)
//            {
//                return IocCrypt.Instance.Encrypt(time.Ticks.ToString());
//            }

//            return null;
//        }

//        public override bool CanConvertFromString(string value, IValueSerializerContext context)
//        {
//            return true;
//        }

//        public override bool CanConvertToString(object value, IValueSerializerContext context)
//        {
//            return value is DateTime time;
//        }

//        public override object ConvertFromString(string value, IValueSerializerContext context)
//        {
//            if (value == null)
//                return DateTime.MinValue;

//            string v = IocCrypt.Instance.Decrypt(value);
//            if (long.TryParse(v, out long result) == false)
//                return DateTime.MinValue;
//            return new DateTime(result);
//        }
//    }


//    public class EncriyptoValueSerializer : ValueSerializer
//    {
//        public override string ConvertToString(object value, IValueSerializerContext context)
//        {
//            return IocCrypt.Instance.Encrypt(value.ToString());
//        }

//        public override object ConvertFromString(string value, IValueSerializerContext context)
//        {
//            return IocCrypt.Instance.Decrypt(value);
//        }
//    }


//}
