using H.Data.Test;
using H.Services.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using H.Extensions.TypeLicense.LicenseProviders;
using H.Mvvm.ViewModels.Base;
using H.Common.Interfaces.Where;

namespace H.Test.Test
{

    [LicenseProvider(typeof(DefaultTypeFileLicenseProvider))]
    public class MainViewModel : Bindable
    {
        public MainViewModel()
        {

            {
                bool r = LicenseManager.IsValid(this.GetType());
                System.Diagnostics.Debug.WriteLine("MainViewModel License:" + (r ? "true" : "false"));
            }

            {
                bool r = LicenseManager.IsValid(this.GetType(), this, out License license);
                license?.Dispose();
                System.Diagnostics.Debug.WriteLine("MainViewModel License:" + (r ? "true" : "false"));
            }
        }
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


        private IFilterable _filter;
        /// <summary> 说明  </summary>
        public IFilterable Filter
        {
            get { return _filter; }
            set
            {
                _filter = value;
                RaisePropertyChanged();
            }
        }


        private Student _selectedStudent;
        /// <summary> 说明  </summary>
        public Student SelectedStudent
        {
            get { return _selectedStudent; }
            set
            {
                _selectedStudent = value;
                RaisePropertyChanged();
            }
        }


        private string _NotifyClass;
        /// <summary> 说明  </summary>
        public string NotifyClass
        {
            get { return _NotifyClass; }
            set
            {
                _NotifyClass = value;
                RaisePropertyChanged();
            }
        }

        private TestBindables _testViewModels = TestBindable.Randoms(100);
        /// <summary> 说明  </summary>
        public TestBindables TestViewModels
        {
            get { return _testViewModels; }
            set
            {
                _testViewModels = value;
                RaisePropertyChanged();
            }
        }

        private TestBindable _selectedTestViewModel;
        /// <summary> 说明  </summary>
        public TestBindable SelectedTestViewModel
        {
            get { return _selectedTestViewModel; }
            set
            {
                _selectedTestViewModel = value;
                RaisePropertyChanged();
            }
        }
    }
}
