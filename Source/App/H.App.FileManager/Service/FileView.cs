using H.Mvvm;
using System;

namespace H.App.FileManager
{
    public abstract class FileView<T> : SelectBindable<T>, IFileView where T : fm_dd_file
    {
        protected FileView(T t) : base(t)
        {
        }

        public string Description { get; set; }

        //public RelayCommand UpdateTimeCommand => new RelayCommand(e=>
        //{
        //    this.Model.LastPlayTime = DateTime.Now;
        //    this.Model.PlayCount= this.Model.PlayCount+1;
        //});
    }
}
