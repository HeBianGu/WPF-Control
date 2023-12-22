
using H.DataBases.Share;
using H.Extensions.ViewModel;
using H.Providers.Mvvm;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace H.App.FileManager
{
    /// <summary> Ma</summary>
    internal class MainViewModel : NotifyPropertyChanged
    {
        private object _content;
        /// <summary> 说明  </summary>
        public object Content
        {
            get { return _content; }
            set
            {
                _content = value;
                RaisePropertyChanged();
            }
        }

        //public IRepositoryViewModel<fm_dd_file> File => DbIoc.Services.GetService<IRepositoryViewModel<fm_dd_file>>();     
    }
}
