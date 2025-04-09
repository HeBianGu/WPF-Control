// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control
using System.IO;
using System.Security.Cryptography;
using System.Windows.Media;

namespace H.Extensions.Common;


public static partial class FileExtension
{
    public const string ImageExtension = "jpg jpeg png gif pdf tga tif svg tga bmp dds eps webp";
    public const string VedioExtension = "wmv asf asx rm rmvb mpg mpeg mpe 3gp mov mp4 m4v avi dat mkv flv vob dat bdmv";
    public const string AudioExtension = "mp3 mpeg3 wav wave flac fla aiff aif aifc aac adt adts m2ts mp2 3g2 3gp2 3gp 3gpp m4a m4v mp4v mp4 mov asf wm wmv wma mp1 avi ac3 ec3";

    public static string[] ImageExtensions => ImageExtension.Split(new char[] { ';', ' ' }, StringSplitOptions.RemoveEmptyEntries);
    public static string[] VedioExtensions => VedioExtension.Split(new char[] { ';', ' ' }, StringSplitOptions.RemoveEmptyEntries);
    public static string[] AudioExtensions => AudioExtension.Split(new char[] { ';', ' ' }, StringSplitOptions.RemoveEmptyEntries);

    public static IEnumerable<string> GetAllImages(this string foldPath)
    {
        var files = foldPath.ToDirectoryEx().GetAllFiles(x => x.FullName.IsImage());
        return files;
    }

    public static bool IsImage(this string filePath)
    {
        if (string.IsNullOrEmpty(filePath))
            return false;
        return ImageExtensions.Any(x => x.Equals(Path.GetExtension(filePath).Trim('.'), StringComparison.OrdinalIgnoreCase));
    }

    public static ImageSource ToImageSource(this string filePath)
    {
        if (filePath.IsImage())
            return filePath.ToByTypeConverter<ImageSource>();
        return null;
    }

    public static IEnumerable<string> GetAllVedios(this string foldPath)
    {
        var files = foldPath.ToDirectoryEx().GetAllFiles(x => x.FullName.IsVedio());
        return files;
    }


    public static bool IsVedio(this string filePath)
    {
        if (string.IsNullOrEmpty(filePath))
            return false;
        return VedioExtensions.Any(x => x.Equals(Path.GetExtension(filePath).Trim('.'), StringComparison.OrdinalIgnoreCase));
    }

    public static IEnumerable<string> GetAllAudios(this string foldPath)
    {
        var files = foldPath.ToDirectoryEx().GetAllFiles(x => x.FullName.IsAudio());
        return files;
    }
    public static bool IsAudio(this string filePath)
    {
        if (string.IsNullOrEmpty(filePath))
            return false;
        return AudioExtensions.Any(x => x.Equals(Path.GetExtension(filePath).Trim('.'), StringComparison.OrdinalIgnoreCase));
    }
    public static string ImageExtensionsFilter => $"图像文件({ImageExtension}) |{GetExtension(ImageExtensions)}|所有文件(*.*)|*.*";
    public static string VedioExtensionsFilter => $"视频文件({VedioExtension}) |{GetExtension(VedioExtensions)}|所有文件(*.*)|*.*";
    public static string AudioExtensionsFilter => $"音频文件({AudioExtension}) |{GetExtension(AudioExtensions)}|所有文件(*.*)|*.*";

    public static string GetExtension(string[] extensions)
    {
        return extensions.Select(x => $" *.{x};").Aggregate((x, y) => x + y);
    }
}


public static partial class FileExtension
{
    public static long GetDirectorySize(this DirectoryInfo dirInfo)
    {
        long size = 0;
        FileInfo[] files;
        try
        {
            files = dirInfo.GetFiles();
        }
        catch (UnauthorizedAccessException)
        {
            return 0;
        }
        catch (Exception)
        {
            return 0;
        }

        foreach (FileInfo file in files)
        {
            try
            {
                size += file.Length;
            }
            catch (UnauthorizedAccessException)
            {

            }
            catch (Exception)
            {

            }
        }
        DirectoryInfo[] subDirs;
        try
        {
            subDirs = dirInfo.GetDirectories();
        }
        catch (UnauthorizedAccessException)
        {
            return size;
        }
        catch (Exception)
        {
            return size;
        }

        foreach (DirectoryInfo subDir in subDirs)
        {
            size += GetDirectorySize(subDir);
        }
        return size;
    }


    //public void CopyDirectory()
    //{
    //    try
    //    {
    //        // 复制目录
    //        CopyDirectory(sourceDirectory, targetDirectory);
    //        Console.WriteLine("Directory copied successfully.");
    //    }
    //    catch (Exception ex)
    //    {
    //        Console.WriteLine("Error copying directory: " + ex.Message);
    //    }

    //    void CopyDirectory(string sourceDir, string targetDir)
    //    {
    //        // 获取源目录的信息
    //        DirectoryInfo dir = new DirectoryInfo(sourceDir);

    //        // 如果目标目录不存在，则创建它
    //        if (!Directory.Exists(targetDir))
    //        {
    //            Directory.CreateDirectory(targetDir);
    //        }

    //        // 获取源目录中的文件并复制到目标目录
    //        foreach (FileInfo file in dir.GetFiles())
    //        {
    //            string tempPath = Path.Combine(targetDir, file.Name);
    //            file.CopyTo(tempPath, false); // false表示不覆盖已存在的文件
    //        }

    //        // 递归复制子目录
    //        foreach (DirectoryInfo subDir in dir.GetDirectories())
    //        {
    //            string tempPath = Path.Combine(targetDir, subDir.Name);
    //            CopyDirectory(subDir.FullName, tempPath);
    //        }
    //    }

    //}
}

public static partial class FileExtension
{
    public static DirectoryEx ToDirectoryEx(this string folder) => new DirectoryEx(folder);

    public static FileEx ToFileEx(this string filePath) => new FileEx(filePath);
}

public class DirectoryEx
{
    public DirectoryEx(string folder)
    {
        this.Folder = folder;
    }
    public string Folder { get; }

    public DirectoryEx CreateDirectoryIfNotExsit()
    {
        if (!Directory.Exists(this.Folder))
            Directory.CreateDirectory(this.Folder);
        return this;
    }

    public void BackupToDirectory(string destFolder, Action<string> logAction = null)
    {
        string sourceFolder = this.Folder;
        int totalFiles = 0;
        int totalSubFolders = 0;
        int totalSuccess = 0;
        int totalFailed = 0;
        int totalSkipped = 0;
        if (!Directory.Exists(destFolder))
            Directory.CreateDirectory(destFolder);
        string[] files = null;
        try
        {
            files = Directory.GetFiles(sourceFolder);
        }
        catch (UnauthorizedAccessException ex)
        {
            //IocLog.Instance?.Error(ex);
            logAction?.Invoke($"{sourceFolder}\r\nAccess Denied\r\n");
        }
        catch (Exception e)
        {
            //IocLog.Instance?.Error(e);
            logAction?.Invoke($"{sourceFolder}\r\n{e.Message}\r\n");
        }

        if (files != null && files.Length > 0)
        {
            totalFiles += files.Length;

            foreach (string file in files)
            {
                try
                {
                    string name = Path.GetFileName(file);
                    string dest = Path.Combine(destFolder, name);

                    if (File.Exists(dest))
                    {
                        DateTime srcWriteTime = File.GetLastWriteTime(file);
                        DateTime destWriteTime = File.GetLastWriteTime(dest);

                        if (srcWriteTime > destWriteTime)
                        {
                            File.Copy(file, dest, true);

                            FileInfo fileInfo = new FileInfo(file);
                            double fileSize = (double)fileInfo.Length / 1048576.0;  // Convert to MB
                            logAction?.Invoke($"{fileSize:000.000} MB - (updated) {file}");
                            totalSuccess++;
                        }
                        else
                        {
                            totalSkipped++;
                        }
                    }
                    else
                    {
                        File.Copy(file, dest, true);

                        FileInfo fileInfo = new FileInfo(file);
                        double fileSize = (double)fileInfo.Length / 1048576.0;  // Convert to MB
                        logAction?.Invoke($"{fileSize:000.000} MB - {file}");

                        totalSuccess++;
                    }
                }
                catch (UnauthorizedAccessException)
                {
                    logAction?.Invoke($"{file}\r\nAccess Denied\r\n");
                    totalFailed++;
                }
                catch (Exception e)
                {
                    totalFailed++;
                    logAction?.Invoke($"{file}\r\n{e.Message}\r\n");
                }
            }
        }
        string[] folders = null;

        try
        {
            folders = Directory.GetDirectories(sourceFolder);
        }
        catch (UnauthorizedAccessException)
        {
            logAction?.Invoke($"{sourceFolder}\r\nAccess denied\r\n");

        }
        catch (Exception e)
        {
            logAction?.Invoke($"{sourceFolder}\r\nAccess {e.Message}\r\n");
        }

        if (folders != null && folders.Length > 0)
        {
            totalSubFolders += folders.Length;

            foreach (string folder in folders)
            {
                try
                {
                    string name = Path.GetFileName(folder);
                    string dest = Path.Combine(destFolder, name);
                    folder.ToDirectoryEx().BackupToDirectory(dest, logAction);
                }
                catch (UnauthorizedAccessException)
                {
                    logAction?.Invoke($"{folder}\r\nAccess denied\r\n");
                }
                catch (Exception e)
                {
                    logAction?.Invoke($"{sourceFolder}\r\nAccess {e.Message}\r\n");
                }
            }
        }
    }

    public List<string> GetAllFiles(Predicate<FileInfo> match = null)
    {
        List<string> ss = new List<string>();
        if (!Directory.Exists(this.Folder))
            return ss;
        DirectoryInfo dir = new DirectoryInfo(this.Folder);
        foreach (FileSystemInfo d in dir.GetFileSystemInfos().Where(d => !d.Attributes.HasFlag(FileAttributes.Hidden | FileAttributes.System)))
        {
            if (d is DirectoryInfo)
            {
                DirectoryInfo dd = d as DirectoryInfo;
                ss.AddRange(dd.FullName.ToDirectoryEx().GetAllFiles(match));
            }
            else if (d is FileInfo)
            {
                FileInfo dd = d as FileInfo;
                if (match == null || match(dd))
                    ss.Add(d.FullName);
            }
        }
        return ss;
    }
}

public class FileEx
{
    public FileEx(string fullPath)
    {
        this.FullPath = fullPath;
    }
    public string FullPath { get; }

    public FileEx CreateDirectoryIfNotExsit()
    {
        string folder = Path.GetDirectoryName(this.FullPath);
        folder.ToDirectoryEx().CreateDirectoryIfNotExsit();
        return this;
    }

    public string GetFileHashMD5()
    {
        using (FileStream fs = new FileStream(this.FullPath, FileMode.Open, FileAccess.Read))
        {
            using (MD5 sha256 = MD5.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(fs);
                return BitConverter.ToString(hashBytes).Replace("-", "");
            }
        }
    }

    public string GetFileHashSHA256()
    {
        using (FileStream fs = new FileStream(this.FullPath, FileMode.Open, FileAccess.Read))
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(fs);
                return BitConverter.ToString(hashBytes).Replace("-", "");
            }
        }
    }

    public string GetFileSizeToDisplay()
    {
        double TB = 1024 * 1024 * 1024 * 1024.0;
        int GB = 1024 * 1024 * 1024;
        int MB = 1024 * 1024;
        int KB = 1024;

        if (!File.Exists(this.FullPath))
            return null;
        long KSize = new FileInfo(this.FullPath).Length;
        bool isMinus = KSize < 0;
        string result;
        KSize = Math.Abs(KSize);
        if (KSize / TB >= 1)
            result = Math.Round(KSize / (float)TB, 2).ToString() + "T";
        else if (KSize / GB >= 1)
            result = Math.Round(KSize / (float)GB, 2).ToString() + "G";
        else if (KSize / MB >= 1)

            result = Math.Round(KSize / (float)MB, 2).ToString() + "MB";
        else if (KSize / KB >= 1)

            result = Math.Round(KSize / (float)KB, 2).ToString() + "KB";
        else
            result = KSize.ToString() + "Byte";
        return isMinus ? "-" + result : result;
    }
}


//public static class IODialogExtension
//{
//    public static string ShowOpenImageFileCommon(this IIOFileDialogService dialog, string initialDirectory = null)
//    {
//        return dialog.ShowOpenFile(x =>
//        {
//            x.Filter = FileExtension.ImageExtensionsFilter;
//            x.Title = "打开图片";
//        });
//    }
//    public static string ShowOpenVedioFileCommon(this IIOFileDialogService dialog, string initialDirectory = null)
//    {
//        return dialog.ShowOpenFile(x => x.Filter = FileExtension.VedioExtensionsFilter);
//    }
//    public static string ShowOpenAudioFileCommon(this IIOFileDialogService dialog, string initialDirectory = null)
//    {
//        return dialog.ShowOpenFile(x => x.Filter = FileExtension.AudioExtensionsFilter);
//    }
//}
