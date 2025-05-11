// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Mvvm.ViewModels.Base;
using System.ComponentModel;

namespace H.Extensions.Unit
{
    public class UnitTest : BindableBase
    {
        private long _byte = 12535456;
        [TypeConverter(typeof(ByteSizeUnitableTypeConverter))]
        public long Byte
        {
            get { return _byte; }
            set
            {
                _byte = value;
                RaisePropertyChanged();
            }
        }

        private long _millimeter = 12535456;
        [TypeConverter(typeof(MillimeterUnitableTypeConverter))]
        public long Millimeter
        {
            get { return _millimeter; }
            set
            {
                _millimeter = value;
                RaisePropertyChanged();
            }
        }

        private int _weight = 12535456;
        public int Weight
        {
            get { return _weight; }
            set
            {
                _weight = value;
                RaisePropertyChanged();
            }
        }

        private long _time = 12535456;
        public long Time
        {
            get { return _time; }
            set
            {
                _time = value;
                RaisePropertyChanged();
            }
        }

        private double _lengh = 12535.456;
        public double Lengh
        {
            get { return _lengh; }
            set
            {
                _lengh = value;
                RaisePropertyChanged();
            }
        }
    }
}

