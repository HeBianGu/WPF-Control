namespace H.App.FileManager
{
    public interface IFileToViewService
    {
        IFileView ToView(fm_dd_file file, string desc = null);
    }
}
