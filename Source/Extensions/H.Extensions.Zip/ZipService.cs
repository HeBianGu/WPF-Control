using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Extensions.Zip;

public interface IZipSaveFileDialogService
{
    void ZipFilesSaveFileDialog(string defaultZipFileName, Func<string, IEnumerable<string>> fileFunc);
    void ZipFolderSaveFileDialog(string folder);
}

public class ZipService : IZipSaveFileDialogService
{
    public bool UseAutoOpenFile { get; set; } = true;

    public void ZipFiles(string zipFile, IEnumerable<string> files, bool autoDelete = false)
    {
        this.ZipFiles(zipFile, (zipFile) => files, autoDelete);
    }

    public void ZipFiles(string zipFile, Func<string, IEnumerable<string>> fileFunc, bool autoDelete = false)
    {
        var files = fileFunc.Invoke(zipFile);
        using (var zipArchive = ZipFile.Open(zipFile, ZipArchiveMode.Create))
        {
            foreach (string file in files)
            {
                zipArchive.CreateEntryFromFile(file, Path.GetFileName(file));
            }

        }
        if (!autoDelete)
            return;

        foreach (var file in files)
        {
            File.Delete(file);
        }

        if (this.UseAutoOpenFile)
            this.ShowFile(zipFile);
    }

    public void ZipFilesSaveFileDialog(string defaultZipFileName, Func<string, IEnumerable<string>> fileFunc)
    {
        var dlg = new Microsoft.Win32.SaveFileDialog()
        {
            DefaultExt = "*.zip",
            Filter = "All Files|*.* ",
            FileName = $"{defaultZipFileName}.zip"
        };
        if (dlg.ShowDialog() != true)
            return;
        this.ZipFiles(dlg.FileName, fileFunc.Invoke(dlg.FileName));
    }

    public void ZipFolder(string folder, string zipFilePath)
    {
        var files = this.GetAllFiles(folder);
        using (var zipArchive = ZipFile.Open(zipFilePath, ZipArchiveMode.Create))
        {
            foreach (string file in files)
            {
                var entryName = Path.GetRelativePath(folder, file);
                zipArchive.CreateEntryFromFile(file, entryName);
            }
        }
    }

    public void ZipFolder(string folder)
    {
        var folderName = Path.GetFileName(folder.TrimEnd(Path.DirectorySeparatorChar));
        var zipFilePath = Path.Combine(folder, $"{folderName}.zip");
        this.ZipFolder(folder, zipFilePath);
    }

    public void ZipFolderSaveFileDialog(string folder)
    {
        var files = this.GetAllFiles(folder);
        var defaultZipFileName = Path.GetFileName(folder.TrimEnd(Path.DirectorySeparatorChar));

        var dlg = new Microsoft.Win32.SaveFileDialog()
        {
            DefaultExt = "*.zip",
            Filter = "All Files|*.* ",
            FileName = $"{defaultZipFileName}.zip"
        };
        if (dlg.ShowDialog() != true)
            return;
        ZipFolder(folder, dlg.FileName);
    }

    private void ShowFile(string file)
    {
        try
        {
            if (string.IsNullOrEmpty(file) || !File.Exists(file))
                return;
            string args = string.Format("/e, /select, \"{0}\"", file);
            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = "explorer.exe";
            info.Arguments = args;
            Process.Start(info);
        }
        catch (System.Exception ex)
        {
            // 记录异常或显示错误信息
            Debug.WriteLine($"Failed to open file in explorer: {ex.Message}");
        }
    }


    public void ExtractZipToDirectory(string sourceZipFile, string destinationDirectory, bool overwriteFiles)
    {
        ZipFile.ExtractToDirectory(sourceZipFile, destinationDirectory, overwriteFiles);
    }

    public void AddFilesToZip(string zipFilePath, string[] files)
    {
        using (var zipArchive = ZipFile.Open(zipFilePath, ZipArchiveMode.Update))
        {
            foreach (var file in files)
            {
                zipArchive.CreateEntryFromFile(file, Path.GetFileName(file));
            }
        }
    }

    private IEnumerable<string> GetAllFiles(string folder)
    {
        return Directory.GetFiles(folder, "*", SearchOption.AllDirectories);
    }


}
