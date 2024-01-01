using H.Providers.Mvvm;

namespace H.App.FileManager
{
    public abstract class FileView<T> : SelectViewModel<T>, IFileView where T : fm_dd_file
    {
        protected FileView(T t) : base(t)
        {
        }

        public string Description { get; set; }
    }
}
