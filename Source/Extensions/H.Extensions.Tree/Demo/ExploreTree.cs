﻿using System;
using System.Collections;
using System.IO;
using System.Linq;

namespace H.Extensions.Tree
{
    public class ExploreTree : ITree, IParent
    {
        public string SearchPattern { get; set; } = "*";
        public SearchOption SearchOption { get; set; }
        public string Root { get; set; }
        public IEnumerable GetChildren(object parent)
        {
            if (parent == null)
            {
                if (!Directory.Exists(Root))
                    return DriveInfo.GetDrives();
                return new DirectoryInfo[] { new DirectoryInfo(Root) };
            }

            if (parent is DriveInfo drive)
                return drive.RootDirectory.GetFileSystemInfos(SearchPattern, SearchOption).Where(d => !d.Attributes.HasFlag(FileAttributes.Hidden | FileAttributes.System));

            if (parent is DirectoryInfo directory)
            {
                try
                {
                    return directory.GetFileSystemInfos(SearchPattern, SearchOption).Where(d => !d.Attributes.HasFlag(FileAttributes.Hidden | FileAttributes.System));
                }
                catch (Exception ex)
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
}
