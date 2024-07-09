using H.Mvvm;

namespace H.App.FileManager
{
    public abstract class FileView<T> : SelectBindable<T>, IFileView where T : fm_dd_file
    {
        protected FileView(T t) : base(t)
        {
        }

        public string Description { get; set; }
    }
}
