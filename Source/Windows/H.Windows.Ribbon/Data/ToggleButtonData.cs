﻿// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Windows.Ribbon
{
    public class ToggleButtonData : ControlData
    {
        public bool IsChecked
        {
            get
            {
                return _isChecked;
            }
            set
            {
                if (_isChecked != value)
                {
                    _isChecked = value;
                    RaisePropertyChanged();
                }
            }
        }
        private bool _isChecked;
    }
}
