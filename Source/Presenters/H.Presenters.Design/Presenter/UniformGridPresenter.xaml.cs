﻿using H.Presenters.Design;
using H.Providers.Mvvm;
using System.ComponentModel.DataAnnotations;

namespace H.Presenters.Design
{
    [Displayer(Name = "Uniform", Icon = "\xe734")]
    public class UniformGridPresenter : PanelPresenterBase
    {
        private int _columns;
        [Display(Name = "列数", GroupName = "常用,布局")]
        public int Columns
        {
            get { return _columns; }
            set
            {
                _columns = value;
                RaisePropertyChanged();
            }
        }

        private int _rows;
        [Display(Name = "行数", GroupName = "常用,布局")]
        public int Rows
        {
            get { return _rows; }
            set
            {
                _rows = value;
                RaisePropertyChanged();
            }
        }
    }
}