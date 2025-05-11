// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;

namespace H.Extensions.TypeConverter
{
    public class StringObservableCollectionConverter : System.ComponentModel.TypeConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (value is StringObservableCollection obs)
            {
                return string.Join(",", obs);
            }

            return null;
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string str)
            {
                StringObservableCollection sss = new StringObservableCollection(str?.Split(','));

                return sss;
            }

            return null;
        }
    }

    [TypeConverter(typeof(StringObservableCollectionConverter))]
    public class StringObservableCollection : ObservableCollection<string>
    {
        public StringObservableCollection(IEnumerable<string> collection) : base(collection)
        {

        }

        public StringObservableCollection()
        {

        }
    }
}
