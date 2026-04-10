// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.IO;

namespace H.Controls.Form.PropertyItem.Attribute
{
    public class GetFilesSourceAttribute : GetSourceAttribute
    {
        public GetFilesSourceAttribute(string folderPath)
        {
            this.FolderPath = folderPath;
        }

        public string FolderPath { get; set; }
        public SearchOption SearchOption { get; set; } = SearchOption.TopDirectoryOnly;
        public string SearchPattern { get; set; } = SearchPatterns.AllFiles;

        public bool UseFullPath { get; set; } = true;
        public override IEnumerable GetSource(PropertyInfo propertyInfo, object obj)
        {
            if (this.FolderPath == null)
                return null;
            var results = this.FolderPath.GetFiles(this.SearchPattern, this.SearchOption);
            if (this.UseFullPath)
                return results.Select(x => x.GetFullPath());
            return results;
        }
    }
}
