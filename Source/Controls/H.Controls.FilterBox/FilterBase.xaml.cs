﻿using H.Services.Common;
using H.Mvvm.ViewModels.Base;

namespace H.Controls.FilterBox
{
    public abstract class FilterBase : DisplayBindableBase, IFilterable
    {
        public abstract bool IsMatch(object obj);

        public override string ToString()
        {
            return this.Name ?? GetType().Name;
        }
    }
}
