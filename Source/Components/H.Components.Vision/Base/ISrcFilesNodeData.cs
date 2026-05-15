// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Components.Vision.Base;

public interface ISrcFilesNodeData : IFlowableNodeData
{
    bool UseAllImage { get; set; }
    bool UseAutoSwitch { get; set; }

    string SrcFilePath { get; set; }
    ObservableCollection<string> SrcFilePaths { get; set; }

    bool IsValid(out string message);
}

public static class SrcFilesNodeDataExtension
{

    public static int MoveNext(this ISrcFilesNodeData srcFilesNodeData)
    {
        int index = srcFilesNodeData.SrcFilePaths.IndexOf(srcFilesNodeData.SrcFilePath);
        index = index < srcFilesNodeData.SrcFilePaths.Count - 1 ? index + 1 : 0;
        srcFilesNodeData.SrcFilePath = srcFilesNodeData.SrcFilePaths[index];
        return index;
    }
}

