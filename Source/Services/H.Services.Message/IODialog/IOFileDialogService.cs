using Microsoft.Win32;
namespace H.Services.Message.IODialog;

public class IOFileDialogService : IIOFileDialogService
{
    public string ShowOpenFile(Action<IIOFileDialogOption> optionAction)
    {
        OpenFileDialog openFileDialog = this.GetOpenFileDialog(optionAction, false);
        return openFileDialog.ShowDialog() != true ? null : openFileDialog.FileName;
    }

    public string[] ShowOpenFiles(Action<IIOFileDialogOption> optionAction)
    {
        OpenFileDialog openFileDialog = this.GetOpenFileDialog(optionAction, true);
        return openFileDialog.ShowDialog() != true ? null : openFileDialog.FileNames;
    }

    public OpenFileDialog GetOpenFileDialog(Action<IIOFileDialogOption> optionAction, bool multiselect)
    {
        IOFileDialogOption option = new IOFileDialogOption();
        optionAction?.Invoke(option);
        OpenFileDialog openFileDialog = new OpenFileDialog();
        openFileDialog.InitialDirectory = option.InitialDirectory; //设置初始路径
        openFileDialog.Filter = option.Filter; //设置“另存为文件类型”或“文件类型”框中出现的选择内容
        openFileDialog.FilterIndex = 1; //设置默认显示文件类型为Csv文件(*.csv)|*.csv
        openFileDialog.Title = option.Title; //获取或设置文件对话框标题
        openFileDialog.RestoreDirectory = option.RestoreDirectory; //设置对话框是否记忆上次打开的目录
        openFileDialog.Multiselect = multiselect;//设置多选
        return openFileDialog;
    }

    public string ShowSaveFile(Action<IIOSaveFileDialogOption> optionAction)
    {
        IOSaveFileDialogOption option = new IOSaveFileDialogOption();
        optionAction?.Invoke(option);
        SaveFileDialog saveFileDialog = new SaveFileDialog();
        saveFileDialog.Filter = option.Filter;
        saveFileDialog.FileName = option.DefaultFileName;
        saveFileDialog.DefaultExt = option.DefaultExt;
        saveFileDialog.AddExtension = true;
        saveFileDialog.RestoreDirectory = option.RestoreDirectory;
        saveFileDialog.Title = option.Title;
        return saveFileDialog.ShowDialog() != true ? null : saveFileDialog.FileName;
    }

}

