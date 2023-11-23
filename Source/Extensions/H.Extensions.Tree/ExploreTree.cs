using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Security.AccessControl;

namespace H.Extensions.Tree
{
    public class ExploreTree : ITree
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
                return drive.RootDirectory.GetFileSystemInfos(this.SearchPattern,SearchOption);

            if (parent is DirectoryInfo directory)
            {
                //if (!directory.Attributes.HasFlag(FileAttributes.System))

                try
                {
                    return directory.GetFileSystemInfos();
                }
                catch (Exception ex)
                {


                }
            }

            return Enumerable.Empty<string>();
        }
    }
}
