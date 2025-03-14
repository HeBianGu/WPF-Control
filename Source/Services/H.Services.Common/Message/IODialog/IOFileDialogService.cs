using Microsoft.Win32;
namespace H.Services.Common;

public class IOFileDialogService : IIOFileDialogService
{
    public string ShowOpenFile(string filter = IIOFileDialogService.defaultFilter, string title = IIOFileDialogService.defaultTitle, string initialDirectory = null, bool restoreDirectory = true)
    {
        OpenFileDialog openFileDialog = this.GetOpenFileDialog(filter, title, initialDirectory, restoreDirectory, false);
        if (openFileDialog.ShowDialog() != true)
            return null;
        return openFileDialog.FileName;
    }

    public string[] ShowOpenFiles(string filter = IIOFileDialogService.defaultFilter, string title = IIOFileDialogService.defaultTitle, string initialDirectory = null, bool restoreDirectory = true)
    {
        OpenFileDialog openFileDialog = this.GetOpenFileDialog(filter, title, initialDirectory, restoreDirectory, true);
        if (openFileDialog.ShowDialog() != true)
            return null;
        return openFileDialog.FileNames;
    }

    public OpenFileDialog GetOpenFileDialog(string filter, string title, string initialDirectory = null, bool restoreDirectory = true, bool multiselect = false)
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();
        openFileDialog.InitialDirectory = initialDirectory; //设置初始路径
        openFileDialog.Filter = filter; //设置“另存为文件类型”或“文件类型”框中出现的选择内容
        openFileDialog.FilterIndex = 1; //设置默认显示文件类型为Csv文件(*.csv)|*.csv
        openFileDialog.Title = title; //获取或设置文件对话框标题
        openFileDialog.RestoreDirectory = restoreDirectory; //设置对话框是否记忆上次打开的目录
        openFileDialog.Multiselect = multiselect;//设置多选
        return openFileDialog;
    }
}

