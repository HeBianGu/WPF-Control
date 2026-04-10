// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using H.Services.Message.IODialog;
using H.Extensions.Mvvm.ViewModels.Tree;
using H.Extensions.Tree;
using H.ValueConverter.Files;
using System.IO;

namespace H.Presenters.Common;


[Icon("\xE70F")]
[Display(Name = "文件信息")]
public class ExploreTreePresenter : DisplayBindableBase
{
    public ExploreTreePresenter(string filePath)
    {
        this.FilePath = filePath;
        this.Update(filePath);
    }

    private string _FilePath;
    public string FilePath
    {
        get { return _FilePath; }
        set
        {
            _FilePath = value;
            RaisePropertyChanged();
        }
    }


    private bool _IsRecursion;
    public bool IsRecursion
    {
        get { return _IsRecursion; }
        set
        {
            _IsRecursion = value;
            RaisePropertyChanged();
        }
    }


    private ObservableCollection<ITreeNode> _TreeNodes = new ObservableCollection<ITreeNode>();
    public ObservableCollection<ITreeNode> TreeNodes
    {
        get { return _TreeNodes; }
        set
        {
            _TreeNodes = value;
            RaisePropertyChanged();
        }
    }


    protected void Update(string filePath)
    {

        ExploreTree tree = new ExploreTree()
        {
            Root = filePath
        };
        System.Collections.Generic.IEnumerable<ITreeNode> result = tree.GetTreeNodes(this.IsRecursion);
        this.TreeNodes = result.ToObservable();
    }
}
