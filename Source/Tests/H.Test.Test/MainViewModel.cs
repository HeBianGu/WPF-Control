using H.Data.Test;
using H.Providers.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Test.Test
{

    /// <summary> 说明</summary>
    public class MainViewModel : NotifyPropertyChanged
    {
        #region - 属性 -

        private Students _students = Student.Randoms(100);
        /// <summary> 说明  </summary>
        public Students Students
        {
            get { return _students; }
            set
            {
                _students = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region - 命令 -

        #endregion

    }

}
