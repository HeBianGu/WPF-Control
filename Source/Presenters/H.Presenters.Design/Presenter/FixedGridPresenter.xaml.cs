using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using H.Presenters.Design;

namespace H.Presenters.Design
{
    [Display(Name = "FixedGrid")]
    public class FixedGridPresenter : GridPresenterBase
    {
        private int _rows;
        [Display(Name = "行数", GroupName = "常用,样式")]
        public int Rows
        {
            get { return _rows; }
            set
            {
                _rows = value;
                RaisePropertyChanged();
            }
        }


        private int _columns;
        [Display(Name = "列数", GroupName = "常用,样式")]
        public int Columns
        {
            get { return _columns; }
            set
            {
                _columns = value;
                RaisePropertyChanged();
            }
        }


        private GridLength _rowGridLength = GridLength.Auto;
        [Display(Name = "行高", GroupName = "常用,样式")]
        public GridLength RowGridLength
        {
            get { return _rowGridLength; }
            set
            {
                _rowGridLength = value;
                RaisePropertyChanged();
            }
        }


        private GridLength _columnGridLength = GridLength.Auto;
        [Display(Name = "列宽", GroupName = "常用,样式")]
        public GridLength ColumnGridLength
        {
            get { return _columnGridLength; }
            set
            {
                _columnGridLength = value;
                RaisePropertyChanged();
            }
        }

    }
}
