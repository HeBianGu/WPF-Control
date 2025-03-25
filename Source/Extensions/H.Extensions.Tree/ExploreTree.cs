using System.Collections;
using System.IO;

namespace H.Extensions.Tree;

public class ExploreTree : ITree, IParent
{
    public string SearchPattern { get; set; } = "*";
    public SearchOption SearchOption { get; set; }
    public string Root { get; set; }
    public IEnumerable GetChildren(object parent)
    {
        if (parent == null)
        {
            if (!Directory.Exists(this.Root))
                return DriveInfo.GetDrives();
            return new DirectoryInfo[] { new DirectoryInfo(this.Root) };
        }

        if (parent is DriveInfo drive)
            return drive.RootDirectory.GetFileSystemInfos(this.SearchPattern, this.SearchOption).Where(d => !d.Attributes.HasFlag(FileAttributes.Hidden | FileAttributes.System));

        if (parent is DirectoryInfo directory)
        {
            try
            {
                return directory.GetFileSystemInfos(this.SearchPattern, this.SearchOption).Where(d => !d.Attributes.HasFlag(FileAttributes.Hidden | FileAttributes.System));
            }
            catch (Exception)
            {


            }
        }
        return Enumerable.Empty<string>();
    }

    public object GetParent(object current)
    {
        if (current is FileSystemInfo info)
            Path.GetDirectoryName(info.FullName);
        return null;
    }
}
