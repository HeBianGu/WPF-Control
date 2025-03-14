using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Services.Common.Message.FileDialog;

public interface IIODialog
{
    const string defaultFilter = "所有文件(*.*)|*.*";
    const string defaultTitle = "打开文件";
    const string defaultImageFilter = "图片文件(*.jpg;*.png;*.bmp)|*.jpg;*.png;*.bmp";
    const string defaultTextFilter = "文本文件(*.txt;*.csv)|*.txt;*.csv";
    const string defaultExcelFilter = "Excel文件(*.xlsx;*.xls)|*.xlsx;*.xls";
    const string defaultWordFilter = "Word文件(*.doc;*.docx)|*.doc;*.docx";
    const string defaultPdfFilter = "PDF文件(*.pdf)|*.pdf";
    const string defaultZipFilter = "压缩文件(*.zip;*.rar)|*.zip;*.rar";
    const string defaultVideoFilter = "视频文件(*.mp4;*.avi)|*.mp4;*.avi";
    const string defaultAudioFilter = "音频文件(*.mp3;*.wav)|*.mp3;*.wav";
    const string defaultXmlFilter = "XML文件(*.xml)|*.xml";
    const string defaultJsonFilter = "JSON文件(*.json)|*.json";
    string ShowOpenFile(string filter = defaultFilter, string title = defaultTitle, string initialDirectory = null, bool restoreDirectory = true);
    string[] ShowOpenFiles(string filter = defaultFilter, string title = defaultTitle, string initialDirectory = null, bool restoreDirectory = true);
}

public class IODialog : IIODialog
{
    public string ShowOpenFile(string filter = IIODialog.defaultFilter, string title = IIODialog.defaultTitle, string initialDirectory = null, bool restoreDirectory = true)
    {
        OpenFileDialog openFileDialog = this.GetOpenFileDialog(filter, title, initialDirectory, restoreDirectory, false);
        return openFileDialog.FileName;
    }

    public string[] ShowOpenFiles(string filter = IIODialog.defaultFilter, string title = IIODialog.defaultTitle, string initialDirectory = null, bool restoreDirectory = true)
    {
        OpenFileDialog openFileDialog = this.GetOpenFileDialog(filter, title, initialDirectory, restoreDirectory, true);
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

public static class IODialogExtension
{
    public static string ShowOpenFile(this IIODialog dialog, params string[] filterExtensions)
    {
        var filter = filterExtensions.Select(x => $" *.{x};").Aggregate((x, y) => x + y);
        return dialog.ShowOpenFile(filter);
    }

    public static string ShowOpenImageFile(this IIODialog dialog, string title = IIODialog.defaultTitle, string initialDirectory = null)
    {
        return dialog.ShowOpenFile(IIODialog.defaultImageFilter, title, initialDirectory);
    }
    public static string ShowOpenVedioFile(this IIODialog dialog, string title = IIODialog.defaultTitle, string initialDirectory = null)
    {
        return dialog.ShowOpenFile(IIODialog.defaultVideoFilter, title, initialDirectory);
    }
    public static string ShowOpenAudioFile(this IIODialog dialog, string title = IIODialog.defaultTitle, string initialDirectory = null)
    {
        return dialog.ShowOpenFile(IIODialog.defaultAudioFilter, title, initialDirectory);
    }

    public static string ShowOpenExcelFile(this IIODialog dialog, string title = IIODialog.defaultTitle, string initialDirectory = null)
    {
        return dialog.ShowOpenFile(IIODialog.defaultExcelFilter, title, initialDirectory);
    }

    public static string ShowOpenPDFFile(this IIODialog dialog, string title = IIODialog.defaultTitle, string initialDirectory = null)
    {
        return dialog.ShowOpenFile(IIODialog.defaultPdfFilter, title, initialDirectory);
    }

    public static string ShowOpenZipFile(this IIODialog dialog, string title = IIODialog.defaultTitle, string initialDirectory = null)
    {
        return dialog.ShowOpenFile(IIODialog.defaultZipFilter, title, initialDirectory);
    }

    public static string ShowOpenJsonFile(this IIODialog dialog, string title = IIODialog.defaultTitle, string initialDirectory = null)
    {
        return dialog.ShowOpenFile(IIODialog.defaultJsonFilter, title, initialDirectory);
    }
}

