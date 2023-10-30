﻿// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;

namespace H.Controls.PropertyGrid
{
    internal struct FilterInfo
    {
        public string InputString;
        public Predicate<object> Predicate;
    }
}
