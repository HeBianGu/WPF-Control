// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Mvvm.ViewModels.Base;

namespace H.Components.VisionDiagram.Base;
public interface ISrcFilesNodeData : IFlowableNodeData
{
    SrcFileLoopMode SrcFileLoopMode { get; set; }
    bool UseAutoSwitch { get; set; }
    SrcFilePathData SrcFilePath { get; set; }
    ObservableCollection<SrcFilePathData> SrcFilePaths { get; set; }
    bool IsValid(out string message);
}

public static class SrcFilesNodeDataExtension
{
    public static bool TrySwitchNextFile(this ISrcFilesNodeData srcFilesNodeData)
    {
        if (srcFilesNodeData.UseAutoSwitch)
        {
            var source = srcFilesNodeData.SrcFilePaths;
            if (srcFilesNodeData.SrcFileLoopMode == SrcFileLoopMode.Selected)
            {
                var selects = srcFilesNodeData.SrcFilePaths.Where(x => x.IsSelected).ToList();
                if (selects.Count() == 0)
                    selects.Add(srcFilesNodeData.SrcFilePath);
                source = new ObservableCollection<SrcFilePathData>(selects);
            }
            var next = source.GetNext(srcFilesNodeData.SrcFilePath);
            if (next != null)
                srcFilesNodeData.SrcFilePath = next;
            return true;
        }
        return false;
    }
}

public enum SrcFileLoopMode
{
    All = 0,
    Selected = 1
}

public class SrcFilePathData : BindableBase
{
    public SrcFilePathData()
    {


    }

    public SrcFilePathData(string srcFilePath)
    {
        this.SrcFilePath = srcFilePath;
    }
    private string _SrcFilePath;
    public string SrcFilePath
    {
        get { return _SrcFilePath; }
        set
        {
            _SrcFilePath = value;
            RaisePropertyChanged();
        }
    }

    private bool _IsSelected;
    public bool IsSelected
    {
        get { return _IsSelected; }
        set
        {
            _IsSelected = value;
            RaisePropertyChanged();
        }
    }

}

